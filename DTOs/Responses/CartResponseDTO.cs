using CoffeeShop.DTOs.Request;

namespace CoffeeShop.DTOs.Responses
{
    public class CartResponseDTO
    {
        public Guid CartId { get; set; }
        public string? CustomerName { get; set; }
        public string CustomerPhone { get; set; }
        public int Table { get; set; }
        public decimal Total { get; set; }
        public List<CartDetailResponseDTO> CartDetails { get; set; }
    }
}
