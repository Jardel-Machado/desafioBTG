using DesafioBtg.Dominio.Rabbitmq.Repositorios.Interfaces;
using DesafioBtg.Infra.Rabbitmq.Repositorios;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;

namespace DesafioBtg.Ioc.Configuracoes;

public static class RabbitmqExtensions
{
    public static IServiceCollection AddRabbitmq(this IServiceCollection services, IConfiguration configuration)
    {
        IConfigurationSection rabbitmqSection = configuration.GetSection("RabbitMQ");

        string hostName = Environment.GetEnvironmentVariable("RabbitMQ__Host") ?? rabbitmqSection["HostName"];

        string userName = Environment.GetEnvironmentVariable("RabbitMQ__Username") ?? rabbitmqSection["UserName"];

        string password = Environment.GetEnvironmentVariable("RabbitMQ__Password") ?? rabbitmqSection["Password"];

        int port = int.Parse(Environment.GetEnvironmentVariable("RabbitMQ__Port") ?? rabbitmqSection["Port"]);        

        services.AddSingleton<IConnectionFactory>(new ConnectionFactory
        {
            HostName = hostName,
            Port = port,
            UserName = userName,
            Password = password,
            VirtualHost = "/", // Se usar com docker usar essa linha
            AutomaticRecoveryEnabled = true, //Recuperação automática de conexão
            RequestedConnectionTimeout = TimeSpan.FromSeconds(30), //Tempo limite de conexão
            TopologyRecoveryEnabled = true //Recuperação automática de topologia
        });

        services.AddSingleton<IRabbitmqRepositorio, RabbitmqRepositorio>();

        return services;
    }
}
