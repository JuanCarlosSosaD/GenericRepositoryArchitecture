using Everyware.GRDomain.Interfaces;


namespace Everyware.GRInfrastructure.Repositories;

public class RepositoryFactory : IRepositoryFactory
{
    private readonly IServiceProvider _services;
    public RepositoryFactory() { }

    public RepositoryFactory(IServiceProvider services)
    {
        _services = services;
    }

    public IRepository<TEntity> GetRepository<TEntity>() where TEntity : class, IAggregateRoot
    {
        var instance = (IRepository<TEntity>)_services.GetService(typeof(IRepository<TEntity>));
        return instance;
    }

    public IUnitOfWork GetUnitOfWork()
    {
        //Import instance of T from the DI container
        var instance = (IUnitOfWork)_services.GetService(typeof(IUnitOfWork));

        return instance;
    }

}
