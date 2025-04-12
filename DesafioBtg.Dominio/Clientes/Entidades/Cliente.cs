using DesafioBtg.Dominio.Excecoes;
using DesafioBtg.Dominio.Pedidos.Entidades;

namespace DesafioBtg.Dominio.Clientes.Entidades;

public class Cliente
{
    public virtual int Id { get; protected set; }
    public virtual string Nome { get; protected set; }
    public virtual int CodigoCliente { get; protected set; }
    public virtual IList<Pedido> Pedidos { get; protected set; } = [];

    protected Cliente() { }

    public Cliente(int codigoCliente)
    {
        SetCodigoCliente(codigoCliente);
    }

    public virtual void SetNome(string nome)
    {
        if (string.IsNullOrWhiteSpace(nome))
            throw new AtributoObrigatorioExcecao("Nome");

        nome = nome.Trim();

        if (nome.Length > 150)
            throw new TamanhoDeAtributoInvalidoExcecao("Nome", 1, 150);

        Nome = nome;
    }

    public virtual void SetCodigoCliente(int codigoCliente)
    {
        if (codigoCliente <= 0)
            throw new AtributoInvalidoExcecao("Código do cliente");

        CodigoCliente = codigoCliente;
    }
}
