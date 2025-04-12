using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;
using System.Text;

namespace DesafioBtg.Dominio.Excecoes;

[ExcludeFromCodeCoverage]
[Serializable]

public class TamanhoDeAtributoInvalidoExcecao : RegraDeNegocioExcecao
{
    public TamanhoDeAtributoInvalidoExcecao(string atributo) : base(MontaMensagemErro(atributo, null, null)){}

    public TamanhoDeAtributoInvalidoExcecao(string atributo, int? tamanhoMinimo, int? tamanhoMaximo) : base(MontaMensagemErro(atributo, tamanhoMinimo, tamanhoMaximo)){}

    private static string MontaMensagemErro(string atributo, int? tamanhoMinimo, int? tamanhoMaximo)
    {
        StringBuilder stringBuilder = new StringBuilder("Tamanho do campo " + atributo + " inválido.");
        if (tamanhoMinimo.HasValue)
        {
            stringBuilder.Append(" Tamanho mínimo: ");
            stringBuilder.Append(tamanhoMinimo.Value);
            stringBuilder.Append(".");
        }

        if (tamanhoMaximo.HasValue)
        {
            stringBuilder.Append(" Tamanho máximo: ");
            stringBuilder.Append(tamanhoMaximo.Value);
            stringBuilder.Append(".");
        }

        return stringBuilder.ToString();
    }

    protected TamanhoDeAtributoInvalidoExcecao(SerializationInfo info, StreamingContext context) : base(info, context){}
}

