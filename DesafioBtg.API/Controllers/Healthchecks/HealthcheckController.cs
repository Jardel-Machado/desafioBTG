using DesafioBtg.Aplicacao.Healthchecks.Servicos.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DesafioBtg.API.Controllers.Healthchecks;

[Route("api/v{version:apiVersion}/healthcheck")]
[ApiController]
public class HealthcheckController : ControllerBase
{
    private readonly ILogger<string> logger;
    private readonly IHealthchecksAppServico healthchecksAppServico;

    public HealthcheckController(ILogger<string> logger, IHealthchecksAppServico healthchecksAppServico)
    {
        this.logger = logger;
        this.healthchecksAppServico = healthchecksAppServico;
    }

    [HttpGet]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    public ActionResult Healthcheck()
    {
        logger.LogInformation("Teste Healthcheck - Information - OK");

        healthchecksAppServico.Healthcheck();

        return Ok("Healthcheck - OK");
    }
}
