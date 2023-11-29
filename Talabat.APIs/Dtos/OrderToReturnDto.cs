using Talabat.Core.Entities.Order_Aggregate;

namespace Talabat.APIs.Dtos
{
    public class OrderToReturnDto
    {

        public int Id { get; set; }

        public string BuyerEmail { get; set; }

        public DateTimeOffset OrderDate { get; set; } 

        public OrderStatus Status { get; set; } 

        public AddressDto ShippingAddress { get; set; }


        public string ShortName { get; set; }

        public decimal Cost { get; set; }


        public ICollection<OrderItemDto> Items { get; set; } 

        public decimal SubTotal { get; set; }

        public decimal Total { get; set; }

        public string PaymentIntentId { get; set; } 

    }
}
