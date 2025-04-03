using Core.Entites;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Specification;

namespace Repo.Data.Spec
{
    public class Spec_Evaluator<TEntity> where TEntity : BaseEntity
    {
        public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inner, ISpec<TEntity> spec)
        {
            if (inner == null)
                throw new ArgumentNullException(nameof(inner));

            if (spec == null)
                throw new ArgumentNullException(nameof(spec));

            var query = inner;

            if (spec.Critirea != null)
            {
                query = query.Where(spec.Critirea);
            }



            if (spec.OrderBy != null)
            {
                query = query.OrderBy(spec.OrderBy);
            }

            if (spec.OrderByDesc != null)
            {
                query = query.OrderByDescending(spec.OrderByDesc);
            }

            if (spec.Includes != null)
            {
                query = spec.Includes.Aggregate(query, (currentQuery, include) => currentQuery.Include(include));
            }

            Console.WriteLine($"Generated Query: {query.ToQueryString()}");

            return query;
        }
    }
}
