using DesafioBtg.Dominio.Excecoes;
using DesafioBtg.Dominio.Pedidos.Entidades;

namespace DesafioBtg.Dominio.ItensPedidos.Entidades;

public class ItemPedido
{
    public virtual int Id { get; protected set; }
    public virtual string Produto { get; protected set; }
    public virtual int Quantidade { get; protected set; }
    public virtual decimal Preco { get; protected set; }
    public virtual Pedido Pedido { get; protected set; }

    protected ItemPedido(){}

    public ItemPedido(string produto, int quantidade, decimal preco, Pedido pedido)
    {
        SetProduto(produto);
        SetQuantidade(quantidade);
        SetPreco(preco);
        SetPedido(pedido);
    }

    public virtual void SetProduto(string produto)
    {
        if (string.IsNullOrWhiteSpace(produto))
            throw new AtributoObrigatorioExcecao("Produto");

        produto = produto.Trim();

        if (produto.Length > 100)
            throw new TamanhoDeAtributoInvalidoExcecao("Produto", 1, 100);

        Produto = produto;
    }

    public virtual void SetQuantidade(int quantidade)
    {
        if (quantidade <= 0)
            throw new AtributoInvalidoExcecao("Quantidade");

        Quantidade = quantidade;
    }

    public virtual void SetPreco(decimal preco)
    {
        if (preco <= 0)
            throw new AtributoInvalidoExcecao("Preço");

        Preco = preco;
    }

    public virtual void SetPedido(Pedido pedido)
    {
        if (pedido is null)
            throw new AtributoObrigatorioExcecao("Pedido");

        Pedido = pedido;
    }
}
