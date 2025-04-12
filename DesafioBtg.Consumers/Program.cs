using DesafioBtg.Consumers.Controllers;
using DesafioBtg.Consumers.Extensions;
using DesafioBtg.Consumers.Pedidos;
using DesafioBtg.Consumers.Pedidos.Interfaces;
using DesafioBtg.Consumers.Usuarios;
using DesafioBtg.Consumers.Usuarios.Interfaces;
using DesafioBtg.Dominio.Redis.Repositorios.Interfaces;
using DesafioBtg.Ioc;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using System.Text.Json.Serialization;

namespace DesafioBtg.Consumers;

public static class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        AddServices(builder);

        AddBackgroundServices(builder);

        var app = builder.Build();

        using (var scope = app.Services.CreateScope())
        {
            _ = scope.ServiceProvider.GetRequiredService<IRedisRepositorio>();
        }

        ConfigureServices(app);

        app.Run();
    }

    public static void AddServices(WebApplicationBuilder builder)
    {
        builder.Services.AddControllers()
            .ConfigureApplicationPartManager(apm =>
            {
                apm.ApplicationParts.Clear();
                apm.ApplicationParts.Add(new AssemblyPart(typeof(ServiceManagerController).Assembly));
            })
            .AddJsonOptions(op =>
        {
            op.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            op.JsonSerializerOptions.PropertyNamingPolicy = null;
        });

        NativeInjectorBootStrapper.RegisterServices(builder.Services, builder.Configuration);

        builder.Services.AddSwaggerConsumersExtensions();

        builder.Services.AddEndpointsApiExplorer();

        builder.Services.Configure<ApiBehaviorOptions>(options =>
        {
            options.SuppressModelStateInvalidFilter = true;
        });
    }

    public static void AddBackgroundServices(WebApplicationBuilder builder)
    {
        builder.Services.AddHostedService<UsuariosBackgroundService>();
        builder.Services.AddHostedService<PedidosBackgroundService>();
        builder.Services.AddScoped<IUsuarioConsumer, UsuarioConsumer>();
        builder.Services.AddScoped<IPedidoConsumer, PedidoConsumer>();
    }

    public static void ConfigureServices(WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.ConfigureSwaggerConsumers();
        }

        app.AddCorsConsumersExtensions();

        app.MapControllers();
    }
}
