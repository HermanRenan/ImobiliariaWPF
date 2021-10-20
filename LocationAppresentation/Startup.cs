using Application.Interfaces;
using Application.Model;
using Application.OpenApp;
using Domain.Interfaces.Generics;
using Domain.Interfaces.InterfaceProduct;
using Entities.Entities;
using infrastructure.Configuration;
using infrastructure.Repository.Generics;
using infrastructure.Repository.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LocationAppresentation
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
            services.AddSingleton(typeof(IGeneric<>), typeof(RepositoryGenerics<>));
            services.AddSingleton<IProduct, RepositoryProduct>();
            services.AddSingleton<InterfaceAppProduct, ServiceProduct>();

            #region Adicionar os Mappers
            services.AddAutoMapper(typeof(ClientModel));
            services.AddAutoMapper(typeof(Client));
            #endregion

            services.AddControllersWithViews();

            ConfigureDataBase(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }

        private void ConfigureDataBase(IServiceCollection services)
        {
            services.AddDbContext<ContextBase>(options => 
            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
        }
    }
}
