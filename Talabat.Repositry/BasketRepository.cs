using Microsoft.VisualBasic;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Repository.Contract;

namespace Talabat.Repositry
{
    public class BasketRepository : IBasketRespository
    {
        private readonly IDatabase database;
        public BasketRepository(IConnectionMultiplexer connection)
        {
            database = connection.GetDatabase();
        }
        public async Task<bool> DeleteBasketAsync(string Id)
        {
            return await database.KeyDeleteAsync(Id);
        }

        public async Task<CustmoerBasket?> GetBasketAsync(string Id)
        {

            var basket = await database.StringGetAsync(Id);
            return basket.IsNull ? null : JsonSerializer.Deserialize<CustmoerBasket>(basket);

        }

        public async Task<CustmoerBasket> UpdateOrAddBasketAsync(CustmoerBasket model)
        {
            var updatedOrAddBasket = await database.StringSetAsync(model.Id,JsonSerializer.Serialize(model),TimeSpan.FromDays(2));

            if (updatedOrAddBasket is false) return null;
            return await GetBasketAsync(model.Id);
        }
    }
}
