using CoffeeShop.Models.Enums;

namespace CoffeeShop.DTOs.Request
{
    public class UserUpdateRequestDTO
    {
        public EnumPostition UserPosition { get; set; }
        public EnumRole Role { get; set; }
        public string PhoneNumber { get; set; }
    }
}