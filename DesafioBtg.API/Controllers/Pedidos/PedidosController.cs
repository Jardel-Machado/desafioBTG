using DesafioBtg.Aplicacao.Pedidos.Servicos.Interfaces;
using DesafioBtg.DataTransfer.Pedidos.Requests;
using DesafioBtg.DataTransfer.Pedidos.Responses;
using Microsoft.AspNetCore.Mvc;

namespace DesafioBtg.API.Controllers.Pedidos;

[ApiController]
[Route("api/v{version:apiVersion}/pedidos")]
public class PedidosController : ControllerBase
{
    private readonly IPedidosAppServico pedidosAppServico;

    public PedidosController(IPedidosAppServico pedidosAppServico)
    {
        this.pedidosAppServico = pedidosAppServico;
    }

    /// <summary>
    /// Recupera um candidato por Id
    /// </summary>
    /// <param name="codigoPedido"></param>
    /// <returns></returns>
    [HttpGet("{codigoPedido}/valores-totais")]
    public ActionResult<PedidoValorTotalResponse> BuscarValorTotalPedido(int codigoPedido)
    {
        if (codigoPedido <= 0)
            return BadRequest("O código do pedido deve ser maior que zero.");

        PedidoValorTotalResponse valorTotal = pedidosAppServico.BuscarValorTotalPedido(codigoPedido);

        return Ok(valorTotal);
    }

    /// <summary>
    /// Adiciona um novo pedido na fila
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<ActionResult> InserirPedidoFilaAsync([FromBody] PedidoInserirFilaRequest request, CancellationToken cancellationToken)
    {
        if (request is null)
            return BadRequest("O corpo da requisição não pode ser nulo.");

        string mensagem = await pedidosAppServico.InserirPedidoFilaAsync(request, cancellationToken);

        return Accepted(mensagem);
    }
}
