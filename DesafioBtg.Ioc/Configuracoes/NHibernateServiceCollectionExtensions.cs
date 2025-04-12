using DesafioBtg.Infra.Usuarios.Mapeamentos;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;
using NHibernate;
using NHibernate.Tool.hbm2ddl;
using Environment = NHibernate.Cfg.Environment;

namespace DesafioBtg.Ioc.Configuracoes;

public static class NHibernateServiceCollectionExtensions
{
    public static void CriarTabelas(string connectionString)
    {
        var configuration = Fluently.Configure()
            .Database(MySQLConfiguration.Standard
                .FormatSql()
                .ShowSql()
                .ConnectionString(connectionString))
            .Mappings(m => m.FluentMappings.AddFromAssemblyOf<UsuarioMap>())
            .BuildConfiguration();
        
        new SchemaUpdate(configuration).Execute(false, true);
    }

    public static IServiceCollection AddNHibernateSchema(this IServiceCollection services, IConfiguration configuration, IHostEnvironment environment)
    {
        var logger = LoggerSistemaExtensions.AddLoggerConsole().AddLoggerSistema("NHibernate");

        var connectionString = configuration.GetConnectionString("MySql")!;

        NHibernateServiceCollectionExtensions.CriarSchemaSeNaoExistir(connectionString);

        NHibernateServiceCollectionExtensions.Inicializar(connectionString, environment, logger, configuration);

        return services;
    }

    private static void CriarSchemaSeNaoExistir(string connectionString)
    {
        var nomeSchema = ObterNomeSchema(connectionString);

        // Remove o Database da connection string para conectar no MySQL sem schema selecionado
        var builder = new MySqlConnectionStringBuilder(connectionString)
        {
            Database = string.Empty
        };

        using var connection = new MySqlConnection(builder.ConnectionString);

        connection.Open();

        // Cria o schema dinamicamente
        using var command = new MySqlCommand($"CREATE SCHEMA IF NOT EXISTS `{nomeSchema}`;", connection);

        command.ExecuteNonQuery();
    }

    private static string ObterNomeSchema(string connectionString)
    {
        var builder = new MySqlConnectionStringBuilder(connectionString);

        return builder.Database;
    }

    private static void Inicializar(string connectionString, IHostEnvironment environment, ILogger logger, IConfiguration configuration)
    {
        bool desenvolvimento = environment.IsDevelopment();

        string evento = "NHibernateSchema";

        string redisConnectionString = configuration.GetValue<string>("Redis:ConnectionString")!;

        string redisNomeSessao = configuration.GetValue<string>("Redis:NomeSessao")!;

        ISessionFactory sessionFactory = Fluently.Configure()
            .Database(MySQLConfiguration.Standard
                .FormatSql()
                .ShowSql()
                .ConnectionString(connectionString))
            .Mappings(m => m.FluentMappings.AddFromAssemblyOf<UsuarioMap>())
            .ExposeConfiguration(cfg =>
            {
                cfg.SetProperty(Environment.UseSecondLevelCache, "true");

                cfg.SetProperty(Environment.UseQueryCache, "true");

                if (!desenvolvimento)
                {
                    cfg.SetProperty(Environment.CacheProvider, "NHibernate.Caches.StackExchangeRedis.RedisCacheProvider");

                    cfg.SetProperty("redis.connectionstring", redisConnectionString);

                    cfg.SetProperty("redis.session.name", redisNomeSessao);

                    logger.LogInformation("<{EventoId}> {Mensagem} Redis: {RedisConnection} Sessao: {RedisSessao}", evento, "Ambiente de produção detectado. Cache Redis habilitado.", redisConnectionString, redisNomeSessao);
                }

                cfg.SetProperty(Environment.CacheProvider, "NHibernate.Cache.HashtableCacheProvider");

                logger.LogInformation("<{EventoId}> {Mensagem}", evento, "Ambiente de desenvolvimento detectado. Cache em memória habilitado.");

                cfg.SetProperty(Environment.GenerateStatistics, "true");

                logger.LogInformation("<{EventoId}> {Mensagem}", evento, "Atualizando/criando tabelas com SchemaUpdate.");

                new SchemaUpdate(cfg).Execute(false, true);
            })
            .BuildSessionFactory();

        if (sessionFactory.Statistics.IsStatisticsEnabled)
        {
            var stats = sessionFactory.Statistics;

            logger.LogInformation("<{EventoId}> Estatísticas do NHibernate", evento);

            logger.LogInformation("<{EventoId}> Sessões abertas: {Total}", evento, stats.SessionOpenCount);

            logger.LogInformation("<{EventoId}> Sessões fechadas: {Total}", evento, stats.SessionCloseCount);

            logger.LogInformation("<{EventoId}> Entidades carregadas: {Total}", evento, stats.EntityLoadCount);

            logger.LogInformation("<{EventoId}> Entidades recuperadas via Fetch: {Total}", evento, stats.EntityFetchCount);

            logger.LogInformation("<{EventoId}> Execuções de query: {Total}", evento, stats.QueryExecutionCount);

            logger.LogInformation("<{EventoId}> Cache de 2º nível - Hits: {Hits}, Misses: {Misses}, Inserts: {Puts}", evento, stats.SecondLevelCacheHitCount, stats.SecondLevelCacheMissCount, stats.SecondLevelCachePutCount);
        }
    }
}
