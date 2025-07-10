using System.Linq.Expressions;

namespace Clinic.Application.Contracts.Persistence
{
    /// <summary>
    /// Generic repository interface for common CRUD operations.
    /// </summary>
    /// <typeparam name="T">Entity type</typeparam>
    public interface IGenericRepository<T> where T : class
    {
        Task<IReadOnlyList<T>> GetAllAsync();
        Task<T?> GetByIdAsync(int id);
        Task<T> CreateAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>>? predicate = null,
                                       Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
                                       string includeString = "",
                                       bool disableTracking = true);
        Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>>? predicate = null,
                                       Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
                                       List<Expression<Func<T, object>>>? includes = null,
                                       bool disableTracking = true);
        Task<T?> GetEntityWithSpec(ISpecification<T> spec);
        Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec);
        Task<int> CountAsync(ISpecification<T> spec);
        Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate);
    }
}

