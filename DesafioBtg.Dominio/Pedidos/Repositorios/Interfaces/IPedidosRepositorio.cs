using DesafioBtg.Dominio.Pedidos.Entidades;
using DesafioBtg.Dominio.Genericos.Interfaces;
using DesafioBtg.Dominio.Pedidos.Repositorios.Filtros;

namespace DesafioBtg.Dominio.Pedidos.Repositorios.Interfaces;

public interface IPedidosRepositorio : IGenericosRepositorio<Pedido>
{
    IQueryable<Pedido> Filtrar(PedidoListarFiltro filtro);
}
