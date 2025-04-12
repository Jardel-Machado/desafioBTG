using Microsoft.AspNetCore.Rewrite;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerUI;
using System.Reflection;

namespace DesafioBtg.Consumers.Extensions;

public static class SwaggerConsumersExtensions
{
    public static IServiceCollection AddSwaggerConsumersExtensions(this IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "DesafioBtg.Consumers", Version = "v1" });

            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";

            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

            c.IncludeXmlComments(xmlPath);
            
            c.MapType<DateTime>(() => new OpenApiSchema
            {
                Type = "string",
                Format = "date-time",
                Example = new OpenApiString(DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"))
            });
        });

        return services;
    }

    public static WebApplication ConfigureSwaggerConsumers(this WebApplication app)
    {
        app.UseSwagger();

        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("../swagger/v1/swagger.json", "DesafioBtg.Consumers v1");
            c.DisplayRequestDuration();
            c.DocExpansion(DocExpansion.None);
        });

        RewriteOptions option = new();

        option.AddRedirect("^$", "swagger");

        app.UseRewriter(option);

        return app;
    }
}

