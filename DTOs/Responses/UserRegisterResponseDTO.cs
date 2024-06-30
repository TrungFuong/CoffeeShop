using CoffeeShop.Models.Enums;

namespace CoffeeShop.DTOs.Responses
{
    public class UserRegisterResponseDTO
    {
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public string UserPosition { get; set; }
        public EnumRole Role { get; set; }
        public string PhoneNumber { get; set; }
    }
}
