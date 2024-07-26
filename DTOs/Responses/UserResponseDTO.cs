using CoffeeShop.Models.Enums;

namespace CoffeeShop.DTOs.Responses
{
    public class UserResponseDTO
    {
        public Guid UserId { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public EnumPostition UserPosition { get; set; }
        public EnumRole Role { get; set; }
        public string PhoneNumber { get; set; }
        public EnumGender Gender { get; set; }
        public bool IsFirstLogin { get; set; }
    }
}
