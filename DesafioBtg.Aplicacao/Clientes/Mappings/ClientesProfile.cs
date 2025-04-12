using Mapster;
using DesafioBtg.Aplicacao.MapeamentosBases;
using DesafioBtg.DataTransfer.Clientes.Requests;
using DesafioBtg.Dominio.Clientes.Repositorios.Filtros;
using DesafioBtg.Dominio.Clientes.Entidades;
using DesafioBtg.DataTransfer.Clientes.Responses.Responses;
using DesafioBtg.DataTransfer.Clientes.Requests.Requests;
using DesafioBtg.Dominio.Clientes.Servicos.Comandos;
using DesafioBtg.Dominio.Clientes.Consultas;
using DesafioBtg.DataTransfer.Clientes.Responses;

namespace DesafioBtg.Aplicacao.Clientes.Mappings;

public class ClientesMapping : MapeamentoBase
{
    public override void Register(TypeAdapterConfig config)
    {
        MapeamentoSimples<Cliente, ClienteResponse>(config);        
        MapeamentoSimples<ClienteListarRequest, ClienteListarFiltro>(config);
        MapeamentoSimples<ClienteRequest, ClienteComando>(config);
        MapeamentoSimples<ClienteQuantidadePedidoConsulta, ClienteQuantidadePedidoResponse>(config);
        MapeamentoSimples<ClientePedidoRealizadoConsulta, ClientePedidoRealizadoResponse>(config);
    }
}
