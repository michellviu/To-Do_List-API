using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using To_Do_List.Core.Domain.Entities;
using To_Do_List.Core.DomainService.Repository;
using To_Do_List.Core.DomainService.Services;
using To_Do_List.Infrastructure.Persistence.Context;
using To_Do_List.Infrastructure.Persistence.Repositories;
using To_Do_List.Infrastructure.Persistence.UnitWork;
using To_Do_List.Infrastructure.Services.AppService;

namespace To_Do_List.Infrastructure.Services;

public static class ServiceRegistration
{
    public static void AddApplicationServices(this IServiceCollection services)
    {

        services.AddScoped<ITodoItemRepository, TodoItemRepository>();
        services.AddScoped<UnitWork>();
        services.AddScoped<ITodoItemService, TodoItemService>();
        services.AddScoped<ITokenGenerator, TokenGenerator>();
    }

    public static void AddIdentityServices(this IServiceCollection services)
    {
        services.AddIdentity<User, IdentityRole<int>>(options =>
        {
            options.Password.RequiredLength = 6;
            options.Password.RequiredUniqueChars = 3;
            options.Password.RequireNonAlphanumeric = false;

        })
         .AddEntityFrameworkStores<AppDBContext>()
         .AddDefaultTokenProviders();
    }


    public static void AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        var jwtSettings = configuration.GetSection("JwtSettings");
        var key = Encoding.UTF8.GetBytes(jwtSettings["SigningKey"]);

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = jwtSettings["Issuer"],
                ValidAudience = jwtSettings["Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(key)
            };
        });
    }
}

