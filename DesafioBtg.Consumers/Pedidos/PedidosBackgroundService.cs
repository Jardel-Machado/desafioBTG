using DesafioBtg.Consumers.Pedidos.Interfaces;
using DesafioBtg.Ioc.Abstracoes;

namespace DesafioBtg.Consumers.Pedidos;

public class PedidosBackgroundService(IServiceProvider serviceProvider, ILogger<PedidosBackgroundService> logger) : BackgroundServiceBase<PedidosBackgroundService>(logger)
{
    protected override async Task Process(CancellationToken cancellationToken)
    {
        internalCancellationToken = cancellationToken;

        using var scope = serviceProvider.CreateScope();

        var executor = scope.ServiceProvider.GetRequiredService<IPedidoConsumer>();

        await executor.ExecuteAsync(cancellationToken);
    }
}
