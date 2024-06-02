using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoffeeShop.Models
{
    public class Product
    {
        [Required]
        [Key]
        public int ProductId { get; set; }
        [Required]
        [MaxLength(255)]
        public string ProductName { get; set; }
        [Required]
        public decimal ProductPrice { get; set; }
        [Required]
        [MaxLength(255)]
        public string ProductDescription { get; set; }
        [Required]
        [ForeignKey("CategoryId")]
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public ICollection<ReceiptDetail> ReceiptDetails { get; set; }
        public ICollection<ProductImage> ProductImages { get; set; }
    }
}
