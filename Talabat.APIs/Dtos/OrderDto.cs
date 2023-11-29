

namespace Talabat.APIs.Dtos
{
    public class OrderDto
    {
        public string BuyerEmail { get; set; }

        public string BasketId { get; set; }

        public AddressDto ShippingAddress { get; set; }

        public int DeleviryMethod { get; set; }

    }
}
