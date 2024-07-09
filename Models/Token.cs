using System.ComponentModel.DataAnnotations;

namespace CoffeeShop.Models
{
    public class Token
    {
        [Key]
        public long Id { get; set; }
        public Guid UserId { get; set; }
        public User? User { get; set; }
        public string HashToken { get; set; } = string.Empty;
    }
}
