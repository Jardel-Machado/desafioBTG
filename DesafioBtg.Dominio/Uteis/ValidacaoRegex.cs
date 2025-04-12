using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;

namespace DesafioBtg.Dominio.Uteis;

[ExcludeFromCodeCoverage]
public static partial class ValidacaoRegex
{
    [GeneratedRegex(@"^(?:\d{3}[.-]?){2}\d{3}-?\d{2}$")]
    public static partial Regex Cpf();

    [GeneratedRegex(@"^(\d{2}\.\d{3}\.\d{3}\/\d{4}\-\d{2}|\d{14})$")]
    public static partial Regex Cnpj();

    [GeneratedRegex(@"^((\d{3}[.-]?){2}\d{3}-?\d{2}|(\d{2}\.\d{3}\.\d{3}/\d{4}-\d{2}|\d{14}))$")]
    public static partial Regex Documento();

    [GeneratedRegex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$")]
    public static partial Regex Email();

    [GeneratedRegex(@"^\d{4,9}-?[A-Za-z]{2}$")]
    public static partial Regex Crm();

    [GeneratedRegex(@"^\d{5}-?\d{3}$")]
    public static partial Regex Cep();

    [GeneratedRegex(@"^(?:1[1-9]|2[1-9]|3[0-9]|4[1-9]|5[1-9]|6[1-9]|7[1-9]|8[1-9]|9[1-9])$")]
    public static partial Regex Ddd();
    
    [GeneratedRegex(@"^(\d{2})?(\d{4,5})-?(\d{4})$")]
    public static partial Regex Telefone();

    [GeneratedRegex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[\W]).+$")]
    public static partial Regex Senha();
}
