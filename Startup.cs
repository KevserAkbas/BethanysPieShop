using BethanysPieShop.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BethanysPieShop
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {            
            services.AddScoped<ICategoryRepository, MockCategoryRepository>();
            services.AddScoped<IPieRepository, MockPieRepository>();
            //services.AddSingleton
            //services.AddTransient

            services.AddControllersWithViews(); //MVC ile çalýþmak için destek saðlayacak
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment()) //Development ortamýnda çalýþýlýyorsa if yapýsýndaki kodlarý iþler
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseHttpsRedirection(); //HTTP isteklerini HTTPS'ye yönlendiren middleware
            app.UseStaticFiles(); //Resimler, JavaScript Dosyalarý, CSS vb. statik dosyalarý sunmak için
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name:"default",
                    pattern:"{controller=Home}/{action=index}/{id}"
                    );
            });
        }
    }
}
