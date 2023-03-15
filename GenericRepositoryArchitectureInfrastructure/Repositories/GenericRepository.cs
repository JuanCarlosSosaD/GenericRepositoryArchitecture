using Everyware.GRDomain.Interfaces;
using Everyware.GRInfrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Everyware.GRInfrastructure.Repositories;


public class GenericRepository<T> : IReadRepository<T>, IRepository<T> where T : class, IAggregateRoot
{
    private readonly ApplicationDbContext _dbContext;

    public GenericRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public T GetById(int id)
    {
        return _dbContext.Set<T>().Find(id);
    }

    public Task<T> GetByIdAsync(long id)
    {
        return _dbContext.Set<T>().FindAsync(id).AsTask();
    }

    public Task<T> GetByIdAsync(Guid id)
    {
        return _dbContext.Set<T>().FindAsync(id).AsTask();
    }

    public Task<T> GetByIdAsync(int id)
    {
        return _dbContext.Set<T>().FindAsync(id).AsTask();
    }
    public Task<T> GetByIdAsync(string id)
    {
        return _dbContext.Set<T>().FindAsync(id).AsTask();
    }

    public async Task<T> GetAsync(Expression<Func<T, bool>> filter, string sortExpression = null)
    {
        var dbset = _dbContext.Set<T>();
        var query = await dbset.AsNoTracking().FirstOrDefaultAsync(filter);
        return query;
    }

    public async Task<T> GetAsync(Func<IQueryable<T>, IQueryable<T>> transform, Expression<Func<T, bool>> filter, string sortExpression = null)
    {
        var dbset = _dbContext.Set<T>().AsNoTracking().Where(filter);
        return await transform(dbset).FirstOrDefaultAsync();
    }

    public async Task<bool> ExistsAsync(Expression<Func<T, bool>> filter, string sortExpression = null)
    {
        var dbset = _dbContext.Set<T>();
        var query = await dbset.AsNoTracking().AnyAsync(filter);
        return query;
    }
    public IEnumerable<T> GetAll(string sortExpression = null)
    {
        var dbset = _dbContext.Set<T>();
        var query = dbset.AsNoTracking();
        return query.ToList();
    }
    public IEnumerable<T> GetAll(Func<IQueryable<T>, IQueryable<T>> transform, Expression<Func<T, bool>> filter = null, string sortExpression = null)
    {
        var dbset = _dbContext.Set<T>();
        var query = filter == null ? dbset.AsNoTracking() : dbset.AsNoTracking().Where(filter);
        var notSortedResults = transform(query);
        return notSortedResults.ToList();
    }
    public IEnumerable<TResult> GetAll<TResult>(Func<IQueryable<T>, IQueryable<TResult>> transform, Expression<Func<T, bool>> filter = null, string sortExpression = null)
    {
        var dbset = _dbContext.Set<T>();
        var query = filter == null ? dbset.AsNoTracking() : dbset.AsNoTracking().Where(filter);
        var notSortedResults = transform(query);
        return notSortedResults.ToList();
    }
    public IEnumerable<T> GetAll(Expression<Func<T, bool>> filter, string sortExpression = null)
    {
        var dbset = _dbContext.Set<T>();
        var query = dbset.AsNoTracking().Where(filter);
        return query.ToList();
    }

    public async Task<IEnumerable<T>> GetAllAsync(string sortExpression = null)
    {
        var dbset = _dbContext.Set<T>();
        var query = dbset.AsNoTracking();
        return await query.ToListAsync();
    }

    public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> filter, string sortExpression = null)
    {
        var dbset = _dbContext.Set<T>();
        var query = dbset.AsNoTracking().Where(filter);
        return await query.ToListAsync();
    }

    public async Task<IEnumerable<T>> GetAllAsync(Func<IQueryable<T>, IQueryable<T>> transform, Expression<Func<T, bool>> filter = null, string sortExpression = null)
    {
        var dbset = _dbContext.Set<T>();
        var query = filter == null ? dbset.AsNoTracking() : dbset.AsNoTracking().Where(filter);
        var notSortedResults = transform(query);
        return await notSortedResults.ToListAsync();
    }

    public async Task<IEnumerable<TResult>> GetAllAsync<TResult>(Func<IQueryable<T>, IQueryable<TResult>> transform, Expression<Func<T, bool>> filter = null, string sortExpression = null)
    {
        var dbset = _dbContext.Set<T>();
        var query = filter == null ? dbset.AsNoTracking() : dbset.AsNoTracking().Where(filter);
        var notSortedResults = transform(query);
        return await notSortedResults.ToListAsync();
    }

    public async Task<T> AddAsync(T entity)
    {
        await _dbContext.Set<T>().AddAsync(entity);
        await _dbContext.SaveChangesAsync();

        return entity;
    }
    public Task UpdateAsync(T entity)
    {
        _dbContext.Entry(entity).State = EntityState.Modified;
        return _dbContext.SaveChangesAsync();
    }

    public Task DeleteAsync(T entity)
    {
        _dbContext.Set<T>().Remove(entity);
        return _dbContext.SaveChangesAsync();
    }


    public async Task<decimal> AverageAsync(Expression<Func<T, decimal>> property = null, Expression<Func<T, bool>> filter = null)
    {
        var dbset = _dbContext.Set<T>();
        var query = filter == null
            ? await dbset.AsNoTracking().AverageAsync(property)
            : (await dbset.AsNoTracking().Where(filter).ToListAsync()).Count > 0 ?
                await dbset.AsNoTracking().Where(filter).AverageAsync(property) : 0;
        return query;
    }

    public async Task<T> LastAsync(Expression<Func<T, bool>> filter, string sortExpression = null)
    {
        var dbset = _dbContext.Set<T>();
        var query = await dbset.AsNoTracking().OrderByDescending(x => x).FirstOrDefaultAsync(filter);
        return query;
    }


}
