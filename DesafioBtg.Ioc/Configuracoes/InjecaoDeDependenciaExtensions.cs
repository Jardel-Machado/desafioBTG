using DesafioBtg.Aplicacao.Usuarios.Servicos;
using DesafioBtg.Dominio.Usuarios.Servicos;
using DesafioBtg.Infra.Usuarios.Repositorios;
using Microsoft.Extensions.DependencyInjection;
using Scrutor;

namespace DesafioBtg.Ioc.Configuracoes;

public static class InjecaoDeDependenciaExtensions
{
    public static IServiceCollection AddInjecaoDeDependenciaExtensions(this IServiceCollection services)
    {
        services.Scan(scan => scan
            .FromAssemblyOf<UsuariosAppServico>()
                .AddClasses(classes => classes.Where(type => !typeof(Exception).IsAssignableFrom(type) && !type.FullName.Contains("RegexGenerator")))
                    .AsImplementedInterfaces()
                    .WithScopedLifetime());

        services.Scan(scan => scan
            .FromAssemblyOf<UsuariosRepositorio>()
                .AddClasses(classes => classes.Where(type => !typeof(Exception).IsAssignableFrom(type) && !type.FullName.Contains("RegexGenerator")))
                    .UsingRegistrationStrategy(RegistrationStrategy.Skip)
                    .AsImplementedInterfaces()
                    .WithScopedLifetime());

        services.Scan(scan => scan
            .FromAssemblyOf<UsuariosServico>()
                .AddClasses(classes => classes.Where(type => !typeof(Exception).IsAssignableFrom(type) && !type.FullName.Contains("RegexGenerator")))
                    .AsImplementedInterfaces()
                    .WithScopedLifetime());

        return services;
    }
}
