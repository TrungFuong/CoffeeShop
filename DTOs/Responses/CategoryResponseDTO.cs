namespace CoffeeShop.DTOs.Responses
{
    public class CategoryResponseDTO
    {
        public Guid CategoryId { get; set; }
        public string CategoryName { get; set; } = string.Empty;
        public bool IsDeleted { get; set; }
    }
}
