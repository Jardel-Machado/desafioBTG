using DesafioBtg.Dominio.ItensPedidos.Entidades;
using DesafioBtg.Dominio.Genericos.Interfaces;
using DesafioBtg.Dominio.ItensPedidos.Repositorios.Filtros;

namespace DesafioBtg.Dominio.ItensPedidos.Repositorios.Interfaces;

public interface IItensPedidosRepositorio : IGenericosRepositorio<ItemPedido>
{
    IQueryable<ItemPedido> Filtrar(ItemPedidoListarFiltro filtro, CancellationToken cancellationToken);
}
