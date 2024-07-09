namespace CoffeeShop.DTOs.Request
{
    public class ProductRequestDTO
    {
        public string ProductName { get; set; }
        public decimal ProductPrice { get; set; }
        public string ProductDescription { get; set; }
        public Guid CategoryId { get; set; }
        public string? ImageUrl { get; set; }
    }
}
