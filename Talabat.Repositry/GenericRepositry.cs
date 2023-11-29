using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Repository.Contract;
using Talabat.Core.Specifications;
using Talabat.Repositry.Data;
using Talabat.Repositry.SpecificationsEvaluator;

namespace Talabat.Repositry
{
    public class GenericRepositry<T> : IGenericRepositry<T> where T : BaseEntity
    {
        private readonly StoreContext _storeContext;

        public GenericRepositry(StoreContext storeContext)
        {
            _storeContext = storeContext;
        }
        public async Task<T?> GetAsync(int id)
        {
            return await _storeContext.Set<T>().FindAsync(id);
        }
       
        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            //if(typeof(T)==typeof(Product))
            //return (IReadOnlyList<T>) await _storeContext.Set<Product>().Include(P=>P.Brand).Include(P=>P.Category).ToListAsync();
           
                return await _storeContext.Set<T>().ToListAsync();
        }

        public async Task<IReadOnlyList<T>> GetAllWithSpecAsync(ISpecifications<T> spec)
        {
            return await ApplaySpecifications(spec).ToListAsync();
        }

        public async Task<T?> GetWithSpecAsync(ISpecifications<T> spec)
        {
            return await ApplaySpecifications(spec).FirstOrDefaultAsync() as T;

        }
        public async Task<int> CountAsync(ISpecifications<T> spec)
        {
            return await ApplaySpecifications(spec).CountAsync();
        }

        private IQueryable<T> ApplaySpecifications(ISpecifications<T> spec)
        {
            return SpecifcationsEvalutor<T>.GetQuery(_storeContext.Set<T>(), spec);
        }

        public async Task AddAsync(T entity)
            =>  await _storeContext.AddAsync(entity);

        public void Update(T entity)
            => _storeContext.Update(entity);

        public void Delete(T entity)
            => _storeContext.Remove(entity);
    }
}
