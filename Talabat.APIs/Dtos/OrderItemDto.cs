namespace Talabat.APIs.Dtos
{
    public class OrderItemDto
    {
        public int Id { get; set; }

        public int ProductId { get; set; }

        public string ProductName { get; set; }

        public string ProductUrl { get; set; }
        public decimal Price { get; set; }

        public int Quantity { get; set; }
    }
}
