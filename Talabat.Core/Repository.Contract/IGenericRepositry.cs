using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Specifications;

namespace Talabat.Core.Repository.Contract
{
    public interface IGenericRepositry<T> where T : BaseEntity
    {
        Task<IReadOnlyList<T>> GetAllAsync();

        Task<T?> GetAsync(int id);



        Task<T?> GetWithSpecAsync(ISpecifications<T> spec);

        Task<IReadOnlyList<T>> GetAllWithSpecAsync(ISpecifications<T> spec);

        Task<int> CountAsync(ISpecifications<T> spec);

        Task AddAsync(T entity);

        void Update(T entity);

        void Delete(T entity);

    }
}
