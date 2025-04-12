using DesafioBtg.Infra.Usuarios.Mapeamentos;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NHibernate;

namespace DesafioBtg.Ioc.Configuracoes;

public static class NHibernateExtensions
{
    public static IServiceCollection AddNHibernateExtensions(this IServiceCollection services, IConfiguration configuration)
    {
        string connectionString = configuration.GetConnectionString("MySql")!;

        var sessionFactory = Fluently.Configure()
            .Database(MySQLConfiguration.Standard.Dialect<NHibernate.Dialect.MySQL8Dialect>()
                .FormatSql()
                .ShowSql()
                .ConnectionString(connectionString))
            .Mappings(m => m.FluentMappings.AddFromAssemblyOf<UsuarioMap>())
            .BuildSessionFactory();

        services.AddSingleton(sessionFactory);

        services.AddScoped(factory => factory.GetService<ISessionFactory>()!.OpenSession());

        return services;
    }
}
