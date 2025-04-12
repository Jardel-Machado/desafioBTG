using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;
using System.Text;

namespace DesafioBtg.Dominio.Excecoes;

[ExcludeFromCodeCoverage]
[Serializable]

public class LimiteDeValorInvalidoExcecao : RegraDeNegocioExcecao
{
    public LimiteDeValorInvalidoExcecao(string atributo) : base(MontaMensagemErro(atributo, null, null)){}

    public LimiteDeValorInvalidoExcecao(string atributo, int? tamanhoMinimo, int? tamanhoMaximo) : base(MontaMensagemErro(atributo, tamanhoMinimo, tamanhoMaximo)){}

    private static string MontaMensagemErro(string atributo, int? tamanhoMinimo, int? tamanhoMaximo)
    {
        StringBuilder stringBuilder = new StringBuilder("Limite de valores do campo " + atributo + " inválido.");
        
        if (tamanhoMinimo.HasValue)
            stringBuilder.Append($" Valor mínimo: {tamanhoMinimo.Value}.");

        if (tamanhoMaximo.HasValue)        
            stringBuilder.Append($" Valor máximo: {tamanhoMaximo.Value}.");

        return stringBuilder.ToString();
    }

    protected LimiteDeValorInvalidoExcecao(SerializationInfo info, StreamingContext context) : base(info, context){}
}
