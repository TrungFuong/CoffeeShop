using System.ComponentModel.DataAnnotations;

namespace CoffeeShop.Models
{
    public class Customer
    {
        [Required]
        [Key]
        public Guid CustomerId { get; set; } = Guid.NewGuid();
        [Required]
        [MaxLength(255)]
        public string? CustomerName { get; set; }
        [Required]
        [MaxLength(255)]
        public string? CustomerPhone { get; set; }
        [Required]
        [MaxLength(255)]
        public DateTime CustomerBirthday { get; set; }
        public bool IsDeleted { get; set; }
        public ICollection<Receipt> Receipts{ get; set; }
    }
}
