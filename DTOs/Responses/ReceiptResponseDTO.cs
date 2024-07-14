using CoffeeShop.Models;
using Microsoft.Identity.Client;

namespace CoffeeShop.DTOs.Responses
{
    public class ReceiptResponseDTO
    {
        public Guid ReceiptId { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
        public String FullName { get; set; }
        public Guid? CustomerId { get; set; }
        public Customer? Customer { get; set; }
        public string CustomerPhone { get; set; }
        public DateTime ReceiptDate { get; set; }
        public int Table { get; set; }
        public decimal ReceiptTotal { get; set; }
        public ICollection<ReceiptDetail> ReceiptDetails { get; set; }
    }
}
