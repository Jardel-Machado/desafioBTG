using DesafioBtg.Ioc.Abstracoes;
using Microsoft.Extensions.Hosting;

namespace DesafioBtg.Ioc.Interfaces;

public interface IHostedServiceCustom : IHostedService
{
    DadosServico GetDadosServico();
    CancellationToken GetCancellationToken();
}
