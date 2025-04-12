using DesafioBtg.Dominio.ItensPedidos.Repositorios.Interfaces;
using DesafioBtg.Infra.Genericos;
using DesafioBtg.Dominio.ItensPedidos.Entidades;
using NHibernate;
using DesafioBtg.Dominio.ItensPedidos.Repositorios.Filtros;

namespace DesafioBtg.Infra.ItensPedidos.Repositorios;

public class ItensPedidosRepositorio : GenericosRepositorio<ItemPedido>, IItensPedidosRepositorio
{
    public ItensPedidosRepositorio(ISession session) : base(session) { }

    public IQueryable<ItemPedido> Filtrar(ItemPedidoListarFiltro filtro, CancellationToken cancellationToken)
    {
        IQueryable<ItemPedido> query = Query();

        if (filtro.Id.HasValue)
            query = query.Where(i => i.Id == filtro.Id.Value);

        if(string.IsNullOrWhiteSpace(filtro.Produto))
            query = query.Where(i => i.Produto.Contains(filtro.Produto, StringComparison.OrdinalIgnoreCase));

        return query;
    }
}
