using CoffeeShop.Constants;
using System.ComponentModel.DataAnnotations;

namespace CoffeeShop.DTOs.Request
{
    public class ChangePasswordRequest
    {
        [Required]
        public string Username { get; set; } = string.Empty;

        [Required]
        public string OldPassword { get; set; } = string.Empty;

        [Required]
        [RegularExpression(RegexConstants.PASSWORD, ErrorMessage = ErrorMessage.ERROR_PASSWORD)]
        public string NewPassword { get; set; } = string.Empty;
        public string RefreshToken { get; set; } = string.Empty;
    }
}
