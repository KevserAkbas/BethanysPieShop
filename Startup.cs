using BethanysPieShop.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
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
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<Models.AppDbContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddDefaultIdentity<IdentityUser>().AddEntityFrameworkStores<AppDbContext>();

            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IPieRepository, PieRepository>();

            services.AddScoped<IOrderRepository, OrderRepository>();

            //services.AddSingleton
            //services.AddTransient
            services.AddScoped<ShoppingCart>(sp => ShoppingCart.GetCart(sp)); //bu �nemli, ��nk� kullan�c� sisteme geldi�inde GetCart y�ntemini kulanarak kapsaml� bir al��veri� sepeti olu�turulacak
            services.AddHttpContextAccessor();
            services.AddSession();
            services.AddControllersWithViews(); //MVC ile �al��mak i�in destek sa�layacak
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment()) //Development ortam�nda �al���l�yorsa if yap�s�ndaki kodlar� i�ler
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseHttpsRedirection(); //HTTP isteklerini HTTPS'ye y�nlendiren middleware
            app.UseStaticFiles(); //Resimler, JavaScript Dosyalar�, CSS vb. statik dosyalar� sunmak i�in
            app.UseSession();

            app.UseRouting();
            app.UseAuthentication();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                endpoints.MapRazorPages();
            });
        }
    }
}
