using DesafioBtg.Dominio.Criptografias.Repositorios.Interfaces;
using DesafioBtg.Dominio.Redis.Repositorios.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Security.Cryptography;

namespace DesafioBtg.Infra.Criptografias.Repositorios;

public class CriptografiasRepositorio : ICriptografiasRepositorio
{
    private readonly byte[] chave;
    private readonly byte[] iv;

    public CriptografiasRepositorio(IRedisRepositorio redisRepositorio, IConfiguration configuration)
    {
        string chaveBase64 = redisRepositorio.ObterAesEncryptionKeyAsync().GetAwaiter().GetResult();

        if (string.IsNullOrEmpty(chaveBase64))
        {
            chaveBase64 = configuration["AesEncryptionKey"];

            redisRepositorio.DefinirAesEncryptionKeyAsync(chaveBase64);
        }

        chave = Convert.FromBase64String(chaveBase64);

        iv = new byte[16];
    }    

    public string Criptografar(string textoPlano)
    {
        using var aes = Aes.Create();

        aes.Key = chave;

        aes.IV = iv;

        using var encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

        using var ms = new MemoryStream();

        using (var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))

        using (var sw = new StreamWriter(cs))
        {
            sw.Write(textoPlano);
        }

        return Convert.ToBase64String(ms.ToArray());
    }

    public string Descriptografar(string textoCifrado)
    {
        var buffer = Convert.FromBase64String(textoCifrado);

        using var aes = Aes.Create();

        aes.Key = chave;

        aes.IV = iv;

        using var decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

        using var ms = new MemoryStream(buffer);

        using var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read);

        using var sr = new StreamReader(cs);

        return sr.ReadToEnd();
    }
}
