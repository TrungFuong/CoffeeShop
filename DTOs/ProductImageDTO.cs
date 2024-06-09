namespace CoffeeShop.DTOs
{
    public class ProductImageRequestDTO
    {
        public string ProductImagePath { get; set; }
        public string ProductImageDescription { get; set; }
        public Guid ProductId { get; set; }
    }
    public class ProductImageResponseDTO
    {
        public Guid ProductImageId { get; set; }
        public string ProductImagePath { get; set; }
        public string ProductImageDescription { get; set; }
        public Guid ProductId { get; set; }
    }
}