using DesafioBtg.Ioc.Interfaces;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace DesafioBtg.Ioc.Abstracoes;

public abstract class BackgroundServiceBase<T> : BackgroundService, IHostedServiceCustom
{
    protected readonly ILogger<T> logger;

    protected CancellationToken internalCancellationToken;

    private DadosServico DadosServico { get; } = new();

    private static string Name => typeof(T).Name;

    protected BackgroundServiceBase(ILogger<T> logger)
    {
        this.logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await Task.Delay(2000, stoppingToken);

        logger.LogInformation("<{EventID}> - Inicializando {BackgroundServiceName} Hosted Service.", "InicializandoBackgroundService", BackgroundServiceBase<T>.Name);

        DadosServico.Name = Name;

        DadosServico.EmExecucao = true;

        DadosServico.DataInicio = DateTime.Now;

        await Process(stoppingToken);
    }

    protected virtual Task Process(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    public override async Task StopAsync(CancellationToken cancellationToken)
    {
        DadosServico.EmExecucao = false;

        DadosServico.DataEncerramento = DateTime.Now;

        logger.LogInformation("<{EventID}> - Encerrando {BackgroundServiceName} Hosted Service.", "EncerrandoBackgroundService", BackgroundServiceBase<T>.Name);

        await base.StopAsync(cancellationToken);
    }

    public DadosServico GetDadosServico()
    {
        return DadosServico;
    }

    public CancellationToken GetCancellationToken()
    {
        return internalCancellationToken;
    }
}
