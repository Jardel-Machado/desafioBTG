using DesafioBtg.Dominio.Clientes.Repositorios.Interfaces;
using DesafioBtg.Infra.Genericos;
using DesafioBtg.Dominio.Clientes.Entidades;
using NHibernate;
using DesafioBtg.Dominio.Clientes.Repositorios.Filtros;

namespace DesafioBtg.Infra.Clientes.Repositorios;

public class ClientesRepositorio : GenericosRepositorio<Cliente>, IClientesRepositorio
{
    public ClientesRepositorio(ISession session) : base(session) { }

    public IQueryable<Cliente> Filtrar(ClienteListarFiltro filtro)
    {
        IQueryable<Cliente> query = Query();

        if (filtro.Id.HasValue)
            query = query.Where(c => c.Id == filtro.Id.Value);

        if (!string.IsNullOrWhiteSpace(filtro.Nome))
            query = query.Where(c => c.Nome.Contains(filtro.Nome, StringComparison.OrdinalIgnoreCase));

        return query;
    }
}
