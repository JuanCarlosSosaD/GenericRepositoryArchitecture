using Everyware.GRDomain.Interfaces;
using Everyware.GRInfrastructure.Data;
using Microsoft.EntityFrameworkCore.Storage;

namespace Everyware.GRInfrastructure;
public class UnitOfWork : IUnitOfWork, IDisposable
{
    // Track whether Dispose has been called.
    private bool _Disposed = false;

    private readonly ApplicationDbContext _dbContext;

    public UnitOfWork(ApplicationDbContext dataContext)
    {
        _dbContext = dataContext;
    }

    public IDbContextTransaction CreateTransaction()
    {
        return _dbContext.Database.BeginTransaction();
    }

    public int SaveChanges()
    {
        return _dbContext.SaveChanges();
    }

    public void Dispose()
    {
        Dispose(true);

        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_Disposed)
        {
            if (disposing && _dbContext != null)
            {
                _dbContext.Dispose();
            }

            _Disposed = true;
        }
    }
}
