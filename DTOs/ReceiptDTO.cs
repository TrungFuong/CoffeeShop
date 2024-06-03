namespace CoffeeShop.DTOs
{
    public class ReceiptDTO
    {
        public Guid EmployeeId { get; set; }
        public Guid CustomerId { get; set; }
        public DateTime ReceiptDate { get; set; }
        public decimal ReceiptTotal { get; set; }
    }
}
