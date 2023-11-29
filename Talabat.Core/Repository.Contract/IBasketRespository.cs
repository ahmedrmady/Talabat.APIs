using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Core.Repository.Contract
{
    public interface IBasketRespository
    {
        public Task<CustmoerBasket?> GetBasketAsync(string Id);

        public Task<bool> DeleteBasketAsync(string Id);

        public Task<CustmoerBasket> UpdateOrAddBasketAsync(CustmoerBasket model);
    }
}
