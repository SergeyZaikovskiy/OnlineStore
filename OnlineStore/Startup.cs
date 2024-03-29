﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OnlineStore.DAL.Context;
using OnlineStore.Data;
using OnlineStore.Domain.Entities.UserEntities;
using OnlineStore.Infrastructure.Implementations;
using OnlineStore.Infrastructure.Interfeices;
using SmartBreadcrumbs.Extensions;

namespace OnlineStore
{
    public class Startup
    {
        
        public IConfiguration Configuration { get; set; }

        public Startup(IConfiguration Configuration) {
            this.Configuration = Configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<OnlineStoreContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddTransient<OnlineStoreContextInitializer>();

            //services.AddSingleton создается на время открытия приложения
            //services.AddScoped<>() регистрируется на время обработки запроса
            //services.AddTransient<>() на каждый запрос создается свой объект

            services.AddScoped<IEmployee, SqlEmployeeData>();
            services.AddScoped<IOrderService,SqlOrdersService>();
            services.AddScoped<IProductData, SqlProductData>();
            services.AddScoped<ICartService, CookieCartService>();           


            services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<OnlineStoreContext>()
                .AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(cfg =>
            {
                cfg.Password.RequiredLength = 5;
                cfg.Password.RequireDigit = true;
                cfg.Password.RequireLowercase = true;
                cfg.Password.RequireUppercase = true;
                cfg.Password.RequiredUniqueChars = 3;
                cfg.Password.RequireNonAlphanumeric = false;

                cfg.Lockout.MaxFailedAccessAttempts = 4;
                cfg.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
                cfg.Lockout.AllowedForNewUsers = true;

                cfg.User.RequireUniqueEmail = false;

            });           

            services.ConfigureApplicationCookie(cfg =>
            {
                cfg.Cookie.HttpOnly = true;
                cfg.Cookie.Expiration = TimeSpan.FromDays(150);
                cfg.Cookie.MaxAge = TimeSpan.FromDays(150);


                cfg.LoginPath = "/Account/Login";
                cfg.LogoutPath = "/Account/Logout";
                cfg.AccessDeniedPath = "/Account/AccessDenied";

                cfg.SlidingExpiration = true;
            });

            services.AddMvc();

            services.AddBreadcrumbs(GetType().Assembly, options =>
            {
                // Testing
                options.DontLookForDefaultNode = true;
            });

        }

       
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, OnlineStoreContextInitializer db)
        {
            db.InitializeAsync().Wait();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();
            app.UseDefaultFiles();
            app.UseAuthentication();

            app.UseMvc(route =>
            {
                //Маршрут для обрасти администратора
                route.MapRoute(
                    name: "areas",
                    template: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
                  );

                route.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
