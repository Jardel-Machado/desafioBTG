namespace DesafioBtg.Aplicacao.Transacoes.Servicos.Interface;

public interface IUnitOfWork : IDisposable
{
    void BeginTransaction();
    void Rollback();
    void Commit();
    void Limpar();
    void Flush();
}
