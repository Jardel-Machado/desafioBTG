namespace DesafioBtg.DataTransfer.Clientes.Responses;

public class ClientePedidoRealizadoResponse
{
    public int CodigoPedido { get; set; }
    public DateTime DataPedido { get; set; }
    public decimal ValorTotal { get; set; }
}
