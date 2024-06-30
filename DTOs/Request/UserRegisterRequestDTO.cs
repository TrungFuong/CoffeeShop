using CoffeeShop.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace CoffeeShop.DTOs.Request
{
    public class UserRegisterRequestDTO
    {
        public string Username { get; set; }
        [Required]
        [RegularExpression(@"^[a-zA-Z]+([a-zA-Z ]*[a-zA-Z])?$", ErrorMessage = "First name can only contain letters and spaces, and cannot start or end with a space.")]
        public string FirstName { get; set; } = string.Empty;
        [Required]
        [RegularExpression(@"^[a-zA-Z]+([a-zA-Z ]*[a-zA-Z])?$", ErrorMessage = "Last name can only contain letters and spaces, and cannot start or end with a space.")]
        public string LastName { get; set; } = string.Empty;
        [Required]
        public DateOnly DateOfBirth { get; set; }
        [Required]
        public string UserPosition { get; set; }
        [Required]
        public EnumRole Role { get; set; } = EnumRole.Employee;
        [Required]
        public EnumGender Gender { get; set; }
        [Required]
        public string PhoneNumber { get; set; }

    }
}
