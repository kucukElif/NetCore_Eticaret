using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using DAL.Context;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using BLL.Abstract;
using BLL.Repository;
using DAL.Entity;

namespace MVC
{
    public class Startup
    {
        private readonly IConfiguration configuration;

        public Startup(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>(options=>options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),b=>b.MigrationsAssembly("MVC")));
            services.AddMvc(x=>x.EnableEndpointRouting=false);
            //Scoped
            services.AddScoped<ICategoryService, CategoryRepository>();
            services.AddScoped<IProductService, ProductRepository>();
            services.AddScoped<IOrderService, OrderRepository>();
            //Identity
            services.AddIdentity<AppUser, AppUserRole>().AddEntityFrameworkStores<AppDbContext>();
            //Session

            services.AddSession(x => { x.Cookie.Name = "Cart.Session"; x.IdleTimeout = TimeSpan.FromMinutes(5); });
            //Cookie
            services.ConfigureApplicationCookie(x => {
                x.LoginPath = new PathString("/Member/Login");
                x.AccessDeniedPath = new PathString("/Member/Login");
                x.Cookie = new CookieBuilder
                {
                    Name = "LoginCookie",
                    Expiration = null
                };
                x.ExpireTimeSpan = TimeSpan.FromMinutes(5);
            });
          
        
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseSession();
            app.UseRouting();

            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapGet("/", async context =>
            //    {
            //        await context.Response.WriteAsync("Hello World!");
            //    });
            //});
            app.UseStaticFiles();
            app.UseRouting();
            app.UseStaticFiles();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                name: "areas",
                template: "{area=exists}/{controller=Home}/{action=Index}/{id?}");

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}"
                    );
                
        });

        }
    }
}
