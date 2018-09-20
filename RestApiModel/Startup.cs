using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RestApiModel.Interfaces;
using RestApiModel.Model;
using RestApiModel.Repository;
using RestApiModel.Helper;
using TobitLogger.Core;
using TobitLogger.Logstash;
using TobitLogger.Middleware;
using TobitWebApiExtensions.Extensions;


namespace RestApiModel
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.Configure<DbSettings>(Configuration.GetSection("DbSettings"));

            services.AddSingleton<IDbContext, Helper.DbContext>();
            services.AddScoped<ICompanyRepository, CompanyRepo>();
            services.AddScoped<IAddressRepository, AddressRepo>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseMvc();
            //app.Run(async (context) =>
            //{
                //throw new Exception("Example exception");
            //});
            /*
            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Hello World!");
            });*/
        }
    }
}
