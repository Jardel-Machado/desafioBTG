using DesafioBtg.Dominio.Uteis;
using DesafioBtg.Dominio.Uteis.Enumeradores;

namespace DesafioBtg.Dominio.ItensPedidos.Repositorios.Filtros;

public class ItemPedidoListarFiltro : PaginacaoFiltro
{
    public int? Id { get; set; }
    public string Produto { get; set; }

    public ItemPedidoListarFiltro() : base(cpOrd: "", tpOrd: TipoOrdenacaoEnum.Asc) { }
}
