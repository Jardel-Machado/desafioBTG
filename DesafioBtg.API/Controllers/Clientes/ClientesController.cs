using DesafioBtg.Aplicacao.Clientes.Servicos.Interfaces;
using DesafioBtg.DataTransfer.Clientes.Requests;
using DesafioBtg.DataTransfer.Clientes.Requests.Requests;
using DesafioBtg.DataTransfer.Clientes.Responses;
using DesafioBtg.DataTransfer.Clientes.Responses.Responses;
using DesafioBtg.Dominio.Uteis;
using Microsoft.AspNetCore.Mvc;

namespace DesafioBtg.API.Controllers.Clientes;

[ApiController]
[Route("api/v{version:apiVersion}/clientes")]
public class ClientesController : ControllerBase
{
    private readonly IClientesAppServico clientesAppServico;

    public ClientesController(IClientesAppServico clientesAppServico)
    {
        this.clientesAppServico = clientesAppServico;
    }

    /// <summary>
    /// Recupera um cliente por Id
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<ClienteResponse>> RecuperarAsync(int id, CancellationToken cancellationToken)
    {
        ClienteResponse response = await clientesAppServico.RecuperarAsync(id, cancellationToken);

        return Ok(response);
    }

    /// <summary>
    /// Lista os clientes com paginação
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet]
    public async Task<ActionResult<PaginacaoConsulta<ClienteResponse>>> ListarAsync([FromQuery] ClienteListarRequest request, CancellationToken cancellationToken)
    {
        PaginacaoConsulta<ClienteResponse> response = await clientesAppServico.ListarAsync(request, cancellationToken);

        return Ok(response);
    }

    /// <summary>
    /// Recupera um candidato por Id
    /// </summary>
    /// <param name="codigoCliente"></param>
    /// <returns></returns>
    [HttpGet("{codigoCliente}/quantidades-pedidos")]
    public ActionResult<ClienteQuantidadePedidoResponse> QuantidadeDePedidodPorcliente(int codigoCliente)
    {
        if (codigoCliente <= 0)        
            return BadRequest("Código do cliente inválido.");
        
        ClienteQuantidadePedidoResponse response = clientesAppServico.QuantidadeDePedidodPorcliente(codigoCliente);

        return Ok(response);
    }

    /// <summary>
    /// Recupera um candidato por Id
    /// </summary>
    /// <param name="codigoCliente"></param>
    /// <returns></returns>
    [HttpGet("{codigoCliente}/pedidos")]
    public ActionResult<IEnumerable<ClientePedidoRealizadoResponse>> QuantidadeDePedidosRealizadosPorCliente(int codigoCliente)
    {
        if (codigoCliente <= 0)
            return BadRequest("Código do cliente inválido.");

        IEnumerable<ClientePedidoRealizadoResponse> response = clientesAppServico.QuantidadeDePedidosRealizadosPorCliente(codigoCliente);

        return Ok(response);
    }

    /// <summary>
    /// Adiciona um novo cliente
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<ActionResult<ClienteResponse>> InserirAsync([FromBody] ClienteRequest request, CancellationToken cancellationToken)
    {
        ClienteResponse response = await clientesAppServico.InserirAsync(request, cancellationToken);

        return Ok(response);
    }
}
