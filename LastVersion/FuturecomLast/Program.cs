using Business.Services;
using DataAccessLayer.Concrete;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Options;
using System;
using System.Net;

namespace FuturecomLast;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllersWithViews();
        builder.Services.AddMvc(config =>
        {
            var policy = new AuthorizationPolicyBuilder()
            .RequireAuthenticatedUser()
            .Build();
            config.Filters.Add(new AuthorizeFilter(policy));
        });
        builder.Services.AddDbContext<Context>();

        builder.Services.AddHttpContextAccessor();

        builder.Services.AddTransient<IUserService, UserService>();


        builder.Services.AddHttpContextAccessor();

        builder.Services.AddIdentity<User, Role>()
      .AddEntityFrameworkStores<Context>()
      .AddDefaultTokenProviders();

        builder.Services.AddSession(options =>
        {
            options.IdleTimeout = TimeSpan.FromMinutes(3);
            options.Cookie.HttpOnly = true;
            options.Cookie.IsEssential = true; 
        });
           


        builder.Services.AddScoped<RoleManager<Role>>();



        builder.Services.AddAuthorization();



        builder.Services.AddMvc();
        builder.Services.ConfigureApplicationCookie(o =>
        {
          //  o.ExpireTimeSpan = TimeSpan.FromMinutes(3);
          
            o.LoginPath = "/Auth/Login";
            o.LogoutPath = "/Auth/Login";
        });
            
        //builder.Services.AddAuthentication().AddCookie();


        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");

            app.UseHsts();
        }




        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseStatusCodePagesWithReExecute("/ErrorPages/HandleError/{0}");



        


        app.UseRouting();


        app.UseAuthentication();
        //Authonticate authorizon üzerindeydi
        app.UseAuthorization();

        app.UseSession();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Auth}/{action=Login}/{id?}");

        app.Run();
    }
}

