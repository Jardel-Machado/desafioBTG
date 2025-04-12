using DesafioBtg.Dominio.ItensPedidos.Servicos.Comandos;

namespace DesafioBtg.DataTransfer.Pedidos.Requests.Requests;

public class PedidoRequest
{
    public int IdCliente { get; set; }
    IEnumerable<ItemPedidoComando> itens { get; set; }
}
