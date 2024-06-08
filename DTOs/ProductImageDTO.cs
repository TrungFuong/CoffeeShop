namespace CoffeeShop.DTOs
{
    public class ProductImageRequestDTO
    {
        public string ProductImagePath { get; set; }
        public string ProductImageDescription { get; set; }
        public int ProductId { get; set; }
    }
    public class ProductImageResponseDTO
    {
        public int ProductImageId { get; set; }
        public string ProductImagePath { get; set; }
        public string ProductImageDescription { get; set; }
        public int ProductId { get; set; }
    }
}