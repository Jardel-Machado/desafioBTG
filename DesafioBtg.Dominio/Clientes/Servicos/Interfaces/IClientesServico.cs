using DesafioBtg.Dominio.Clientes.Consultas;
using DesafioBtg.Dominio.Clientes.Entidades;
using DesafioBtg.Dominio.Clientes.Servicos.Comandos;

namespace DesafioBtg.Dominio.Clientes.Servicos.Interfaces;

public interface IClientesServico
{
    Task<Cliente> ValidarAsync(int id, CancellationToken cancellationToken);
    Task<Cliente> InserirAsync(ClienteComando comando, CancellationToken cancellationToken);
    Cliente? ValidarPorCodigoCliente(int codigoCliente, CancellationToken cancellationToken);
    ClienteQuantidadePedidoConsulta QuantidadeDePedidodPorcliente(int codigoCliente);
    IEnumerable<ClientePedidoRealizadoConsulta> QuantidadeDePedidosRealizadosPorCliente(int codigoCliente);
}
