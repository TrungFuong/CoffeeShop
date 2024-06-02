using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoffeeShop.Models
{
    public class Receipt
    {
        [Required]
        [Key]
        public Guid ReceiptId { get; set; }     
        [ForeignKey("EmployeeId")]
        public Guid EmployeeId { get; set; }
        public Employee Employee { get; set; }
        [ForeignKey("CustomerId")]
        public Guid CustomerId { get; set; }
        public Customer Customer { get; set; }
        [Required]
        public DateTime ReceiptDate { get; set; }
        [Required]
        public decimal ReceiptTotal { get; set; }
        public ICollection<ReceiptDetail> ReceiptDetails { get; set; }
    }
}
