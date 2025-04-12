using DesafioBtg.DataTransfer.Pedidos.Requests;
using DesafioBtg.DataTransfer.Pedidos.Responses;

namespace DesafioBtg.Aplicacao.Pedidos.Servicos.Interfaces;

public interface IPedidosAppServico
{
    Task<string> InserirPedidoFilaAsync(PedidoInserirFilaRequest request, CancellationToken cancellationToken);
    PedidoValorTotalResponse BuscarValorTotalPedido(int codigoPedido);
}
