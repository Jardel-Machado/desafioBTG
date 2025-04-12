namespace DesafioBtg.Dominio.Criptografias.Repositorios.Interfaces;

public interface ICriptografiasRepositorio
{
    string Criptografar(string textoPlano);
    string Descriptografar(string textoCifrado);
}
