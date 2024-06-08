namespace CoffeeShop.DTOs
{
    public class ReceiptResponseDTO
    {
        public Guid ReceiptId { get; set; }
        public Guid EmployeeId { get; set; }
        public Guid CustomerId { get; set; }
        public DateTime ReceiptDate { get; set; }
        public decimal ReceiptTotal { get; set; }
    }
    public class ReceiptRequestDTO
    {
        public Guid EmployeeId { get; set; }
        public Guid CustomerId { get; set; }
        public decimal ReceiptTotal { get; set; }
    }
}
