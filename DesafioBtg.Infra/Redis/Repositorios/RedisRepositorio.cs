using DesafioBtg.Dominio.Redis.Repositorios.Interfaces;
using Microsoft.Extensions.Configuration;
using StackExchange.Redis;
using System.Text.Json;

namespace DesafioBtg.Infra.Redis.Repositorios;
public class RedisRepositorio : IRedisRepositorio
{
    private readonly IDatabase redisDatabase;
    private readonly IConfiguration configuration;
    private readonly string NomeSessao;

    public RedisRepositorio(IConnectionMultiplexer redis, IConfiguration configuration)
    {
        this.redisDatabase = redis.GetDatabase();
        this.configuration = configuration;
        this.NomeSessao = configuration.GetValue<string>("Redis:NomeSessao") ?? "Sessao-Default";
    }

    public void CachearDados<T>(string identificadorUnico, T dados, TimeSpan? expiraEm)
    {
        redisDatabase.StringSet($"{NomeSessao}:{identificadorUnico}", JsonSerializer.Serialize(dados), expiraEm);
    }

    public string CachearDados<T>(T dados, TimeSpan? expiraEm)
    {
        string identificadorUnico = Guid.NewGuid().ToString();

        redisDatabase.StringSet($"{NomeSessao}:{identificadorUnico}", JsonSerializer.Serialize(dados), expiraEm);

        return identificadorUnico;
    }

    public async Task<string> CachearDadosAsync<T>(T dados, TimeSpan? expiraEm)
    {
        string identificadorUnico = Guid.NewGuid().ToString();

        await redisDatabase.StringSetAsync($"{NomeSessao}:{identificadorUnico}", JsonSerializer.Serialize(dados), expiraEm);

        return identificadorUnico;
    }

    public async Task CachearDadosAsync<T>(string identificadorUnico, T dados, TimeSpan? expiraEm)
    {
        await redisDatabase.StringSetAsync($"{NomeSessao}:{identificadorUnico}", JsonSerializer.Serialize(dados), expiraEm);
    }

    public void DeletarDados(string identificadorUnico)
    {
        redisDatabase.KeyDelete($"{NomeSessao}:{identificadorUnico}");
    }

    public async Task DeletarDadosAsync(string identificadorUnico)
    {
        await redisDatabase.KeyDeleteAsync($"{NomeSessao}:{identificadorUnico}");
    }

    public T ObterDados<T>(string identificadorUnico)
    {
        var dadosString = redisDatabase.StringGet($"{NomeSessao}:{identificadorUnico}");

        return string.IsNullOrWhiteSpace(dadosString) ? default : JsonSerializer.Deserialize<T>(dadosString);
    }

    public async Task<T> ObterDadosAsync<T>(string identificadorUnico)
    {
        var dadosString = await redisDatabase.StringGetAsync($"{NomeSessao}:{identificadorUnico}");

        return string.IsNullOrWhiteSpace(dadosString) ? default : JsonSerializer.Deserialize<T>(dadosString);
    }

    public async Task<bool> CriaBloqueioAsync(string chaveBloqueio, TimeSpan expiraEm)
    {
        return await redisDatabase.StringSetAsync($"{NomeSessao}:{chaveBloqueio}", "bloqueio", expiraEm, When.NotExists);
    }

    public async Task RemoveBloqueioAsync(string chaveBloqueio)
    {
        await redisDatabase.KeyDeleteAsync($"{NomeSessao}:{chaveBloqueio}");
    }

    public async Task<bool> DefinirSinalizadorDeCriacaoAsync(string identificadorUnico, TimeSpan expiraEm)
    {
        return await redisDatabase.StringSetAsync($"{NomeSessao}:{identificadorUnico}", "criando", expiraEm, When.NotExists);
    }

    public async Task<T> RetornaPrimeiroItemDaListaDepoisRemoveAsync<T>(string identificadorUnico)
    {
        var item = await redisDatabase.ListLeftPopAsync($"{NomeSessao}:{identificadorUnico}");

        return item.HasValue ? JsonSerializer.Deserialize<T>(item.ToString()) : default;
    }

    public async Task AdicionarNaListaAsync<T>(string identificadorUnico, T item, TimeSpan expiraEm)
    {
        RedisValue redisValue = JsonSerializer.Serialize(item);

        await redisDatabase.ListRightPushAsync($"{NomeSessao}:{identificadorUnico}", redisValue);

        await redisDatabase.KeyExpireAsync($"{NomeSessao}:{identificadorUnico}", expiraEm);
    }

    public async Task<int> ObterTamanhoListaAsync(string identificadorUnico)
    {
        return (int)await redisDatabase.ListLengthAsync($"{NomeSessao}:{identificadorUnico}");
    }

    public async Task<bool> VerificarExistenciaNaListaAsync(string identificadorUnico, string protocolo)
    {
        RedisValue[] lista = await redisDatabase.ListRangeAsync($"{NomeSessao}:{identificadorUnico}");

        return lista.Any(item => item.ToString().Contains(protocolo));
    }

    public async Task AdicionarMultiplosNaListaAsync<T>(string identificadorUnico, List<T> itens, TimeSpan expiraEm)
    {
        RedisValue[] redisValues = itens
                          .Select(item => (RedisValue)JsonSerializer.Serialize(item))
                          .ToArray();

        await redisDatabase.ListRightPushAsync($"{NomeSessao}:{identificadorUnico}", redisValues);

        await redisDatabase.KeyExpireAsync($"{NomeSessao}:{identificadorUnico}", expiraEm);
    }

    public async Task<bool> ExisteChaveAsync(string identificadorUnico)
    {
        return await redisDatabase.KeyExistsAsync($"{NomeSessao}:{identificadorUnico}");
    }

    public async Task<List<T>> ObterTodosItensListaAsync<T>(string identificadorUnico) where T : class
    {
        RedisValue[] redisValues = await redisDatabase.ListRangeAsync($"{NomeSessao}:{identificadorUnico}");

        return redisValues
            .Select(value => JsonSerializer.Deserialize<T>(value.ToString()))
            .Where(item => item != null)
            .ToList();
    }

    public async Task<string> ObterChaveJwtAsync()
    {
        return await redisDatabase.StringGetAsync($"{NomeSessao}:JwtKey");
    }

    public async Task<string> ObterAesEncryptionKeyAsync()
    {
        return await redisDatabase.StringGetAsync($"{NomeSessao}:AesEncryptionKey");
    }

    public async Task DefinirChaveJwtAsync(string chaveJwt)
    {
        await redisDatabase.StringSetAsync($"{NomeSessao}:JwtKey", chaveJwt);
    }

    public async Task DefinirAesEncryptionKeyAsync(string aesKey)
    {
        await redisDatabase.StringSetAsync($"{NomeSessao}:AesEncryptionKey", aesKey);
    }
}
