using System.ComponentModel.DataAnnotations;

namespace CoffeeShop.Models
{
    public class Category
    {
        [Required]
        [Key]
        public int CategoryId { get; set; }
        [Required]
        [MaxLength(255)]
        public string CategoryName { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}
