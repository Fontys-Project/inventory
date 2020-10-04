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
            services.AddSingleton<ProductFacade>();

            DatabaseType databaseType;

            try
            {
                databaseType = (DatabaseType)Enum.Parse(typeof(DatabaseType), Environment.GetEnvironmentVariable("DBTYPE"), true);
            } catch
            {
                databaseType = DatabaseType.MOCK;
            }

            services.AddSingleton<IDatabaseFactory,DatabaseFactory>(x => new DatabaseFactory(databaseType));

            services.AddSwaggerDocument(config =>
            {
                config.PostProcess = document =>
                {
                    document.Info.Version = "v1";
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
