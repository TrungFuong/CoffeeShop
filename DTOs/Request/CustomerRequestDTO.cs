namespace CoffeeShop.DTOs.Request
{
    public class CustomerRequestDTO
    {
        public Guid CustomerId { get; set; }
        public string? CustomerName { get; set; }
        public string? CustomerPhone { get; set; }
        public DateTime? CustomerBirthday { get; set; }
    }
}
