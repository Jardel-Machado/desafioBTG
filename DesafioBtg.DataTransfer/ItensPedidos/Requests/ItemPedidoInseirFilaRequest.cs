using System.Text.Json.Serialization;

namespace DesafioBtg.DataTransfer.ItensPedidos.Requests;

public class ItemPedidoInseirFilaRequest
{
    [JsonPropertyName("produto")]
    public string Produto { get; set; }

    [JsonPropertyName("quantidade")]
    public int Quantidade { get; set; }

    [JsonPropertyName("preco")]
    public decimal Preco { get; set; }
}
