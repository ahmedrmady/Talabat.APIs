using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core;
using Talabat.Core.Entities;
using Talabat.Core.Repository.Contract;
using Talabat.Repositry.Data;

namespace Talabat.Repositry
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StoreContext _context;

        private Hashtable Repositories;

        public UnitOfWork(StoreContext context)
        {
            this._context = context;

            Repositories = new Hashtable();
        }


        public IGenericRepositry<Tentity> Repositry<Tentity>() where Tentity : BaseEntity
        {
            var key = typeof(Tentity).Name;

            if (!Repositories.ContainsKey(key))
            {
                var Repository = new GenericRepositry<Tentity>(_context);

                Repositories.Add(key, Repository);

            }

            return Repositories[key] as IGenericRepositry<Tentity> ;
        }

        public async Task<int> CompleteAsync()
        => await _context.SaveChangesAsync();

        public async ValueTask DisposeAsync()
        => await _context.DisposeAsync();
    }
}
