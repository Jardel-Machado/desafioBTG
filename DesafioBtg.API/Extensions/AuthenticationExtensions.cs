using DesafioBtg.Dominio.Redis.Repositorios.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace DesafioBtg.API.Extensions;

public static class AuthenticationExtensions
{
    //Buscando a chave no Redis
    public static async Task<IServiceCollection> AddAuthenticationExtensions(this IServiceCollection services, IConfiguration configuration, IRedisRepositorio redisRepositorio)
    {
        string jwtKey = await redisRepositorio.ObterChaveJwtAsync();

        if (string.IsNullOrEmpty(jwtKey))
        {
            jwtKey = configuration["Jwt:Key"]!;

            await redisRepositorio.DefinirChaveJwtAsync(jwtKey!);
        }

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = configuration["Jwt:Issuer"],
                    ValidAudience = configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey)),
                    ClockSkew = TimeSpan.Zero
                };
            });

        return services;
    }

    //Buscando a chave no appsettings.json
    //public static IServiceCollection AddCustomJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
    //{
    //    services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    //        .AddJwtBearer(options =>
    //        {
    //            options.TokenValidationParameters = new TokenValidationParameters
    //            {
    //                ValidateIssuer = true,
    //                ValidateAudience = true,
    //                ValidateLifetime = true,
    //                ValidateIssuerSigningKey = true,
    //                ValidIssuer = configuration["Jwt:Issuer"],
    //                ValidAudience = configuration["Jwt:Audience"],
    //                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"])),
    //                ClockSkew = TimeSpan.Zero
    //            };
    //        });

    //    return services;
    //}
}
