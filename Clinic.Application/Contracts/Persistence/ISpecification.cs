using System.Linq.Expressions;

namespace Clinic.Application.Contracts.Persistence
{
    /// <summary>
    /// Specification pattern interface for building complex queries.
    /// </summary>
    /// <typeparam name="T">Entity type</typeparam>
    public interface ISpecification<T>
    {
        Expression<Func<T, bool>>? Criteria { get; }
        List<Expression<Func<T, object>>> Includes { get; }
        List<string> IncludeStrings { get; }
        Expression<Func<T, object>>? OrderBy { get; }
        Expression<Func<T, object>>? OrderByDescending { get; }
        Expression<Func<T, object>>? GroupBy { get; }
        int Take { get; }
        int Skip { get; }
        bool IsPagingEnabled { get; }
    }
}

