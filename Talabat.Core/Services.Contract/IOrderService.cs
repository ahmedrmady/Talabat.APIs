using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities.Order_Aggregate;
using Talabat.Core.Entities.Order_Aggregation;

namespace Talabat.Core.Services.Contract
{
    public interface IOrderService
    {
        public Task<Order?> CreateOrderAsync(string buyerEmail,string basketId,Address shippeingAddress,int deliveryMethodId);

        public Task<IReadOnlyList<Order>> GetOrdersOfUSerAsync(string buyerEmail);

        public Task<Order> GetOrderByIdForUserAsync( int orderId,string buyerEmail);

        public Task<IReadOnlyList<DeliveryMethod>> GetDeliveyMehods();
    }
}
