using DesafioBtg.Dominio.Excecoes;
using DesafioBtg.Dominio.ItensPedidos.Entidades;
using DesafioBtg.Dominio.ItensPedidos.Repositorios.Interfaces;
using DesafioBtg.Dominio.ItensPedidos.Servicos.Interfaces;
using DesafioBtg.Dominio.Pedidos.Servicos.Interfaces;

namespace DesafioBtg.Dominio.ItensPedidos.Servicos;

public class ItensPedidosServico : IItensPedidosServico
{
    private readonly IItensPedidosRepositorio itensPedidosRepositorio;
    private readonly IPedidosServico pedidosServico;

    public ItensPedidosServico(IItensPedidosRepositorio itensPedidosRepositorio, IPedidosServico pedidosServico)
    {
        this.itensPedidosRepositorio = itensPedidosRepositorio;
        this.pedidosServico = pedidosServico;
    }

    public async Task<ItemPedido> ValidarAsync(int id, CancellationToken cancellationToken)
    {
        ItemPedido itemPedido = await itensPedidosRepositorio.RecuperarAsync(id, cancellationToken);

        return itemPedido is null ? throw new AtributoObrigatorioExcecao("ItemPedido") : itemPedido;
    }    
}
