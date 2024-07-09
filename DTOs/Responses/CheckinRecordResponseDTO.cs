namespace CoffeeShop.DTOs.Responses
{
    public class CheckinRecordResponseDTO
    {
        public DateTime CheckinTime { get; set; }
        public DateTime CheckoutTime { get; set; }
        public Guid EmployeeId { get; set; }
        public string FullName { get; set; }
    }
}
