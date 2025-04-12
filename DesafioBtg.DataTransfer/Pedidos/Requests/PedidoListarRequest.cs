using DesafioBtg.Dominio.Uteis;
using DesafioBtg.Dominio.Uteis.Enumeradores;

namespace DesafioBtg.DataTransfer.Pedidos.Requests;

public class PedidoListarRequest : PaginacaoFiltro
{
    public int? Id { get; set; }
    public int? IdCliente { get; set; }
    public string NomeCliente { get; set; }
    public string Produto { get; set; }
    public DateTime? DataPedido { get; set; }

    public PedidoListarRequest() : base(cpOrd: "Id", tpOrd: TipoOrdenacaoEnum.Asc) { }
}
