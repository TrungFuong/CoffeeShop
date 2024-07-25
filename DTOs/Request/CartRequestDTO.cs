using CoffeeShop.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoffeeShop.DTOs.Request
{
    public class CartRequestDTO
    {
        public Guid CartId { get; set; }
        public Guid CustomerId { get; set; }
        public string? CustomerName { get; set; }
        public string CustomerPhone { get; set; }
        public DateTime CustomerBirthday { get; set; }
        [Required]
        public int Table { get; set; }
        public decimal Total { get; set; }
        public List<CartDetailRequestDTO> CartDetails{ get; set; }
    }
}
