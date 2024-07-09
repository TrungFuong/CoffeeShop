namespace CoffeeShop.DTOs.Responses
{
    public class ReceiptResponseDTO
    {
        public Guid ReceiptId { get; set; }
        public Guid EmployeeId { get; set; }
        public String FullName { get; set; }
        public Guid CustomerId { get; set; }
        public String CustomerPhone { get; set; }
        public DateTime ReceiptDate { get; set; }
        public int Table { get; set; }
        public decimal ReceiptTotal { get; set; }
    }
}
