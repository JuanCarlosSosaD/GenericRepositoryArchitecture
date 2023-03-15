namespace Everyware.GRDomain.Interfaces;

public interface IRepositoryFactory
{
    IRepository<TEntity> GetRepository<TEntity>() where TEntity : class, IAggregateRoot;
    IUnitOfWork GetUnitOfWork();
}
