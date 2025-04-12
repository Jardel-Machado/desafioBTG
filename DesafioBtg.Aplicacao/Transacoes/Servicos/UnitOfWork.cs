using DesafioBtg.Aplicacao.Transacoes.Servicos.Interface;
using NHibernate;

namespace DesafioBtg.Aplicacao.Transacoes;

public class UnitOfWork : IUnitOfWork, IDisposable
{
    private ISession session;

    private ITransaction transaction;

    public UnitOfWork(ISession session)
    {
        this.session = session;
    }

    public void BeginTransaction()
    {
        transaction = session.BeginTransaction();
    }

    public void Commit()
    {
        if (transaction != null && transaction.IsActive)
        {
            transaction.Commit();
        }
    }

    public void Rollback()
    {
        if (transaction != null && transaction.IsActive)
        {
            transaction.Rollback();
        }
    }

    public void Dispose()
    {
        if (transaction != null)
        {
            transaction.Dispose();
            transaction = null;
        }

        if (session != null && session.IsOpen)
        {
            session.Close();
            session = null;
        }
    }

    public void Limpar()
    {
        if (session != null)
        {
            session.Clear();
        }
    }

    public void Flush()
    {
        if (session != null)
        {
            session.Flush();
        }
    }
}
