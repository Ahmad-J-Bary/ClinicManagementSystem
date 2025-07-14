using Clinic.Application.Contracts.Persistence;
using Clinic.Domain;
using Microsoft.EntityFrameworkCore;

namespace Clinic.Persistence.Repositories
{
    /// <summary>
    /// Evaluates specifications and applies them to Entity Framework queries.
    /// Implements the Specification pattern for complex query logic.
    /// </summary>
    /// <typeparam name="T">The entity type</typeparam>
    public static class SpecificationEvaluator<T> where T : BaseEntity
    {
        /// <summary>
        /// Applies a specification to a queryable and returns the modified query.
        /// </summary>
        /// <param name="inputQuery">The input queryable</param>
        /// <param name="specification">The specification to apply</param>
        /// <returns>The modified queryable with specification applied</returns>
        public static IQueryable<T> GetQuery(IQueryable<T> inputQuery, ISpecification<T> specification)
        {
            var query = inputQuery;

            // Apply criteria (WHERE clause)
            if (specification.Criteria != null)
            {
                query = query.Where(specification.Criteria);
            }

            // Apply includes
            query = specification.Includes.Aggregate(query, (current, include) => current.Include(include));

            // Apply string-based includes
            query = specification.IncludeStrings.Aggregate(query, (current, include) => current.Include(include));

            // Apply ordering
            if (specification.OrderBy != null)
            {
                query = query.OrderBy(specification.OrderBy);
            }
            else if (specification.OrderByDescending != null)
            {
                query = query.OrderByDescending(specification.OrderByDescending);
            }

            // Apply paging
            if (specification.IsPagingEnabled)
            {
                query = query.Skip(specification.Skip).Take(specification.Take);
            }

            return query;
        }
    }
}

