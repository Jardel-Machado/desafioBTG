using DesafioBtg.Dominio.Uteis.Enumeradores;
using DesafioBtg.Dominio.Uteis;

namespace DesafioBtg.Dominio.Usuarios.Repositorios.Filtros;

public class UsuarioListarFiltro : PaginacaoFiltro
{
    public UsuarioListarFiltro() : base(cpOrd:"", tpOrd:TipoOrdenacaoEnum.Asc){}
}
