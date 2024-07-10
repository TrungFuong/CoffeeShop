using System.ComponentModel.DataAnnotations;

namespace CoffeeShop.DTOs.Request
{
    public class UserLoginRequestDTO
    {
        [Required]
        public string Username { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; } = string.Empty;
    }
}
