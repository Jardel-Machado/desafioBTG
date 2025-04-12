namespace DesafioBtg.Dominio.BCrypts.Repositorios.Interfaces;

public interface IBCryptRepositorio
{
    string CriptografarSenha(string senha);
    bool CompararSenha(string senhaInformada, string senhaArmazenada);
    void ValidaSenha(string senha);
    string GerarSenhaValida();
}
