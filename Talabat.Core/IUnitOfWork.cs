using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Repository.Contract;

namespace Talabat.Core
{
    public  interface IUnitOfWork:IAsyncDisposable
    {
        public IGenericRepositry<Tentity> Repositry<Tentity>() where Tentity : BaseEntity;

        public Task<int> CompleteAsync();
    }
}
