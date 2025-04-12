using DesafioBtg.Dominio.Uteis;
using DesafioBtg.Dominio.Uteis.Enumeradores;

namespace DesafioBtg.DataTransfer.Clientes.Requests;

public class ClienteListarRequest : PaginacaoFiltro
{
    public int? Id { get; set; }
    public string Nome { get; set; } = string.Empty;

    public ClienteListarRequest() : base(cpOrd: "Nome", tpOrd: TipoOrdenacaoEnum.Asc) { }
}
