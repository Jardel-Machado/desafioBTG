using DesafioBtg.Dominio.Uteis;
using Mapster;
using System.Linq.Expressions;

namespace DesafioBtg.Aplicacao.MapeamentosBases;

public abstract class MapeamentoBase : IRegister
{
    public abstract void Register(TypeAdapterConfig config);

    protected static TypeAdapterSetter<TSource, TDestination> MapeamentoCustomizado<TSource, TDestination>(TypeAdapterConfig config)
    {
        // Cria e retorna uma instância para configurar propriedades manualmente
        return config.NewConfig<TSource, TDestination>();
    }

    protected static void MapeamentoSimples<TSource, TDestination>(TypeAdapterConfig config)
    {
        // Registra mapeamento padrão 1:1 (propriedades com o mesmo nome)
        config.NewConfig<TSource, TDestination>();
    }

    protected static void MapeamentoEnum<TSource, TDestination>(TypeAdapterConfig config, Expression<Func<TSource, TDestination>> conversor)
    {
        // Registra conversão direta entre tipos (ex: enum → DTO), usando expressão para análise em tempo de execução
        config.NewConfig<TSource, TDestination>().MapWith(conversor);
    }

    protected static void MapeamentoPaginacaoGenerica(TypeAdapterConfig config)
    {
        // Registra mapeamento genérico aberto para PaginacaoConsulta<T> → PaginacaoConsulta<T>
        config.NewConfig(typeof(PaginacaoConsulta<>), typeof(PaginacaoConsulta<>));
    }
}
