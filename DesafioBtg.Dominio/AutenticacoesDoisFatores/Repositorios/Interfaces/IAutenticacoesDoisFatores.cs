namespace DesafioBtg.Dominio.AutenticacoesDoisFatores.Repositorios.Interfaces;

public interface IAutenticacoesDoisFatores
{
    string GerarChaveSecreta();
    string GerarQrCodeUri(string email, string chaveSecreta);
    string GerarQrCode(string uri);
    bool ValidarCodigo(string chaveSecreta, string codigo);
}
