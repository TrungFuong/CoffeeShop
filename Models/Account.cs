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
        public Employee Employee { get; set; }
        public UserRole Role { get; set; }
        public bool IsDeleted { get; set; }

        public enum UserRole
        {
            Admin = 0,
            Employee = 1
        }
    }
}