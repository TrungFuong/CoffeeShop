using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace CoffeeShop.Models
{
    public class ProductImage
    {
        [Required]
        [Key]
        public int ProductImageId { get; set; }
        [Required]
        [MaxLength(255)]
        public string ProductImagePath { get; set; }
        [Required]
        [MaxLength(255)]
        public string ProductImageDescription { get; set; }
        [Required]
        [ForeignKey("ProductId")]
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}
