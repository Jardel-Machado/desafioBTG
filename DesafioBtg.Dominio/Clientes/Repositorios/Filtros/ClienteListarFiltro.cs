using DesafioBtg.Dominio.Uteis;
using DesafioBtg.Dominio.Uteis.Enumeradores;

namespace DesafioBtg.Dominio.Clientes.Repositorios.Filtros;

public class ClienteListarFiltro : PaginacaoFiltro
{
    public int? Id { get; set; }
    public string Nome { get; set; } = string.Empty;

    public ClienteListarFiltro() : base(cpOrd: "Nome", tpOrd: TipoOrdenacaoEnum.Asc) { }
}
