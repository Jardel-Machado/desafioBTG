using DesafioBtg.Aplicacao.MapeamentosBases;
using Mapster;

namespace DesafioBtg.Aplicacao.PaginacoesConsultas.Mappings;

public class PaginacoesConsultasMapping : MapeamentoBase
{
    public override void Register(TypeAdapterConfig config)
    {
        MapeamentoPaginacaoGenerica(config);
    }
}
