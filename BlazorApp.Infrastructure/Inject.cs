using BlazorApp.Infrastructure.Common;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Identity;
using BlazorApp.Application.Interfaces;
using BlazorApp.Infrastructure.Authentication;
using BlazorApp.Bll.Models;
using BlazorApp.Infrastructure.Repositories;

namespace BlazorApp.Infrastructure;

public static class Inject
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<ServiceOptions>(configuration);
        var options = configuration.Get<ServiceOptions>();
        
        services.AddDbContextFactory<ApplicationDbContext>(o =>
        {
            var connectionString = options?.ConnectionString ?? string.Empty;
            connectionString = connectionString.TrimEnd(';');

            _ = o.UseNpgsql(connectionString);
        });

        return services;
    }

    public static IServiceCollection AddAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthentication().AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                ValidateAudience = false,
                ValidateIssuer = false,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetSection("AppSettings:Token").Value!))
            };
        });

        return services;
    }

    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IRepository<JobSeeker>, JobSeekersRepository>()
                .AddScoped<IRepository<Vacancy>, VacancyRepository>()
                .AddScoped<IRepository<Resume>, ResumeRepository>()
                .AddScoped<IRepository<Interview>, InterviewRepository>();
        

        return services;
    }

    public static IServiceCollection AddAdapters(this IServiceCollection services)
    {
        services.AddScoped<IAuthenticationAdapter, AuthenticationAdapter>();
        return services;
    }
    //public static IServiceCollection AddAuthentication(this IServiceCollection services, IConfiguration configuration)
    //{
    //    services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    //        .AddJwtBearer(options =>
    //        {
    //            options.TokenValidationParameters = new TokenValidationParameters
    //            {
    //                ValidateIssuer = true,
    //                ValidIssuer = AuthOptions.ISSUER,
    //                ValidateAudience = true,
    //                ValidAudience = AuthOptions.AUDIENCE,
    //                ValidateLifetime = true,
    //                IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
    //                ValidateIssuerSigningKey = true
    //            };
    //        });

    //    return services;
    //}
}