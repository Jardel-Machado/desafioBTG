using DesafioBtg.Aplicacao.Usuarios.Profiles;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace DesafioBtg.Ioc.Configuracoes;

public static class AutoMapperExtensions
{
    public static IServiceCollection AddAutoMapperExtensions(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(UsuariosProfile).GetTypeInfo().Assembly);

        return services;
    }
}
