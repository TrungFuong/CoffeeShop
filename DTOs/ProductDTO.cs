namespace CoffeeShop.DTOs
{
    public class ProductRequestDTO
    {
        public string ProductName { get; set; }
        public decimal ProductPrice { get; set; }
        public string ProductDescription { get; set; }
        public Guid CategoryId { get; set; }
        public string? ImageUrl { get; set; }
    }
    public class ProductResponseDTO
    {
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal ProductPrice { get; set; }
        public string ProductDescription { get; set; }
        public Guid CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string? ImageUrl { get; set; }
    }

    public class FileUpload
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public byte[] FileContent { get; set; }
    }
}
