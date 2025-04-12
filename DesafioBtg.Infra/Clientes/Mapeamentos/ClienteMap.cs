using FluentNHibernate.Mapping;
using DesafioBtg.Dominio.Clientes.Entidades;

namespace DesafioBtg.Infra.Clientes.Mapeamentos;

public class ClienteMap : ClassMap<Cliente>
{
    public ClienteMap()
    {
        Schema("desafiobtg");
        Table("cliente");
        Id(cliente => cliente.Id).Column("id").GeneratedBy.Identity();
        Map(cliente => cliente.Nome).Column("nome").Length(150).Nullable();
        Map(cliente => cliente.CodigoCliente).Column("codigocliente").Not.Nullable();
        HasMany(cliente => cliente.Pedidos).Table("pedido").KeyColumn("idcliente").Inverse().Cascade.AllDeleteOrphan();
    }
}
