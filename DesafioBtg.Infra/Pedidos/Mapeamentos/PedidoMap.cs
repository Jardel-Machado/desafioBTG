using FluentNHibernate.Mapping;
using DesafioBtg.Dominio.Pedidos.Entidades;

namespace DesafioBtg.Infra.Pedidos.Mapeamentos;

public class PedidoMap : ClassMap<Pedido>
{
    public PedidoMap()
    {
        Schema("desafiobtg");
        Table("pedido");
        Id(pedido => pedido.Id).Column("id").GeneratedBy.Identity();
        Map(pedido => pedido.DataPedido).Column("datapedido").Not.Nullable();
        Map(pedido => pedido.CodigoPedido).Column("codigopedido").Not.Nullable().Unique();
        References(pedido => pedido.Cliente).Column("idcliente").Not.Nullable().Cascade.None();
        HasMany(pedido => pedido.Itens).Table("itempedido").KeyColumn("idpedido").Inverse().Cascade.AllDeleteOrphan();
    }
}
