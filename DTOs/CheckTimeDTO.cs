namespace CoffeeShop.DTOs
{
    public class CheckTimeDTO
    {
        public DateTime CheckinTime { get; set; }
        public DateTime CheckoutTime { get; set; }
        public Guid EmployeeId { get; set; }
    }
}
