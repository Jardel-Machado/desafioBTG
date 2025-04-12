using DesafioBtg.Dominio.Pedidos.Consultas;
using DesafioBtg.Dominio.Pedidos.Entidades;
using DesafioBtg.Dominio.Pedidos.Servicos.Comandos;

namespace DesafioBtg.Dominio.Pedidos.Servicos.Interfaces;

public interface IPedidosServico
{
    Task<Pedido> InserirAsync(PedidoComando comando, CancellationToken cancellationToken);
    Task<Pedido> ValidarAsync(int id, CancellationToken cancellationToken);
    PedidoValorTotalConsulta BuscarValorTotalPedido(int codigoPedido);
}
