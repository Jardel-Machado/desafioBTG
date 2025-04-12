using DesafioBtg.Dominio.Clientes.Consultas;
using DesafioBtg.Dominio.Clientes.Entidades;
using DesafioBtg.Dominio.Clientes.Repositorios.Interfaces;
using DesafioBtg.Dominio.Clientes.Servicos.Comandos;
using DesafioBtg.Dominio.Clientes.Servicos.Interfaces;
using DesafioBtg.Dominio.Excecoes;
using DesafioBtg.Dominio.Pedidos.Repositorios.Interfaces;

namespace DesafioBtg.Dominio.Clientes.Servicos;

public class ClientesServico : IClientesServico
{
    private readonly IClientesRepositorio clientesRepositorio;
    private readonly IPedidosRepositorio pedidosRepositorio;

    public ClientesServico(IClientesRepositorio clientesRepositorio, IPedidosRepositorio pedidosRepositorio)
    {
        this.clientesRepositorio = clientesRepositorio;
        this.pedidosRepositorio = pedidosRepositorio;
    }

    public async Task<Cliente> ValidarAsync(int id, CancellationToken cancellationToken)
    {
        Cliente cliente = await clientesRepositorio.RecuperarAsync(id, cancellationToken);

        return cliente is null ? throw new AtributoObrigatorioExcecao("Cliente") : cliente;
    }

    public Cliente? ValidarPorCodigoCliente(int codigoCliente, CancellationToken cancellationToken)
    {
        return clientesRepositorio.Query().FirstOrDefault(c => c.CodigoCliente == codigoCliente);
    }

    public async Task<Cliente> InserirAsync(ClienteComando comando, CancellationToken cancellationToken)
    {
        Cliente cliente = Instanciar(comando);

        await clientesRepositorio.InserirAsync(cliente, cancellationToken);

        return cliente;
    }

    public ClienteQuantidadePedidoConsulta QuantidadeDePedidodPorcliente(int codigoCliente)
    {
        var resultado = pedidosRepositorio.Query()
                                          .Where(p => p.Cliente.CodigoCliente == codigoCliente)
                                          .GroupBy(p => p.Cliente.CodigoCliente)
                                          .Select(g => new ClienteQuantidadePedidoConsulta
                                          {
                                            CodigoCliente = g.Key,
                                            QuantidadePedido = g.Count()
                                          })
                                          .FirstOrDefault();

        return resultado is null ? throw new RegraDeNegocioExcecao("Cliente não possui pedidos.") : resultado;
    }

    public IEnumerable<ClientePedidoRealizadoConsulta> QuantidadeDePedidosRealizadosPorCliente(int codigoCliente)
    {
        var resultado = pedidosRepositorio.Query()
                                          .Where(p => p.Cliente.CodigoCliente == codigoCliente)
                                          .Select(p => new ClientePedidoRealizadoConsulta
                                          {
                                              CodigoPedido = p.CodigoPedido,
                                              DataPedido = p.DataPedido,
                                              ValorTotal = p.Itens.Sum(i => i.Preco * i.Quantidade)
                                          });

        return resultado is null ? throw new RegraDeNegocioExcecao("Cliente não possui pedidos.") : resultado;
    }

    private static Cliente Instanciar(ClienteComando comando)
    {
        return new Cliente(comando.CodigoCliente);
    }
}
