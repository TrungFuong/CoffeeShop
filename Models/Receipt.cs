using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace CoffeeShop.Models
{
    public class Receipt
    {
        [Required]
        [Key]
        public Guid ReceiptId { get; set; }
        [ForeignKey("UserId")]
        public Guid UserId { get; set; }
        public User User { get; set; }
        [ForeignKey("CustomerId")]
        public Guid? CustomerId { get; set; }
        public Customer Customer { get; set; }
        [Required]
        public DateTime ReceiptDate { get; set; }
        [Required]
        public decimal ReceiptTotal { get; set; }
        public int Table { get; set; }
        public ICollection<ReceiptDetail> ReceiptDetails { get; set; }
        public bool IsDeleted { get; set; }
        
    }
}
