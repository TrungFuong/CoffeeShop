using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoffeeShop.Models
{
    public class Account
    {
        [Required]
        [Key]
        public Guid AccountId { get; set; }
        [Required]
        [MaxLength(255)]
        public string AccountUsername { get; set; }
        [Required]
        [MaxLength(255)]
        public string AccountPassword { get; set; }
        [Required]
        [ForeignKey("RoleId")]
        public int RoleId { get; set; }
        [Required]
        public Role Role { get; set; }
        [Required]
        public Employee Employee { get; set; }
    }
}