namespace CoffeeShop.DTOs.Responses
{
    public class CustomerResponseDTO
    {
        public Guid CustomerId { get; set; }
        public string? CustomerName { get; set; }
        public string? CustomerPhone { get; set; }
        public DateOnly? CustomerBirthday { get; set; }
        public IEnumerable<ReceiptResponseDTO> Receipts { get; set; }
    }
}
