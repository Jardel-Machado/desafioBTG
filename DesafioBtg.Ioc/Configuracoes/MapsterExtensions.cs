using DesafioBtg.Aplicacao.Usuarios.Mappings;
using Mapster;
using MapsterMapper;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace DesafioBtg.Ioc.Configuracoes;

public static class MapsterExtensions
{
    public static IServiceCollection AddMapsterExtensions(this IServiceCollection services)
    {
        //se não precisasse configurar nada personalizado, como Scan(...)
        //services.AddMapster();

        // Escaneia os mapeamentos implementando IRegister
        TypeAdapterConfig.GlobalSettings.Scan(typeof(UsuariosMapping).GetTypeInfo().Assembly);

        // Registra a configuração global e o IMapper
        services.AddSingleton(TypeAdapterConfig.GlobalSettings);

        services.AddScoped<IMapper, ServiceMapper>();

        return services;
    }
}
