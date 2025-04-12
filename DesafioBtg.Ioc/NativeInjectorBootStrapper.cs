using DesafioBtg.Ioc.Configuracoes;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DesafioBtg.Ioc;
public static class NativeInjectorBootStrapper
{
    public static void RegisterServices(IServiceCollection services, IConfiguration configuration)
    {
        // Configura NHibernate
        services.AddNHibernateExtensions(configuration);

        // Configura AutoMapper
        //services.AddAutoMapperExtensions();

        // Configura o Mapster
        services.AddMapsterExtensions();

        // Configura o HttpClient
        services.AddHttpContextExtensions(configuration);

        // Configura o Redis
        services.AddRedisExtensions(configuration);

        // Configura o Polly
        services.AddConfiguracoesPolly();

        // Configura o RabbitMQ
        services.AddRabbitmq(configuration);

        // Configura a Injeção de Dependência
        services.AddInjecaoDeDependenciaExtensions();
    }
}
