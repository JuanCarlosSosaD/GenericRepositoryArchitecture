using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Everyware.GRDomain.Interfaces;


public interface IRepository<T> where T : class, IAggregateRoot
{
    Task<T> GetByIdAsync(long id);
    Task<T> GetByIdAsync(Guid id);
    Task<T> GetByIdAsync(int id);
    Task<T> GetByIdAsync(string id);
    Task<T> GetAsync(Expression<Func<T, bool>> filter, string sortExpression = null);
    Task<T> GetAsync(Func<IQueryable<T>, IQueryable<T>> transform, Expression<Func<T, bool>> filter, string sortExpression = null);
    Task<bool> ExistsAsync(Expression<Func<T, bool>> filter, string sortExpression = null);
    Task<T> AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(T entity);

    IEnumerable<T> GetAll(string sortExpression = null);
    IEnumerable<T> GetAll(Func<IQueryable<T>, IQueryable<T>> transform, Expression<Func<T, bool>> filter = null, string sortExpression = null);
    IEnumerable<TResult> GetAll<TResult>(Func<IQueryable<T>, IQueryable<TResult>> transform, Expression<Func<T, bool>> filter = null, string sortExpression = null);

    IEnumerable<T> GetAll(Expression<Func<T, bool>> filter, string sortExpression = null);
    Task<IEnumerable<T>> GetAllAsync(string sortExpression = null);
    Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> filter, string sortExpression = null);
    Task<IEnumerable<T>> GetAllAsync(Func<IQueryable<T>, IQueryable<T>> transform, Expression<Func<T, bool>> filter = null, string sortExpression = null);
    Task<IEnumerable<TResult>> GetAllAsync<TResult>(Func<IQueryable<T>, IQueryable<TResult>> transform, Expression<Func<T, bool>> filter = null, string sortExpression = null);

    Task<decimal> AverageAsync(Expression<Func<T, decimal>> property = null, Expression<Func<T, bool>> filter = null);
    Task<T> LastAsync(Expression<Func<T, bool>> filter, string sortExpression = null);
}
