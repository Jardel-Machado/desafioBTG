using System.Linq.Dynamic.Core;
using DesafioBtg.Dominio.Excecoes;
using System.Linq.Dynamic.Core.Exceptions;
using System.Linq.Expressions;
using DesafioBtg.Dominio.Genericos.Interfaces;
using DesafioBtg.Dominio.Uteis;
using DesafioBtg.Dominio.Uteis.Enumeradores;
using NHibernate;
using NHibernate.Linq;

namespace DesafioBtg.Infra.Genericos;

public class GenericosRepositorio<T> : IGenericosRepositorio<T> where T : class
{
    protected readonly ISession session;

    public GenericosRepositorio(ISession session)
    {
        this.session = session;
    }

    public void Editar(T entidade)
    {
        session.Update(entidade);
    }

    public void Excluir(T entidade)
    {
        session.Delete(entidade);
    }

    public void Excluir(IEnumerable<T> entidades)
    {
        foreach (T entidade in entidades)
        {
            session.Delete(entidade);
        }
    }

    public void Inserir(T entidade)
    {
        session.Save(entidade);
    }

    public void Inserir(IEnumerable<T> entidades)
    {
        foreach (T entidade in entidades)
        {
            session.Save(entidade);
        }
    }

    public IQueryable<T> Query()
    {
        return session.Query<T>();
    }

    public IQueryable<T> QueryWithJoin(params Expression<Func<T, object>>[] entitySelectors)
    {
        if (entitySelectors == null || entitySelectors.Length == 0)
            return Query();

        IQueryable<T> queryable = Query();

        foreach (Expression<Func<T, object>> relatedObjectSelector in entitySelectors)
        {
            queryable = queryable.Fetch(relatedObjectSelector);
        }

        return queryable;
    }

    public PaginacaoConsulta<T> Listar(IQueryable<T> query, int qt, int pg, string cpOrd, TipoOrdenacaoEnum tpOrd)
    {
        try
        {
            query = query.OrderBy(cpOrd + " " + tpOrd);

            return Paginar(query, qt, pg);
        }
        catch (ParseException)
        {
            throw new CampoParaOrdernacaoInformadoNaoEValidoExcecao(cpOrd);
        }
    }

    public PaginacaoConsulta<T> Listar(IQueryable<T> query, int qt, int pg, params (string, TipoOrdenacaoEnum)[] ordenacao)
    {
        try
        {
            string ordering = string.Join(",", ordenacao.Select((x) => x.Item1 + " " + x.Item2));

            query = query.OrderBy(ordering);

            return Paginar(query, qt, pg);
        }
        catch (ParseException)
        {
            throw new CampoParaOrdernacaoInformadoNaoEValidoExcecao(string.Join(", ", ordenacao.Select((x) => x.Item1)));
        }
    }

    public T Recuperar(int id)
    {
        return session.Get<T>(id);
    }

    public T Recuperar(Expression<Func<T, bool>> expression)
    {
        return Query().Where(expression).SingleOrDefault();
    }

    public void Refresh(T entidade)
    {
        session.Refresh(entidade);
    }

    private static PaginacaoConsulta<T> Paginar(IQueryable<T> query, int qt, int pg)
    {
        return new PaginacaoConsulta<T>
        {
            Registros = query.Skip((pg - 1) * qt).Take(qt).ToList(),
            Total = query.LongCount()
        };
    }

    public async Task InserirAsync(T entidade, CancellationToken cancelattionToken = default)
    {
        await session.SaveAsync(entidade, cancelattionToken);
    }

    public async Task InserirAsync(IEnumerable<T> entidades, CancellationToken cancelattionToken = default)
    {
        foreach (T entidade in entidades)
        {
            await session.SaveAsync(entidade, cancelattionToken);
        }
    }

    public async Task EditarAsync(T entidade, CancellationToken cancelattionToken = default)
    {
        await session.UpdateAsync(entidade, cancelattionToken);
    }

    public async Task ExcluirAsync(T entidade, CancellationToken cancelattionToken = default)
    {
        await session.DeleteAsync(entidade, cancelattionToken);
    }

    public async Task ExcluirAsync(IEnumerable<T> entidades, CancellationToken cancelattionToken = default)
    {
        foreach (T entidade in entidades)
        {
            await session.DeleteAsync(entidade, cancelattionToken);
        }
    }

    public async Task<T> RecuperarAsync(int id, CancellationToken cancelattionToken = default)
    {
        return await session.GetAsync<T>(id, cancelattionToken);
    }

    public async Task<T> RecuperarAsync(Expression<Func<T, bool>> expression, CancellationToken cancelattionToken = default)
    {
        return await Query().Where(expression).SingleOrDefaultAsync(cancelattionToken);
    }

    public async Task<PaginacaoConsulta<T>> ListarAsync(IQueryable<T> query, int qt, int pg, string cpOrd, TipoOrdenacaoEnum tpOrd, CancellationToken cancelattionToken = default)
    {
        try
        {
            query = query.OrderBy(cpOrd + " " + tpOrd);

            return await PaginarAsync(query, qt, pg, cancelattionToken);
        }
        catch (ParseException)
        {
            throw new CampoParaOrdernacaoInformadoNaoEValidoExcecao(cpOrd);
        }
    }

    public async Task<PaginacaoConsulta<T>> ListarAsync(IQueryable<T> query, int qt, int pg, (string, TipoOrdenacaoEnum)[] ordenacao, CancellationToken cancelattionToken = default)
    {
        try
        {
            string ordering = string.Join(",", ordenacao.Select((x) => x.Item1 + " " + x.Item2));

            query = query.OrderBy(ordering);

            return await PaginarAsync(query, qt, pg, cancelattionToken);
        }
        catch (ParseException)
        {
            throw new CampoParaOrdernacaoInformadoNaoEValidoExcecao(string.Join(", ", ordenacao.Select((x) => x.Item1)));
        }
    }

    public async Task RefreshAsync(T entidade, CancellationToken cancelattionToken = default)
    {
        await session.RefreshAsync(entidade, cancelattionToken);
    }

    private static async Task<PaginacaoConsulta<T>> PaginarAsync(IQueryable<T> query, int qt, int pg, CancellationToken cancelattionToken = default)
    {
        PaginacaoConsulta<T> paginacaoConsulta = new PaginacaoConsulta<T>();

        PaginacaoConsulta<T> paginacaoConsulta2 = paginacaoConsulta;

        paginacaoConsulta2.Registros = await query.Skip((pg - 1) * qt).Take(qt).ToListAsync(cancelattionToken);

        paginacaoConsulta.Total = query.LongCount();

        return paginacaoConsulta;
    }
}
