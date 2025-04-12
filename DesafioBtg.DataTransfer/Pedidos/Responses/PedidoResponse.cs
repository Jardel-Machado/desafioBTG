namespace DesafioBtg.DataTransfer.Pedidos.Responses.Responses;

public class PedidoResponse
{
    public int Id { get; set; }
    public int IdCliente { get; set; }
    public string NomeCliente { get; set; }
    public string Produto { get; set; }
    public int Quantidade { get; set; }
    public decimal Preco { get; set; }
    public DateTime DataPedido { get; set; }
}
