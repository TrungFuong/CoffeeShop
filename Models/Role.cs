using System.ComponentModel.DataAnnotations;

namespace CoffeeShop.Models
{
    public class Role
    {
        [Required]
        [Key]
        public int RoleId { get; set; }
        [Required]
        [MaxLength(255)]
        public string RoleName { get; set; }
        [Required]
        public ICollection<Account> Accounts { get; set; }
    }
}
