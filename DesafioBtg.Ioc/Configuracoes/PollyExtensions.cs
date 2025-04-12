using Polly.Retry;
using Polly;
using Polly.Timeout;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;

namespace DesafioBtg.Ioc.Configuracoes;

public static class PollyExtensions
{
    private static readonly ResiliencePropertyKey<ILogger> LoggerKey = new("ILogger");
    private const int STATUS_CODE = 500;
    private const int DELAY = 1;
    private const int RETRY = 3;
    private const int TIMEOUT = 30;
    public static IServiceCollection AddConfiguracoesPolly(this IServiceCollection services)
    {
        services.AddResiliencePipeline<string, HttpResponseMessage>("RetryPolicy", (builder) =>
        {
            builder.AddRetry(new RetryStrategyOptions<HttpResponseMessage>
            {
                Delay = TimeSpan.FromSeconds(DELAY),
                ShouldHandle = new PredicateBuilder<HttpResponseMessage>()
                    .Handle<HttpRequestException>()
                    .Handle<TimeoutRejectedException>()
                    .HandleResult(response => (int)response.StatusCode >= STATUS_CODE),
                BackoffType = DelayBackoffType.Linear,
                MaxRetryAttempts = RETRY,
                OnRetry = args =>
                {
                    RegistrarLog(args);
                    return ValueTask.CompletedTask;
                }
            });
            builder.AddTimeout(new TimeoutStrategyOptions
            {
                Name = "Timeout",
                Timeout = TimeSpan.FromSeconds(TIMEOUT)
            });
        });

        return services;
    }
    private static void RegistrarLog(OnRetryArguments<HttpResponseMessage> arguments)
    {
        var result = arguments.Outcome.Result;
        var exception = arguments.Outcome.Exception;
        var logger = arguments.Context.ObterContextoDoLog();
        logger?.LogWarning(exception,
            "<{EventoId}> Esperando {PollyDelay}s e então realizando uma nova tentativa. Tentativa número {PollyRetry}."
            + " StatusCode: {PollyStatusCode}"
            + " Método: {PollyApiMetodo}"
            + " Conteúdo: {PollyConteudo}"
            + " RequisicaoUri: {PollyRequisicaoUri}",
            "PoliticaPolly",
            arguments.RetryDelay.TotalSeconds,
            arguments.AttemptNumber + 1,
            result?.StatusCode.ToString(),
            result?.RequestMessage?.Method.Method,
            result?.Content?.ReadAsStringAsync().Result,
            result?.RequestMessage?.RequestUri?.OriginalString);
    }
    private static ILogger ObterContextoDoLog(this ResilienceContext context) =>
        context.Properties.TryGetValue(LoggerKey, out var logger) ? logger : null!;
}
