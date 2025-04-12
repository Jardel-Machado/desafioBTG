using DesafioBtg.Dominio.Excecoes;
using DesafioBtg.Dominio.Uteis;

namespace DesafioBtg.Dominio.Usuarios.Entidades;
public class Usuario
{
    public virtual int Id { get; protected set; }
    public virtual string NomeCompleto { get; protected set; }
    public virtual string NomeUsuario { get; protected set; }
    public virtual string Email { get; protected set; }    

    protected Usuario() { }

    public Usuario(string nomeCompleto, string nomeUsuario, string email)
    {
        SetNomeCompleto(nomeCompleto);
        SetNomeUsuario(nomeUsuario);        
        SetEmail(email);
    }

    public virtual void SetNomeCompleto(string nomeCompleto)
    {
        if (string.IsNullOrWhiteSpace(nomeCompleto))
            throw new AtributoObrigatorioExcecao("Nome");

        nomeCompleto = nomeCompleto.Trim();

        if (nomeCompleto.Length > 100)
            throw new TamanhoDeAtributoInvalidoExcecao("Nome", 1, 100);

        NomeCompleto = nomeCompleto;
    }

    public virtual void SetNomeUsuario(string nomeUsuario)
    {
        if (string.IsNullOrWhiteSpace(nomeUsuario))
            throw new AtributoObrigatorioExcecao("Nome de Usuario");

        nomeUsuario = nomeUsuario.Trim();

        if (nomeUsuario.Length > 100)
            throw new TamanhoDeAtributoInvalidoExcecao("Nome de Usuario", 1, 100);

        NomeUsuario = nomeUsuario;
    }

    public virtual void SetEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            throw new AtributoObrigatorioExcecao("Email");

        email = email.Trim().ToLower();

        if (email.Length > 100)
            throw new TamanhoDeAtributoInvalidoExcecao("Email", 1, 100);

        if (!ValidacaoRegex.Email().IsMatch(email))
            throw new RegraDeNegocioExcecao("Email inválido");

        Email = email;
    }  
}
