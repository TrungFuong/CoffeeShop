using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography.Pkcs;

namespace CoffeeShop.Models
{
    public class Customer
    {
        [Required]
        [Key]
        public Guid CustomerId { get; set; }
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
        public ICollection<Cart> Carts { get; set; }
        public ICollection<Receipt> Receipts{ get; set; }
    }
}
