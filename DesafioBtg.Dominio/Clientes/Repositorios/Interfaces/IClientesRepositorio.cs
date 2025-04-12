using DesafioBtg.Dominio.Clientes.Entidades;
using DesafioBtg.Dominio.Clientes.Repositorios.Filtros;
using DesafioBtg.Dominio.Genericos.Interfaces;

namespace DesafioBtg.Dominio.Clientes.Repositorios.Interfaces;

public interface IClientesRepositorio : IGenericosRepositorio<Cliente>
{
    IQueryable<Cliente> Filtrar(ClienteListarFiltro filtro);
}
