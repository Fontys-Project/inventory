using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InventoryDI.Database;
using InventoryLogic.Facade;
using InventoryLogic.Product;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NSwag.Generation.Processors.Security;
using NSwag.Generation.Processors.Contexts;
using NSwag;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Authorization;
using System.Text;

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
            services.AddControllers();

            RSACryptoServiceProvider myRSA = new RSACryptoServiceProvider(2048);
            RSAParameters publicKey = myRSA.ExportParameters(true);
            //var tokenValidationParameters = new TokenValidationParameters
            //{
            //    //ValidateIssuerSigningKey = false,
            //    //ValidateIssuer = false,
            //    //ValidIssuer = "http://localhost:5000/",
            //    //IssuerSigningKey = new RsaSecurityKey(publicKey),
            //    ValidateIssuer = false,
            //    ValidateAudience = false,
            //    ValidateIssuerSigningKey = false,
            //    ValidateLifetime = false,
            //    RequireExpirationTime = false,
            //    RequireSignedTokens = false
            //};

            services.AddAuthentication(o =>
            {
                o.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            })
                    .AddJwtBearer(o =>
                    {
                        o.RequireHttpsMetadata = false;
                        o.SaveToken = true;
                        o.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuer = false,
                            ValidateAudience = false,
                            ValidateIssuerSigningKey = true,
                            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("qwertyuiopasdfghjklzxcvbnm123456")),
                            ValidateLifetime = false,
                            RequireExpirationTime = false,
                            RequireSignedTokens = false
                        };
                    });

            services.AddAuthorization(
            //    auth =>
            //{
            //    auth.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
            //        .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
            //        .RequireAuthenticatedUser().Build());
            //}
            );

            services.AddSingleton<ProductFacade>();

            DatabaseType databaseType;
            try
            {
                databaseType = (DatabaseType)Enum.Parse(typeof(DatabaseType), Environment.GetEnvironmentVariable("DBTYPE"), true);
            }
            catch
            {
                databaseType = DatabaseType.MOCK;
            }
            services.AddSingleton<IDatabaseFactory, DatabaseFactory>(x => new DatabaseFactory(databaseType));

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
                config.ApiGroupNames = new[] { "0.1", "0.2" };

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
