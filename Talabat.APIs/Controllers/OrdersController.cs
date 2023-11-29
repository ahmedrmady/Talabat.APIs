using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Services;
using System.Security.Claims;
using Talabat.APIs.Dtos;
using Talabat.APIs.Errors;
using Talabat.Core.Entities.Order_Aggregate;
using Talabat.Core.Entities.Order_Aggregation;
using Talabat.Core.Services.Contract;

namespace Talabat.APIs.Controllers
{

    public class OrdersController : BaseApiController
    {
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;

        public OrdersController(IOrderService orderService, IMapper mapper)
        {
            _orderService = orderService;
            _mapper = mapper;
        }


        [HttpPost]
        public async Task<ActionResult<Order>> CreateOrderAsync(OrderDto dto)
        {
            var address = _mapper.Map<AddressDto, Address>(dto.ShippingAddress);

            var order = await _orderService.CreateOrderAsync(dto.BuyerEmail, dto.BasketId, address, dto.DeleviryMethod);

            if (order is null) return NotFound(new ApiResponse(400));


            return Ok(order);
        }


        [HttpGet] //GET:  /api/Orders?buyerEmail=ahmed.naser@link.com
        public async Task<ActionResult<IReadOnlyList<Order>>> GetOrdersForUser(string buyerEmail)
        {

            var orders = await _orderService.GetOrdersOfUSerAsync(buyerEmail);
            if (orders is null) return BadRequest(new ApiResponse(400));
            return Ok(orders);

        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetOrderForUser(int id, string buyerEmail)
        {
            var order = await _orderService.GetOrderByIdForUserAsync(id, buyerEmail);
            if (order is not null) return NotFound(new ApiResponse(404));

            return Ok(order);
        }

        [HttpGet("DeleveryMethods")] // GET : /api/Orders/DeleveryMethods
        public async Task<ActionResult<IReadOnlyList<DeliveryMethod>>> GetDeliveryMethods ()
        {
            var deliveryMethods = await _orderService.GetDeliveyMehods();

            return Ok(deliveryMethods);
        }
        

        
    }
}
