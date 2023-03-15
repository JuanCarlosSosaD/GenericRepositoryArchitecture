using Microsoft.EntityFrameworkCore.Storage;

namespace Everyware.GRDomain.Interfaces;
public interface IUnitOfWork
{
    IDbContextTransaction CreateTransaction();
    int SaveChanges();
}
