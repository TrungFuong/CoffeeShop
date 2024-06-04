namespace CoffeeShop.DTOs
{
    public class AddProductImageDTO
    {
        public string ProductImagePath { get; set; }
        public string ProductImageDescription { get; set; }
        public int ProductId { get; set; }
    }

    public class UpdateProductImageDTO
    {
        public string ProductImagePath { get; set; }
        public string ProductImageDescription { get; set; }
        public int ProductId { get; set; }
    }

    public class GetProductImageDTO
    {
        public int ProductImageId { get; set; }
        public string ProductImagePath { get; set; }
        public string ProductImageDescription { get; set; }
        public int ProductId { get; set; }
    }
}