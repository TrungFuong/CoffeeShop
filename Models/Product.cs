using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoffeeShop.Models
{
    public class Product
    {
        [Required]
        [Key]
        public Guid ProductId { get; set; }
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
        public Guid CategoryId { get; set; }
        public Category Category { get; set; }
        public ICollection<ReceiptDetail> ReceiptDetails { get; set; }
        //public IFormFile Image { get; set; }
        public bool IsDeleted { get; set; }
    }
}
