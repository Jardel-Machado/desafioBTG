using DesafioBtg.Dominio.Pedidos.Entidades;

namespace DesafioBtg.DataTransfer.Clientes.Responses.Responses;

public class ClienteResponse
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public IEnumerable<Pedido> Pedidos { get; set; }
}
