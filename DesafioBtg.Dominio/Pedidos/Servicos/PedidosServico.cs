using DesafioBtg.Dominio.Clientes.Entidades;
using DesafioBtg.Dominio.Clientes.Servicos.Comandos;
using DesafioBtg.Dominio.Clientes.Servicos.Interfaces;
using DesafioBtg.Dominio.Excecoes;
using DesafioBtg.Dominio.ItensPedidos.Repositorios.Interfaces;
using DesafioBtg.Dominio.ItensPedidos.Servicos.Comandos;
using DesafioBtg.Dominio.Pedidos.Consultas;
using DesafioBtg.Dominio.Pedidos.Entidades;
using DesafioBtg.Dominio.Pedidos.Repositorios.Interfaces;
using DesafioBtg.Dominio.Pedidos.Servicos.Comandos;
using DesafioBtg.Dominio.Pedidos.Servicos.Interfaces;

namespace DesafioBtg.Dominio.Pedidos.Servicos;

public class PedidosServico : IPedidosServico
{
    private readonly IPedidosRepositorio pedidosRepositorio;
    private readonly IClientesServico clientesServico;
    private readonly IItensPedidosRepositorio itensPedidosRepositorio;

    public PedidosServico(IPedidosRepositorio pedidosRepositorio, IClientesServico clientesServico, IItensPedidosRepositorio itensPedidosRepositorio)
    {
        this.pedidosRepositorio = pedidosRepositorio;
        this.clientesServico = clientesServico;
        this.itensPedidosRepositorio = itensPedidosRepositorio;
    }

    public async Task<Pedido> ValidarAsync(int id, CancellationToken cancellationToken)
    {
        Pedido pedido = await pedidosRepositorio.RecuperarAsync(id, cancellationToken);

        return pedido is null ? throw new AtributoObrigatorioExcecao("Pedido") : pedido;
    }

    public async Task<Pedido> InserirAsync(PedidoComando comando, CancellationToken cancellationToken)
    {
        Cliente cliente = clientesServico.ValidarPorCodigoCliente(comando.CodigoCliente, cancellationToken);

        if (cliente is null)
        {
            ClienteComando clienteComando = new()
            {
                CodigoCliente = comando.CodigoCliente
            };

            cliente = await clientesServico.InserirAsync(clienteComando, cancellationToken); 
        }

        Pedido pedido = Instanciar(cliente, comando.CodigoPedido);

        AdicionarItens(comando.Itens, pedido);

        await pedidosRepositorio.InserirAsync(pedido, cancellationToken);

        return pedido;
    }

    public PedidoValorTotalConsulta BuscarValorTotalPedido(int codigoPedido)
    {
        var valorTotal = itensPedidosRepositorio.Query()
                            .Where(x => x.Pedido.CodigoPedido == codigoPedido)
                            .GroupBy(i => i.Pedido.CodigoPedido)
                            .Select(g => new PedidoValorTotalConsulta
                            {
                                CodigoPedido = g.Key,
                                ValorTotal = g.Sum(i => i.Preco * i.Quantidade)
                            }).FirstOrDefault();

        if (valorTotal is null)
            throw new RegraDeNegocioExcecao("Pedido não encontrado.");

        return valorTotal;
    }

    private static void AdicionarItens(IEnumerable<ItemPedidoComando> itens, Pedido pedido)
    {
        if (pedido is null)
            throw new AtributoObrigatorioExcecao("Pedido");

        foreach (ItemPedidoComando item in itens)
        {
            pedido.AdicionarItem(item.Produto, item.Quantidade, item.Preco);
        }
    }


    private static Pedido Instanciar(Cliente cliente, int codigoPedido)
    {
        return new Pedido(cliente, codigoPedido);
    }
}
