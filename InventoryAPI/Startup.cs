using System.Linq;
using InventoryLogic.Facade;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NSwag.Generation.Processors.Security;
using NSwag;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Security.Cryptography;
using System;
using InventoryDI;
using InventoryLogic.Interfaces;
using System.Security.Claims;
using System.Text.Json;
using System.Collections.Generic;
using System.Threading.Tasks;
using InventoryAPI.Security;
using NJsonSchema.Generation;
using InventoryLogic.EventBus;

namespace Inventory
{

    public class SchemaNameGenerator : ISchemaNameGenerator
    {
        public string Generate(Type type)
        {
            return type.Name.EndsWith("RequestModel") ? type.Name.Replace("RequestModel", string.Empty) : type.Name;
        }
    }

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers()
                .AddNewtonsoftJson(options =>
                    options.SerializerSettings.ReferenceLoopHandling =
                        Newtonsoft.Json.ReferenceLoopHandling.Ignore
                );



            services.AddAuthentication(o =>
                {
                    o.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                    o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

                })
                    .AddJwtBearer(); // use separate options below to allow dependency injection with rsa key to work.

            // Dependency injection for RSA key -> the instance of the rsa key needs to be a singleton
            services.AddOptions<JwtBearerOptions>(JwtBearerDefaults.AuthenticationScheme)
                .Configure<RsaSecurityKey>((options, skey) =>
                {
                    options.IncludeErrorDetails = true;

                    // fix to map permissions as roles in .net mvc via event handling:
                    options.Events = new JwtBearerEvents
                    {
                        OnTokenValidated = async ctx =>
                        {
                            await Task.Run(() =>
                            {
                                var newClaims = new List<Claim>();

                                foreach (Claim claim in ctx.Principal.Claims)
                                {
                                    if (claim.Type == "user_claims") // we use this attribute chosen by the login service that contains the permissions to be converted to .net mvc identity roles.
                                    {
                                        JwtClaims claims = JsonSerializer.Deserialize<JwtClaims>(claim.Value);

                                        foreach (String permission in claims.permissions)
                                        {
                                            // add each permission as .net identity role for use in MVC controllers role filtering
                                            newClaims.Add(new Claim(ClaimTypes.Role, permission));
                                        }

                                    }
                                }
                                // build new Claims Identity
                                var appIdentity = new ClaimsIdentity(newClaims);

                                // add the claims to the principal identity
                                ctx.Principal.AddIdentity(appIdentity);
                            });
                        }
                    };

                    options.TokenValidationParameters.ValidateIssuer = false;
                    options.TokenValidationParameters.ValidateAudience = false;
                    options.TokenValidationParameters.ValidateIssuerSigningKey = true;
                    options.TokenValidationParameters.IssuerSigningKey = skey;
                    options.TokenValidationParameters.ValidateLifetime = false;
                    options.TokenValidationParameters.RequireExpirationTime = true;
                    options.TokenValidationParameters.RequireSignedTokens = true;

                });

            // needed in order for the rsa validation key to remain in memory (for some reason it does not work otherwise).
            services.AddSingleton<RsaSecurityKey>(provider => {
                RSA rsa = RSA.Create();
                rsa.ImportSubjectPublicKeyInfo(
                    source: Convert.FromBase64String(@"MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAvKZzT8MhSEHRRecmD1kM
oW8unUYufupt/4AV5Yd6YSR84uHjMuArp/lQx/ROrCvt5xSBB547L5oPeJECeS4h
p7Djlsd3VyIvs+yY+e9FM72MLnaoydQLCHKz8RQF2+mr8SeM/4va6vGSTTW3F5Ay
9LOYDsQa18yYJ+a7tc2PQJQGZYQvQ1YWerlScrcZQ1ChB5u+mALdg1VoKpW2n+bP
6ucjGidjqUbLMPKOHQRQBuoMNTXk2fzKQmhhML9lUKw5+2RZ2jtJKBVBNprV1EDP
yBTXutHUCV6D4esOqc35e1jZo6kQGGaWQ0rIpupv/qyXLHTz6Gi/mnvZuMQJIJZ+
iwIDAQAB"),
                    bytesRead: out int _);

                return new RsaSecurityKey(rsa);
            });


            services.AddSingleton<ProductsFacade>();
            services.AddSingleton<TagsFacade>();
            services.AddSingleton<StocksFacade>();
            services.AddSingleton<IRepositoryFactory, RepositoryFactory>();
            services.AddSingleton<IEventBusPublisher, RabbitMessenger>();



            services.AddApiVersioning(x =>
                {
                    x.DefaultApiVersion = new ApiVersion(0, 1);
                    x.AssumeDefaultVersionWhenUnspecified = true;
                    x.ReportApiVersions = true;
                });
            services.AddVersionedApiExplorer(options =>
                {
                    options.GroupNameFormat = "VVVV";
                    options.SubstituteApiVersionInUrl = true;
                    options.SubstitutionFormat = "VVVV";
                });
            services.AddSwaggerDocument(config =>
            {
                config.DocumentName = "0.* (not for production)";
                //config.ApiGroupNames = new[] { "0.1" };

                config.SchemaNameGenerator = new SchemaNameGenerator();

                // Authentication
                config.OperationProcessors.Add(new OperationSecurityScopeProcessor("JWT token"));
                config.AddSecurity("JWT token", Enumerable.Empty<string>(),
                    new OpenApiSecurityScheme()
                    {
                        Type = OpenApiSecuritySchemeType.ApiKey,
                        Name = "Authorization",
                        In = OpenApiSecurityApiKeyLocation.Header,
                        Description = "Copy this into the value field: \nBearer {my long token}"
                    }
                );
                config.PostProcess = document =>
                    {
                        document.Info.Version = "0.1";
                        document.Info.Title = "Inventory Microservice API";
                        document.Info.Description = "API Documentation";
                    };
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // Register the Swagger generator and the Swagger UI middlewares
            app.UseOpenApi();
            app.UseSwaggerUi3();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
                {
                    endpoints.MapControllers();
                });
        }
    }
}
