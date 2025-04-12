using DesafioBtg.Aplicacao.Clientes.Servicos.Interfaces;
using DesafioBtg.Aplicacao.Transacoes.Servicos.Interface;
using DesafioBtg.DataTransfer.Clientes.Requests;
using DesafioBtg.DataTransfer.Clientes.Requests.Requests;
using DesafioBtg.DataTransfer.Clientes.Responses;
using DesafioBtg.DataTransfer.Clientes.Responses.Responses;
using DesafioBtg.Dominio.Clientes.Consultas;
using DesafioBtg.Dominio.Clientes.Entidades;
using DesafioBtg.Dominio.Clientes.Repositorios.Filtros;
using DesafioBtg.Dominio.Clientes.Repositorios.Interfaces;
using DesafioBtg.Dominio.Clientes.Servicos.Comandos;
using DesafioBtg.Dominio.Clientes.Servicos.Interfaces;
using DesafioBtg.Dominio.Uteis;
using MapsterMapper;

namespace DesafioBtg.Aplicacao.Clientes.Servicos;

public class ClientesAppServico : IClientesAppServico
{
    private readonly IClientesServico clientesServico;
    private readonly IClientesRepositorio clientesRepositorio;
    private readonly IMapper mapper;
    private readonly IUnitOfWork unitOfWork;

    public ClientesAppServico(IClientesServico clientesServico, IClientesRepositorio clientesRepositorio, IMapper mapper, IUnitOfWork unitOfWork)
    {
        this.clientesServico = clientesServico;
        this.clientesRepositorio = clientesRepositorio;
        this.mapper = mapper;
        this.unitOfWork = unitOfWork;
    }

    public async Task<ClienteResponse> RecuperarAsync(int id, CancellationToken cancellationToken)
    {
        Cliente cliente = await clientesServico.ValidarAsync(id, cancellationToken);
        
        return mapper.Map<ClienteResponse>(cliente);
    }

    public async Task<PaginacaoConsulta<ClienteResponse>> ListarAsync(ClienteListarRequest request, CancellationToken cancellationToken)
    {
        ClienteListarFiltro filtros = mapper.Map<ClienteListarFiltro>(request);

        IQueryable<Cliente> query = clientesRepositorio.Filtrar(filtros);

        PaginacaoConsulta<Cliente> clientes = await clientesRepositorio.ListarAsync(query, request.Qt, request.Pg, request.CpOrd, request.TpOrd, cancellationToken);

        return mapper.Map<PaginacaoConsulta<ClienteResponse>>(clientes);
    }

    public async Task<ClienteResponse> InserirAsync(ClienteRequest request, CancellationToken cancellationToken)
    {
        ClienteComando comando = mapper.Map<ClienteComando>(request);

        try
        {
            unitOfWork.BeginTransaction();

            Cliente cliente = await clientesServico.InserirAsync(comando, cancellationToken);

            unitOfWork.Commit();

            return mapper.Map<ClienteResponse>(cliente);
        }
        catch
        {
            unitOfWork.Rollback();

            throw;
        }
    }

    public ClienteQuantidadePedidoResponse QuantidadeDePedidodPorcliente(int codigoCliente)
    {
        ClienteQuantidadePedidoConsulta consulta = clientesServico.QuantidadeDePedidodPorcliente(codigoCliente);

        return mapper.Map<ClienteQuantidadePedidoResponse>(consulta);
    }

    public IEnumerable<ClientePedidoRealizadoResponse> QuantidadeDePedidosRealizadosPorCliente(int codigoCliente)
    {
        IEnumerable<ClientePedidoRealizadoConsulta> consulta = clientesServico.QuantidadeDePedidosRealizadosPorCliente(codigoCliente);

        return mapper.Map<IEnumerable<ClientePedidoRealizadoResponse>>(consulta);
    }
}
