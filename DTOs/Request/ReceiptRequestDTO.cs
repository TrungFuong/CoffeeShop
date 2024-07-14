using Microsoft.Identity.Client;

namespace CoffeeShop.DTOs.Request
{
    public class ReceiptRequestDTO
    { 
        public Guid UserId { get; set; }
        public Guid? CustomerId { get; set; }
        public DateTime ReceiptDate { get; set; }
        public decimal ReceiptTotal { get; set; }
        public int Table { get; set; }
        public List<ReceiptDetailDTO> receiptDetailDTOs { get; set; }
    }
}
