using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core;
using Talabat.Core.Entities;
using Talabat.Core.Entities.Order_Aggregate;
using Talabat.Core.Entities.Order_Aggregation;
using Talabat.Core.Repository.Contract;
using Talabat.Core.Services.Contract;
using Talabat.Core.Specifications.Order_Specs;

namespace Talabat.Service
{
    public class OrderService : IOrderService
    {
        private readonly IBasketRespository _basketRespository;

        private readonly IUnitOfWork _unitOfWork;

        public OrderService(
            IBasketRespository basketRespository,
           IUnitOfWork unitOfWork
            )
        {
            _basketRespository = basketRespository;
            _unitOfWork = unitOfWork;
        }
        public async Task<Order?> CreateOrderAsync(string buyerEmail, string basketId, Address shippeingAddress, int deliveryMethodId)
        {
            //1 get the basket from basket repo 

            var basket = await _basketRespository.GetBasketAsync(basketId);

            //2- get Selected Items in basket  from Products Repo

            var orderItems = new List<OrderItem>();
            if (basket?.Items?.Count > 0)
            {
                var prodictRepo =  _unitOfWork.Repositry<Product>();
                foreach (var item in basket.Items)
                {
                    //get the product 
                    var product =await  prodictRepo.GetAsync(item.Id);

                    //create productitemordered object
                    var productItemOrderdDetails = new ProductItemOrdered()
                    {
                        ProductId = item.Id,
                        ProductName = product.Name,
                        ProductUrl = product.PictureUrl
                    };

                    //create orderItem Object
                    var orderItem = new OrderItem(productItemOrderdDetails, product.Price, item.Quintity);

                    // add it to OrderItemsList

                    orderItems.Add(orderItem);
                }


            }
            // 3- calculate SubTotal

            var subTotal = orderItems.Sum(item => item.Price * item.Quantity);


            // 4- Get DeliveryMethod From DeliveyMethods Repo

            var deliveryMethod = await _unitOfWork.Repositry<DeliveryMethod>().GetAsync(deliveryMethodId);

            // 5- Create Order

            var Order = new Order(buyerEmail, shippeingAddress, deliveryMethod, orderItems, subTotal);

            //6 - Save To DataBase
            await _unitOfWork.Repositry<Order>().AddAsync(Order);

           var Result = await _unitOfWork.CompleteAsync();

            if (Result <= 0) return null;
            
            return Order;
        }

        public Task<IReadOnlyList<DeliveryMethod>> GetDeliveyMehods()
        => _unitOfWork.Repositry<DeliveryMethod>().GetAllAsync();

        public async Task<Order> GetOrderByIdForUserAsync(int orderId, string buyerEmail)
        {
            var orderSpecs = new OrderSpecifiactions(orderId, buyerEmail);
            var order = await _unitOfWork.Repositry<Order>().GetWithSpecAsync(orderSpecs);
            return order;
        }

        public async Task<IReadOnlyList<Order>> GetOrdersOfUSerAsync(string buyerEmail)
        {
            var orderSpecs = new OrderSpecifiactions(buyerEmail);

            var orders = await _unitOfWork.Repositry<Order>().GetAllWithSpecAsync(orderSpecs);

            return orders;
        }
    }
}
