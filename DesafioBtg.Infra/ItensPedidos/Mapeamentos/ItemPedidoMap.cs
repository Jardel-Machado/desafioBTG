using FluentNHibernate.Mapping;
using DesafioBtg.Dominio.ItensPedidos.Entidades;

namespace DesafioBtg.Infra.ItensPedidos.Mapeamentos;

public class ItemPedidoMap : ClassMap<ItemPedido>
{
    public ItemPedidoMap()
    {
        Schema("desafiobtg");
        Table("itempedido");
        Id(item => item.Id).Column("id").GeneratedBy.Identity();
        Map(item => item.Produto).Column("produto").Length(100).Not.Nullable();
        Map(item => item.Quantidade).Column("quantidade").Not.Nullable();
        Map(item => item.Preco).Column("preco").Not.Nullable();
        References(x => x.Pedido).Column("idpedido").Not.Nullable().Cascade.None();
    }
}
