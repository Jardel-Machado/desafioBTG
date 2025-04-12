namespace DesafioBtg.Dominio.Redis.Repositorios.Interfaces;

public interface IRedisRepositorio
{
    Task<string> CachearDadosAsync<T>(T dados, TimeSpan? expiraEm);
    Task CachearDadosAsync<T>(string identificadorUnico, T dados, TimeSpan? expiraEm);
    Task<T> ObterDadosAsync<T>(string identificadorUnico);
    Task DeletarDadosAsync(string identificadorUnico);
    Task<bool> CriaBloqueioAsync(string chaveBloqueio, TimeSpan expiraEm);
    Task RemoveBloqueioAsync(string chaveBloqueio);
    Task<bool> DefinirSinalizadorDeCriacaoAsync(string identificadorUnico, TimeSpan expiraEm);
    Task<T> RetornaPrimeiroItemDaListaDepoisRemoveAsync<T>(string identificadorUnico);
    Task AdicionarNaListaAsync<T>(string identificadorUnico, T item, TimeSpan expiraEm);
    Task<int> ObterTamanhoListaAsync(string identificadorUnico);
    Task<bool> VerificarExistenciaNaListaAsync(string identificadorUnico, string protocolo);
    Task AdicionarMultiplosNaListaAsync<T>(string identificadorUnico, List<T> itens, TimeSpan expiraEm);
    Task<bool> ExisteChaveAsync(string identificadorUnico);
    Task<List<T>> ObterTodosItensListaAsync<T>(string identificadorUnico) where T : class;
    Task<string> ObterChaveJwtAsync();
    Task<string> ObterAesEncryptionKeyAsync();
    Task DefinirChaveJwtAsync(string chaveJwt);
    Task DefinirAesEncryptionKeyAsync(string aesKey);
    void CachearDados<T>(string identificadorUnico, T dados, TimeSpan? expiraEm);
    string CachearDados<T>(T dados, TimeSpan? expiraEm);
    T ObterDados<T>(string identificadorUnico);
    void DeletarDados(string identificadorUnico);
}
