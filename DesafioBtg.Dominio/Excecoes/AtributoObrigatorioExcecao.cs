using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace DesafioBtg.Dominio.Excecoes;

[ExcludeFromCodeCoverage]
[Serializable]

public class AtributoObrigatorioExcecao : RegraDeNegocioExcecao
{
    public AtributoObrigatorioExcecao(string atributo) : base(atributo + " é obrigatório"){}

    protected AtributoObrigatorioExcecao(SerializationInfo info, StreamingContext context) : base(info, context){}
}

