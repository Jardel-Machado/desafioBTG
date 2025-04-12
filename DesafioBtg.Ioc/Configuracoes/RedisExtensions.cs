using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace DesafioBtg.Ioc.Configuracoes;

public static class RedisExtensions
{
    public static IServiceCollection AddRedisExtensions(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<IConnectionMultiplexer>(sp =>
        {
            var connectionString = configuration.GetValue<string>("Redis:ConnectionString");

            return ConnectionMultiplexer.Connect(connectionString!);
        });

        services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = configuration.GetValue<string>("Redis:ConnectionString");
        });

        return services;
    }
}
