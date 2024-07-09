using CoffeeShop.Models.Enums;

namespace CoffeeShop.DTOs.Responses
{
    public class UserResponseDTO
    {
        public string Username { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public DateOnly DateOfBirth { get; set; }
        public EnumPostition UserPosition { get; set; } = EnumPostition.PhucVu;
        public string Role { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public EnumGender Gender { get; set; }
    }
}
