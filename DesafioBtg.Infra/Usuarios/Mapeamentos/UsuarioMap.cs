using DesafioBtg.Dominio.Usuarios.Entidades;
using FluentNHibernate.Mapping;

namespace DesafioBtg.Infra.Usuarios.Mapeamentos;

public class UsuarioMap : ClassMap<Usuario>
{
    public UsuarioMap()
    {
        Schema("desafiobtg");
        Table("usuarios");
        Id(usuario => usuario.Id).Column("id").GeneratedBy.Identity();
        Map(usuario => usuario.NomeCompleto).Column("nomeCompleto").Length(150).Not.Nullable();
        Map(usuario => usuario.NomeUsuario).Column("nomeUsuario").Length(50).Unique().Not.Nullable();
        Map(usuario => usuario.Email).Column("email").Length(150).Unique().Not.Nullable();
    }
}
