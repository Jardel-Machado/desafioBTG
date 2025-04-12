using DesafioBtg.Dominio.Excecoes;
using DesafioBtg.Dominio.BCrypts.Repositorios.Interfaces;
using DesafioBtg.Dominio.Uteis;

namespace DesafioBtg.Infra.BCrypts.Repositorios;

public class BCryptRepositorio : IBCryptRepositorio
{
    public bool CompararSenha(string senhaInformada, string senhaArmazenada)
    {
        return BCrypt.Net.BCrypt.EnhancedVerify(senhaInformada, senhaArmazenada);
    }

    public string CriptografarSenha(string senha)
    {
        return BCrypt.Net.BCrypt.EnhancedHashPassword(senha);
    }

    public void ValidaSenha(string senha)
    {
        if (string.IsNullOrWhiteSpace(senha))
            throw new AtributoObrigatorioExcecao("Senha");

        if (senha.Length < 8 || senha.Length > 255)
            throw new TamanhoDeAtributoInvalidoExcecao("Senha", 8, 255);

        if (!ValidacaoRegex.Senha().IsMatch(senha))
            throw new RegraDeNegocioExcecao("A senha deve conter pelo menos 8 caracteres e incluir pelo menos um número, uma letra maiúscula, uma letra minúscula e um caractere especial.");
    }

    public string GerarSenhaValida()
    {
        const int tamanho = 12;

        const string letrasMaiusculas = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

        const string letrasMinusculas = "abcdefghijklmnopqrstuvwxyz";

        const string numeros = "0123456789";

        const string caracteresEspeciais = "!@#$%^&*()-_=+[]{}|;:'\",.<>?/";

        Random random = new();

        string senha = new(
        [
            letrasMaiusculas[random.Next(letrasMaiusculas.Length)],
            letrasMinusculas[random.Next(letrasMinusculas.Length)],
            numeros[random.Next(numeros.Length)],
            caracteresEspeciais[random.Next(caracteresEspeciais.Length)]
        ]);

        string todosCaracteres = letrasMaiusculas + letrasMinusculas + numeros + caracteresEspeciais;

        senha += new string(Enumerable.Repeat(todosCaracteres, tamanho - 4).Select(s => s[random.Next(s.Length)]).ToArray());

        senha = new string([.. senha.OrderBy(_ => random.Next())]);

        return senha;
    }
}
