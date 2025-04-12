using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace DesafioBtg.Dominio.Excecoes;

[ExcludeFromCodeCoverage]
[Serializable]

public class AtributoInvalidoExcecao : RegraDeNegocioExcecao
{
    public AtributoInvalidoExcecao(string atributo) : base(atributo + " inválido"){}

    protected AtributoInvalidoExcecao(SerializationInfo info, StreamingContext context): base(info, context){}
}


