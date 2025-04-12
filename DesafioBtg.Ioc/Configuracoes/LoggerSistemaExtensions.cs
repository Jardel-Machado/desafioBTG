using Microsoft.Extensions.Logging;

namespace DesafioBtg.Ioc.Configuracoes;

public static class LoggerSistemaExtensions
{
    public static ILogger AddLoggerSistema(this ILoggerFactory loggerFactory, string categoria = "Sistema")
    {
        return loggerFactory.CreateLogger(categoria);
    }

    public static ILoggerFactory AddLoggerConsole()
    {
        return LoggerFactory.Create(builder =>
        {
            builder.AddConsole();
        });
    }
}
