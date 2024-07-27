using Microsoft.Identity.Client;

namespace CoffeeShop.DTOs.Request
{
    public class ReceiptRequestDTO
    { 
        public Guid UserId { get; set; }
        public Guid? CustomerId { get; set; }
        public string? CustomerName { get; set; }
        public string? CustomerPhone { get; set; }
        public DateTime? CustomerBirthday { get; set; }
        public DateTime ReceiptDate { get; set; } = DateTime.Now;
        public decimal ReceiptTotal { get; set; }
        public string Table { get; set; }
        public List<ReceiptDetailDTO> receiptDetailDTOs { get; set; }
    }
}
