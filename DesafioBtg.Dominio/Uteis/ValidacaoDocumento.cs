using System.Diagnostics.CodeAnalysis;

namespace DesafioBtg.Dominio.Uteis;

[ExcludeFromCodeCoverage]
public static class ValidacaoDocumento
{
    public static bool ValidarCPF(string cpf)
    {
        cpf = new string(cpf.Where(char.IsDigit).ToArray());

        if (cpf.Length != 11 || cpf.Distinct().Count() == 1)
            return false;

        int soma = 0;

        for (int i = 0; i < 9; i++)
            soma += (cpf[i] - '0') * (10 - i);

        int resto = soma % 11;

        int primeiroDigitoVerificador = (resto < 2) ? 0 : 11 - resto;

        if (cpf[9] - '0' != primeiroDigitoVerificador)
            return false;

        soma = 0;

        for (int i = 0; i < 10; i++)
            soma += (cpf[i] - '0') * (11 - i);

        resto = soma % 11;

        int segundoDigitoVerificador = (resto < 2) ? 0 : 11 - resto;

        return cpf[10] - '0' == segundoDigitoVerificador;
    }

    public static bool ValidarCNPJ(string cnpj)
    {
        cnpj = new string(cnpj.Where(char.IsDigit).ToArray());

        if (cnpj.Length != 14 || cnpj.Distinct().Count() == 1)
            return false;

        int[] multiplicador1 = { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };

        int[] multiplicador2 = { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };

        int soma = 0;

        for (int i = 0; i < 12; i++)
            soma += (cnpj[i] - '0') * multiplicador1[i];

        int resto = soma % 11;

        int primeiroDigitoVerificador = (resto < 2) ? 0 : 11 - resto;

        if (cnpj[12] - '0' != primeiroDigitoVerificador)
            return false;

        soma = 0;

        for (int i = 0; i < 13; i++)
            soma += (cnpj[i] - '0') * multiplicador2[i];

        resto = soma % 11;

        int segundoDigitoVerificador = (resto < 2) ? 0 : 11 - resto;

        return cnpj[13] - '0' == segundoDigitoVerificador;
    }
}
