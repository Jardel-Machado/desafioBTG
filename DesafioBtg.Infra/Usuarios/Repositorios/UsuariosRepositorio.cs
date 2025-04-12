using DesafioBtg.Dominio.Usuarios.Repositorios.Interfaces;
using DesafioBtg.Dominio.Usuarios.Entidades;
using DesafioBtg.Infra.Genericos;
using ISession = NHibernate.ISession;

namespace DesafioBtg.Infra.Usuarios.Repositorios;

public class UsuariosRepositorio : GenericosRepositorio<Usuario>, IUsuariosRepositorio
{
    public UsuariosRepositorio(ISession session) : base(session)
    {
        
    }    
}
