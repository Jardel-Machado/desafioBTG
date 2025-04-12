using DesafioBtg.Aplicacao.Healthchecks.Servicos.Interfaces;
using DesafioBtg.Dominio.Usuarios.Repositorios.Interfaces;

namespace DesafioBtg.Aplicacao.Healthchecks.Servicos;

public class HealthchecksAppServico : IHealthchecksAppServico
{
    private readonly IUsuariosRepositorio usuariosRepositorio;

    public HealthchecksAppServico(IUsuariosRepositorio usuariosRepositorio)
    {
        this.usuariosRepositorio = usuariosRepositorio;
    }

    public void Healthcheck()
    {
        usuariosRepositorio.Query().FirstOrDefault();
    }
}
