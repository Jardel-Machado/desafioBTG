using DesafioBtg.Aplicacao.Pedidos.Servicos.Interfaces;
using DesafioBtg.DataTransfer.Pedidos.Requests;
using DesafioBtg.DataTransfer.Pedidos.Responses;
using DesafioBtg.DataTransfer.Pedidos.Responses.Responses;
using DesafioBtg.Dominio.Pedidos.Consultas;
using DesafioBtg.Dominio.Pedidos.Entidades;
using DesafioBtg.Dominio.Pedidos.Repositorios.Filtros;
using DesafioBtg.Dominio.Pedidos.Repositorios.Interfaces;
using DesafioBtg.Dominio.Pedidos.Servicos.Interfaces;
using DesafioBtg.Dominio.Rabbitmq.Repositorios.Interfaces;
using DesafioBtg.Dominio.Uteis;
using MapsterMapper;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace DesafioBtg.Aplicacao.Pedidos.Servicos;

public class PedidosAppServico : IPedidosAppServico
{
    private readonly IPedidosServico pedidosServico;
    private readonly IPedidosRepositorio pedidosRepositorio;
    private readonly IMapper mapper;
    private readonly ILogger<PedidosAppServico> logger;
    private readonly IRabbitmqRepositorio rabbitmq;

    public PedidosAppServico(IPedidosServico pedidosServico, IPedidosRepositorio pedidosRepositorio, IMapper mapper, ILogger<PedidosAppServico> logger, IRabbitmqRepositorio rabbitmq)
    {
        this.pedidosServico = pedidosServico;
        this.pedidosRepositorio = pedidosRepositorio;
        this.mapper = mapper;
        this.logger = logger;
        this.rabbitmq = rabbitmq;
    }

    public async Task<PedidoResponse> RecuperarAsync(int id, CancellationToken cancellationToken)
    {
        Pedido pedido = await pedidosServico.ValidarAsync(id, cancellationToken);

        var response = mapper.Map<PedidoResponse>(pedido);

        return response;
    }

    public async Task<PaginacaoConsulta<PedidoResponse>> ListarAsync(PedidoListarRequest request, CancellationToken cancellationToken)
    {
        PedidoListarFiltro filtros = mapper.Map<PedidoListarFiltro>(request);

        IQueryable<Pedido> query = pedidosRepositorio.Filtrar(filtros);

        PaginacaoConsulta<Pedido> pedidos = await pedidosRepositorio.ListarAsync(query, request.Qt, request.Pg, request.CpOrd, request.TpOrd, cancellationToken);

        PaginacaoConsulta<PedidoResponse> response;

        response = mapper.Map<PaginacaoConsulta<PedidoResponse>>(pedidos);

        return response;
    }

    public PedidoValorTotalResponse BuscarValorTotalPedido(int codigoPedido)
    {
        PedidoValorTotalConsulta valorTotal = pedidosServico.BuscarValorTotalPedido(codigoPedido);

        return mapper.Map<PedidoValorTotalResponse>(valorTotal);
    }

    public async Task<string> InserirPedidoFilaAsync(PedidoInserirFilaRequest request, CancellationToken cancellationToken)
    {
        try
        {
            string pedido = JsonSerializer.Serialize(request);

            await rabbitmq.PublicarMensagemAsync(pedido, "btg-pactual-order-created", cancellationToken);

            return "Mensagem publicada com sucesso";
        }
        catch(Exception ex)
        {
            logger.LogError(ex, "<{EventoId}> Falha ao publicar pedido na fila", "InserirFilaAsync");

            throw;
        }
    }
}
