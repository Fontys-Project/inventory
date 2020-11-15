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
using System.Text;
using System.Security.Cryptography;
using System;
using InventoryDI;
using InventoryLogic.Interfaces;

namespace Inventory
{
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
                    .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, o =>
                     {
                         o.IncludeErrorDetails = true;
                         RsaSecurityKey rsa = services.BuildServiceProvider().GetRequiredService<RsaSecurityKey>();

                         o.TokenValidationParameters = new TokenValidationParameters
                         {
                             ValidateIssuer = false,
                             ValidateAudience = false,
                             ValidateIssuerSigningKey = true,
                             IssuerSigningKey = rsa,
                             ValidateLifetime = false,
                             RequireExpirationTime = true,
                             RequireSignedTokens = true
                         };
                     });

            services.AddSingleton<ProductsFacade>();
            services.AddSingleton<TagsFacade>();
            services.AddSingleton<StocksFacade>();
            services.AddSingleton<IDatabaseFactory, DatabaseFactory>(x => new DatabaseFactory(DatabaseType.MYSQL));
            services.AddTransient<RsaSecurityKey>(provider => {
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
            services.AddSingleton<IRepositoryFactory, RepositoryFactory>();             


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
