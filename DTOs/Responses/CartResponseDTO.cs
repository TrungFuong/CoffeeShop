using CoffeeShop.DTOs.Request;
using CoffeeShop.Models.Enums;

namespace CoffeeShop.DTOs.Responses
{
    public class CartResponseDTO
    {
        public Guid CartId { get; set; }
        public string? CustomerName { get; set; }
        public string CustomerPhone { get; set; }
        public string Table { get; set; }
        public DateTime CartTime { get; set; }
        public decimal Total { get; set; }
        public EnumCartStatus Status { get; set; }
        public List<CartDetailResponseDTO> CartDetails { get; set; }
    }
}
