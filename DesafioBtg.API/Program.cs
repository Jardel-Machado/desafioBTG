using DesafioBtg.API.Extensions;
using DesafioBtg.Dominio.Redis.Repositorios.Interfaces;
using DesafioBtg.Dominio.Uteis;
using DesafioBtg.Ioc;
using DesafioBtg.Ioc.Configuracoes;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddNHibernateSchema(builder.Configuration, builder.Environment);

// Add services to the container.
builder.Services.AddControllers().AddJsonOptions(op =>
{
    op.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());

    op.JsonSerializerOptions.PropertyNamingPolicy = null;

    op.JsonSerializerOptions.Converters.Add(new CustomDateTimeConverter());
});

// Configura o Versionamento da API
builder.Services.AddVersioningExtensions();

// Configura o Swagger
builder.Services.AddSwaggerExtensions();

builder.Services.AddEndpointsApiExplorer();

// Remove a validação automática do ModelState em requisições de API.
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});

NativeInjectorBootStrapper.RegisterServices(builder.Services, builder.Configuration);

// Configura JWT Authentication usando a chave do Redis
using var scope = builder.Services.BuildServiceProvider().CreateScope();

var redisRepositorio = scope.ServiceProvider.GetRequiredService<IRedisRepositorio>();

await builder.Services.AddAuthenticationExtensions(builder.Configuration, redisRepositorio);

// Configura a Autorização
builder.Services.AddAuthorizationExtensions();

var app = builder.Build();

app.AddCorsExtensions();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.ConfigureSwagger();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
