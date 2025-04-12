using DesafioBtg.Dominio.Uteis;
using DesafioBtg.Dominio.Uteis.Enumeradores;

namespace DesafioBtg.DataTransfer.ItensPedidos.Requests;

public class ItemPedidoListarRequest : PaginacaoFiltro
{
    public ItemPedidoListarRequest() : base(cpOrd: "", tpOrd: TipoOrdenacaoEnum.Asc) { }
}
