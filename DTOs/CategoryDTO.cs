namespace CoffeeShop.DTOs
{
    public class CategoryRequestDTO
    {
        public string CategoryName { get; set; }
        public bool IsDeleted { get; set; }
    }

    public class CategoryResponseDTO
    {
        public Guid CategoryId { get; set; }
        public string CategoryName { get; set;}
        public bool IsDeleted { get; set; }
    }
}
