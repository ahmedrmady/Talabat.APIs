using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Specifications;

namespace Talabat.Repositry.SpecificationsEvaluator
{
    internal class SpecifcationsEvalutor<TEntity> :BaseSpecifications<TEntity> where TEntity : BaseEntity
    {

        public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inputQuery, ISpecifications<TEntity> specs)
        {
            var query = inputQuery;

            // check if the where criteria is not null
            if (specs.Criteria is not null)
                query = query.Where(specs.Criteria);

            if (specs.OrderBy is not null)
                query = query.OrderBy(specs.OrderBy);

            else if (specs.OrderByDesc is not null)
                query = query.OrderByDescending(specs.OrderByDesc);

            if (specs.IsPaginationEnabled)
                query = query.Skip(specs.Skip).Take(specs.Take);

            // Aggregate includes to query
            query = specs.Includes.Aggregate(query, (currentQuery, Include) => currentQuery.Include(Include));

                return query;
        }

       

        
    }
}
