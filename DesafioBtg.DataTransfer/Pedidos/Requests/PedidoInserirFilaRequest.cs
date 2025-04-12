using DesafioBtg.DataTransfer.ItensPedidos.Requests;
using System.Text.Json.Serialization;

namespace DesafioBtg.DataTransfer.Pedidos.Requests;

public class PedidoInserirFilaRequest
{
    [JsonPropertyName("codigoPedido")]
    public int CodigoPedido { get; set; }

    [JsonPropertyName("codigoCliente")]
    public int CodigoCliente { get; set; }

    [JsonPropertyName("itens")]
    public IEnumerable<ItemPedidoInseirFilaRequest> Itens { get; set; }
}
