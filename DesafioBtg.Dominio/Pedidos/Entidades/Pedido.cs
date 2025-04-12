using DesafioBtg.Dominio.Clientes.Entidades;
using DesafioBtg.Dominio.Excecoes;
using DesafioBtg.Dominio.ItensPedidos.Entidades;

namespace DesafioBtg.Dominio.Pedidos.Entidades;

public class Pedido
{
    public virtual int Id { get; protected set; }
    public virtual Cliente Cliente { get; protected set; }
    public virtual int CodigoPedido { get; protected set; }
    public virtual DateTime DataPedido { get; protected set; }
    public virtual IList<ItemPedido> Itens { get; protected set; } = [];

    protected Pedido(){}

    public Pedido(Cliente cliente, int codigoPedido)
    {
        SetCliente(cliente);
        SetCodigoPedido(codigoPedido);
        DataPedido = DateTime.Now;
    }

    public virtual void SetCliente(Cliente cliente)
    {
        if (cliente is null)
            throw new AtributoObrigatorioExcecao("Cliente");

        Cliente = cliente;
    }

    public virtual void AdicionarItem(string produto, int quantidade, decimal preco)
    {
        ItemPedido item = new(produto, quantidade, preco, this);

        Itens.Add(item);
    }

    public virtual void SetCodigoPedido(int codigoPedido)
    {
        if (codigoPedido <= 0)
            throw new AtributoInvalidoExcecao("Código do pedido");

        CodigoPedido = codigoPedido;
    }
}
