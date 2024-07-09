namespace CoffeeShop.DTOs.Request
{
    public class CheckinRecordRequestDTO
    {
        public DateTime CheckinTime { get; set; }
        public DateTime CheckoutTime { get; set; }
        public Guid EmployeeId { get; set; }
    }
}
