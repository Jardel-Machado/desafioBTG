using DesafioBtg.Dominio.Pedidos.Repositorios.Interfaces;
using DesafioBtg.Infra.Genericos;
using DesafioBtg.Dominio.Pedidos.Entidades;
using NHibernate;
using DesafioBtg.Dominio.Pedidos.Repositorios.Filtros;

namespace DesafioBtg.Infra.Pedidos.Repositorios;

public class PedidosRepositorio : GenericosRepositorio<Pedido>, IPedidosRepositorio
{
    public PedidosRepositorio(ISession session) : base(session) { }

    public IQueryable<Pedido> Filtrar(PedidoListarFiltro filtro)
    {
        IQueryable<Pedido> query = Query();

        if (filtro.Id.HasValue)
            query = query.Where(p => p.Id == filtro.Id.Value);

        if (filtro.IdCliente.HasValue)
            query = query.Where(p => p.Cliente.Id == filtro.IdCliente.Value);

        if (!string.IsNullOrWhiteSpace(filtro.NomeCliente))
            query = query.Where(p => p.Cliente.Nome.Contains(filtro.NomeCliente, StringComparison.OrdinalIgnoreCase));

        if (filtro.DataPedido.HasValue)
            query = query.Where(p => p.DataPedido.Date == filtro.DataPedido.Value.Date);

        if (!string.IsNullOrWhiteSpace(filtro.Produto))
            query = query.Where(p => p.Itens.Any(i => i.Produto.Contains(filtro.Produto, StringComparison.OrdinalIgnoreCase)));

        return query;
    }
}
