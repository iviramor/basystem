using bas.website.Service;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore.SqlServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using bas.website.Models.Data;

namespace bas.website
{
    public class Startup
    {
        public IConfiguration Configuration { get; }


        public Startup(IConfiguration Configuration)
        {
            this.Configuration = Configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            Configuration.Bind("ProjectConfig", new ProjectConfig());
            Configuration.Bind("SubjectArea", new SubjectAreaConfig());
            Configuration.Bind("CreditCalc", new CreditCalcConfig());
            Configuration.Bind("CreditCalcOut", new CreditCalcOutConfig());
            Configuration.Bind("History", new HistoryConfig());


            services.AddDbContext<BankDbContext>(config =>
            {
                config.UseSqlServer(Configuration.
                    GetConnectionString(ProjectConfig.Connection));
            });

            services.AddAuthentication("Cookie")
                .AddCookie("Cookie", config => {
                    config.LoginPath = "/credit/calculator";
                });


            services.AddAuthorization();

            services.AddMvc();

            services.AddControllersWithViews().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment()) app.UseDeveloperExceptionPage();

            app.UseRouting();
            app.UseStaticFiles();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => endpoints.MapDefaultControllerRoute());

        }
    }
}
