using DesafioBtg.Dominio.ItensPedidos.Entidades;

namespace DesafioBtg.Dominio.ItensPedidos.Servicos.Interfaces;

public interface IItensPedidosServico
{
    Task<ItemPedido> ValidarAsync(int id, CancellationToken cancellationToken);
}
