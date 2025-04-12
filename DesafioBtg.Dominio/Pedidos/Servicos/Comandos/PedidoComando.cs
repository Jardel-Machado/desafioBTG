using DesafioBtg.Dominio.ItensPedidos.Servicos.Comandos;

namespace DesafioBtg.Dominio.Pedidos.Servicos.Comandos;

public class PedidoComando
{
    public int CodigoPedido { get; set; }
    public int CodigoCliente { get; set; }
    public List<ItemPedidoComando> Itens { get; set; }
}
