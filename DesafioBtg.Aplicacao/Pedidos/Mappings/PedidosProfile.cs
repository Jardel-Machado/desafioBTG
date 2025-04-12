using DesafioBtg.Aplicacao.MapeamentosBases;
using DesafioBtg.DataTransfer.Pedidos.Responses;
using DesafioBtg.Dominio.Pedidos.Consultas;
using Mapster;


namespace DesafioBtg.Aplicacao.Pedidos.Mappings;

public class PedidosMapping : MapeamentoBase
{
    public override void Register(TypeAdapterConfig config)
    {
        MapeamentoSimples<PedidoValorTotalConsulta, PedidoValorTotalResponse>(config);
    }
}
