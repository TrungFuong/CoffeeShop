using System.ComponentModel.DataAnnotations;

namespace CoffeeShop.DTOs.Request
{
    public class UserLoginRequestDTOcs
    {
        [Required]
        public string Username { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; } = string.Empty;
    }
}
