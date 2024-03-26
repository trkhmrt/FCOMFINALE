
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DataAccessLayer.Concrete;
using DataAccessLayer.TokenManager;
using EntityLayer.Concrete;
using FuturecomApi.Middlewares;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.IdentityModel.Tokens;

namespace FuturecomApi;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        //Identity'nin projeye dahil edilmesi
        builder.Services.AddDbContext<Context>();

        builder.Services.AddIdentity<User,Role>()
       .AddEntityFrameworkStores<Context>()
       .AddDefaultTokenProviders();

       

        /* builder.Services.AddControllers(op =>
         {
             op.Filters.Add(new ValidateFilter());
         });
        */
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddControllers(options =>
        {
            options.Filters.Add(new AuthorizeFilter());
        });

       // builder.Services.AddScoped<ValidateFilter>();
        builder.Services.AddCors(options =>
        {
            options.AddDefaultPolicy(
                builder =>
                {

                    //you can configure your custom policy
                    builder.AllowAnyOrigin()
                                        .AllowAnyHeader()
                                        .AllowAnyMethod();
                });
        });

        //JWT BEARER ADD PROJECT
        builder.Services.AddAuthentication(options => {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        })
            .AddJwtBearer(o =>
        {
            o.TokenValidationParameters = new TokenValidationParameters
            {
                ValidIssuer = builder.Configuration["Jwt:Issuer"],
                ValidAudience = builder.Configuration["Jwt:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey
                (Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                
            };
        });


        builder.Services.AddAuthorization();
        builder.Services.AddAuthentication();
        builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("Jwt"));


        builder.Services.AddTransient<GlobalExceptionHandlingMiddleware>();
      
       
   

        var app = builder.Build();

       

        app.UseCors();
        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }


      

         


        //GLobalhandling
        app.UseMiddleware<GlobalExceptionHandlingMiddleware>();
     
      
        app.UseRouting();



        app.UseAuthentication();
        //Autho buradaydı
        app.UseAuthorization();


        

        app.MapControllers();
      /*  app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
      */
        app.UseHttpsRedirection();
        app.Run();
    }
  
}

