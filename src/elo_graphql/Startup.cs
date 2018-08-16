using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Elo;
using Elo.Adworks;
using Elo.GraphQL;
using GraphQL;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Elo
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            var connectionString = Configuration[SystemConstants.ConnectionStringKey];

            services
                .AddEntityFrameworkNpgsql()
                .AddDbContext<AdventureworksDataContext>(options => options.UseNpgsql(connectionString));


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddScoped<IDocumentExecuter, DocumentExecuter>();
            services.AddScoped<IObjectGraphSchemaProvider, ObjectGraphSchemaProvider>();
            services.AddScoped<IDependencyResolver>(s => new FuncDependencyResolver(s.GetRequiredService));

            services.AddTransient<SalespersonType>();
            services.AddScoped<SalespersonQuery>();

            services.AddTransient<StoreDemographicsType>();
            services.AddScoped<StoreDemographicsQuery>();

            services.AddTransient<StoreType>();
            services.AddScoped<StoreQuery>();

            services.AddTransient<SalesTerritoryType>();
            services.AddScoped<SalesTerritoryQuery>();

            services.AddTransient<SalesOrderType>();
            services.AddScoped<SalesOrderQuery>();

            services.AddTransient<SalesOrderDetailType>();
            services.AddScoped<SalesOrderDetailQuery>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                loggerFactory.AddConsole().AddDebug();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
