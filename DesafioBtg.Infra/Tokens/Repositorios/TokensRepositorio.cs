using DesafioBtg.Dominio.Redis.Repositorios.Interfaces;
using DesafioBtg.Dominio.Tokens.Repositorios.Interfaces;
using DesafioBtg.Dominio.Usuarios.Entidades;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DesafioBtg.Infra.Tokens.Repositorios;

public class TokensRepositorio : ITokensRepositorio
{
    private readonly IConfiguration configuration;
    private readonly IRedisRepositorio redisRepositorio;

    public TokensRepositorio(IConfiguration configuration, IRedisRepositorio redisRepositorio)
    {
        this.configuration = configuration;
        this.redisRepositorio = redisRepositorio;
    }

    public async Task<string> GerarTokenAsync(Usuario usuario)
    {       
        var key = await redisRepositorio.ObterChaveJwtAsync();

        var issuer = configuration["Jwt:Issuer"];

        var audience = configuration["Jwt:Audience"];

        var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));

        var credentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha512);

        List<Claim> claims =
        [
            new Claim(type: ClaimTypes.Name, usuario.NomeCompleto),
            new Claim(type: ClaimTypes.NameIdentifier, usuario.NomeUsuario),
            new Claim(type: ClaimTypes.Email, usuario.Email),
            new Claim("IdUsuario", usuario.Id.ToString())            
        ];       

        JwtSecurityToken tokenOptions = new
                (
                    issuer: issuer,
                    audience: audience,
                    expires: DateTime.Now.AddMinutes(10),
                    signingCredentials: credentials,
                    claims: claims
                );

        string token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

        return token;
    }

    public async Task<string> GerarRefreshTokenAsync(Usuario usuario)
    {
        var key = await redisRepositorio.ObterChaveJwtAsync();

        var issuer = configuration["Jwt:Issuer"];

        var audience = configuration["Jwt:Audience"];

        var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));

        var credentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha512);

        List<Claim> claims =
        [
            new Claim(type: ClaimTypes.Name, usuario.NomeCompleto),
            new Claim(type: ClaimTypes.NameIdentifier, usuario.NomeUsuario),
            new Claim(type: ClaimTypes.Email, usuario.Email),
            new Claim("IdUsuario", usuario.Id.ToString()),            
        ];

        JwtSecurityToken tokenOptions = new
                (
                    issuer: issuer,
                    audience: audience,
                    expires: DateTime.Now.AddHours(2),
                    signingCredentials: credentials,
                    claims: claims
                );

        string token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

        return token;
    }

    public async Task<string> GerarTokenRecuperacaoSenhaAsync(Usuario usuario)
    {
        var key = await redisRepositorio.ObterChaveJwtAsync();

        var issuer = configuration["Jwt:Issuer"];

        var audience = configuration["Jwt:Audience"];

        var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));

        var credentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha512);

        List<Claim> claims =
        [
            new Claim(type: ClaimTypes.Email, usuario.Email),
            new Claim("TokenType", "resetPassword"),
            new Claim("IdUsuario", usuario.Id.ToString()),           
        ];

        JwtSecurityToken tokenOptions = new 
        (
            issuer: issuer,
            audience: audience,
            expires: DateTime.Now.AddMinutes(30),
            signingCredentials: credentials,
            claims: claims
        );

        var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

        return token;
    }
}
