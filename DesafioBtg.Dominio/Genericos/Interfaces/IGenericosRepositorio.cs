using DesafioBtg.Dominio.Uteis;
using DesafioBtg.Dominio.Uteis.Enumeradores;
using System.Linq.Expressions;

namespace DesafioBtg.Dominio.Genericos.Interfaces;

public interface IGenericosRepositorio<T> where T : class
{
    void Inserir(T entidade);

    void Inserir(IEnumerable<T> entidades);

    void Editar(T entidade);

    void Excluir(T entidade);

    void Excluir(IEnumerable<T> entidades);

    T Recuperar(int id);

    T Recuperar(Expression<Func<T, bool>> expression);

    IQueryable<T> Query();

    IQueryable<T> QueryWithJoin(params Expression<Func<T, object>>[] entitySelectors);

    PaginacaoConsulta<T> Listar(IQueryable<T> query, int qt, int pg, string cpOrd, TipoOrdenacaoEnum tpOrd);

    PaginacaoConsulta<T> Listar(IQueryable<T> query, int qt, int pg, params (string, TipoOrdenacaoEnum)[] ordenacao);

    void Refresh(T entidade);

    Task InserirAsync(T entidade, CancellationToken cancelattionToken = default);

    Task InserirAsync(IEnumerable<T> entidades, CancellationToken cancelattionToken = default);

    Task EditarAsync(T entidade, CancellationToken cancelattionToken = default);

    Task ExcluirAsync(T entidade, CancellationToken cancelattionToken = default);

    Task ExcluirAsync(IEnumerable<T> entidades, CancellationToken cancelattionToken = default);

    Task<T> RecuperarAsync(int id, CancellationToken cancelattionToken = default);

    Task<T> RecuperarAsync(Expression<Func<T, bool>> expression, CancellationToken cancelattionToken = default);

    Task<PaginacaoConsulta<T>> ListarAsync(IQueryable<T> query, int qt, int pg, string cpOrd, TipoOrdenacaoEnum tpOrd, CancellationToken cancelattionToken = default);

    Task<PaginacaoConsulta<T>> ListarAsync(IQueryable<T> query, int qt, int pg, (string, TipoOrdenacaoEnum)[] ordenacao, CancellationToken cancelattionToken = default);

    Task RefreshAsync(T entidade, CancellationToken cancelattionToken = default);
}
