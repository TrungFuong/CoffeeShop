using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoffeeShop.Models
{
    public class ReceiptDetail
    {
        [ForeignKey("ProductId")]
        [Required]
        public Guid ProductId { get; set; }
        public Product Product { get; set; }
        [ForeignKey("ReceiptId")]
        [Required]
        public Guid ReceiptId { get; set; }
        public Receipt Receipt { get; set; }
        [Required]
        public int ProductQuantity { get; set; }
        [Required]
        public decimal ProductPrice { get; set; }
    }
}
