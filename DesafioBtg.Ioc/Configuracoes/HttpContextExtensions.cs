using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DesafioBtg.Ioc.Configuracoes;

public static class HttpContextExtensions
{
    public static IServiceCollection AddHttpContextExtensions(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHttpContextAccessor();

        services.AddHttpClient("ViaCep.Api", httpClient =>
        {
            httpClient.BaseAddress = new Uri(configuration.GetSection("ViaCep.Api:BaseUrl").Value!);
            httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
            httpClient.DefaultRequestHeaders.Add("User-Agent", AppDomain.CurrentDomain.FriendlyName);
        });        

        return services;
    }
}
