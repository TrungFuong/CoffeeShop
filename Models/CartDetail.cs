using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoffeeShop.Models
{
    public class CartDetail
    {
        [ForeignKey("ProductId")]
        [Required]
        public Guid ProductId { get; set; }
        public Product Product { get; set; }
        [ForeignKey("CartId")]
        [Required]
        public Guid CartId { get; set; }
        public Cart Cart { get; set; }
        [Required]
        public int ProductQuantity { get; set; }
    }
}
