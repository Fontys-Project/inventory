using System.Linq;
using InventoryDI.Database;
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
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
);

            services.AddAuthentication(o =>
            {
                o.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                    .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, o =>
                     {
                         o.TokenValidationParameters = new TokenValidationParameters
                         {
                             ValidateIssuer = false,
                             ValidateAudience = false,
                             ValidateIssuerSigningKey = true,
                             IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("qwertyuiopasdfghjklzxcvbnm123456")),
                             ValidateLifetime = false,
                             RequireExpirationTime = false,
                             RequireSignedTokens = true
                         };
                     });

            services.AddSingleton<ProductFacade>();
            services.AddSingleton<ProductTagsFacade>();
            services.AddSingleton<IDatabaseFactory, DatabaseFactory>(x => new DatabaseFactory(DatabaseType.MYSQL));

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
