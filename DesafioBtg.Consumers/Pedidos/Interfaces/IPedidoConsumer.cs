namespace DesafioBtg.Consumers.Pedidos.Interfaces;

public interface IPedidoConsumer
{
    Task ExecuteAsync(CancellationToken cancellationToken);
}
