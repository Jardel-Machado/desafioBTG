using DesafioBtg.DataTransfer.Dados.Responses;
using DesafioBtg.Ioc.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DesafioBtg.Consumers.Controllers;

[ApiController]
[Route("[controller]")]
public class ServiceManagerController(IServiceProvider serviceProvider) : ControllerBase
{
    private readonly List<IHostedServiceCustom> servicos = serviceProvider.GetServices<IHostedService>().OfType<IHostedServiceCustom>().ToList();

    [HttpGet]
    [Route("/services")]
    public ActionResult<List<DadoServicoResponse>> GetServices()
    {
        var result = new List<DadoServicoResponse>();

        foreach (var serv in servicos)
        {
            var dadosServico = serv.GetDadosServico();

            result.Add(new DadoServicoResponse()
            {
                Name = dadosServico.Name,
                DataInicio = dadosServico.DataInicio,
                DataUltimaExecucao = dadosServico.DataUltimaExecucao,
                DataEncerramento = dadosServico.DataUltimaExecucao,
                EmExecucao = (dadosServico.EmExecucao ? "Sim" : "Não"),
            });
        }

        return result;
    }

    [HttpGet]
    [Route("/services/{nome}/stop")]
    public async Task<ActionResult> StopServiceAsync(string nome)
    {
        var hostedService = servicos.Find(o => o.GetDadosServico().Name == nome)
            ?? throw new ArgumentException($"Serviço {nome} não localizado.");

        if (!hostedService.GetDadosServico().EmExecucao)
            throw new InvalidOperationException($"Serviço {nome} não está em execução.");

        await hostedService.StopAsync(hostedService.GetCancellationToken());

        return Ok();
    }

    [HttpGet]
    [Route("/services/{nome}/start")]
    public async Task<ActionResult> StartServiceAsync(string nome)
    {
        var hostedService = servicos.Find(o => o.GetDadosServico().Name == nome)
            ?? throw new ArgumentException($"Serviço {nome} não localizado.");

        if (hostedService.GetDadosServico().EmExecucao)
            throw new InvalidOperationException($"Serviço {nome} já está em execução.");

        await hostedService.StartAsync(new CancellationToken());

        return Ok();
    }
}

