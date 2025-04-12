using DesafioBtg.DataTransfer.Clientes.Requests;
using DesafioBtg.DataTransfer.Clientes.Requests.Requests;
using DesafioBtg.DataTransfer.Clientes.Responses;
using DesafioBtg.DataTransfer.Clientes.Responses.Responses;
using DesafioBtg.Dominio.Uteis;

namespace DesafioBtg.Aplicacao.Clientes.Servicos.Interfaces;

public interface IClientesAppServico
{
    Task<ClienteResponse> RecuperarAsync(int id, CancellationToken cancellationToken);
    Task<PaginacaoConsulta<ClienteResponse>> ListarAsync(ClienteListarRequest request, CancellationToken cancellationToken);
    Task<ClienteResponse> InserirAsync(ClienteRequest request, CancellationToken cancellationToken);
    ClienteQuantidadePedidoResponse QuantidadeDePedidodPorcliente(int codigoCliente);
    IEnumerable<ClientePedidoRealizadoResponse> QuantidadeDePedidosRealizadosPorCliente(int codigoCliente);
}
