using System.ComponentModel.DataAnnotations;

namespace CoffeeShop.Models
{
    public class Category
    {
        [Required]
        [Key]
        public Guid CategoryId { get; set; }
        [Required]
        [MaxLength(255)]
        public string CategoryName { get; set; } =string.Empty;
        public bool IsDeleted { get; set; } = false;
        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
