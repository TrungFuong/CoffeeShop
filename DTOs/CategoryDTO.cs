namespace CoffeeShop.DTOs
{
    public class CategoryRequestDTO
    {
        public string CategoryName { get; set; }
    }

    public class CategoryResponseDTO
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set;}
    }
}
