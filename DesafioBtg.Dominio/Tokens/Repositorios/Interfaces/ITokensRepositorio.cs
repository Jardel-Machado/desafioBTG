using DesafioBtg.Dominio.Usuarios.Entidades;

namespace DesafioBtg.Dominio.Tokens.Repositorios.Interfaces;

public interface ITokensRepositorio
{
    Task<string> GerarTokenAsync(Usuario usuario);
    Task<string> GerarRefreshTokenAsync(Usuario usuario);
    Task<string> GerarTokenRecuperacaoSenhaAsync(Usuario usuario);
}
