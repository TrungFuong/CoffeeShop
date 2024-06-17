using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoffeeShop.Models
{
    public class Salary
    {
        [Key]
        public Guid SalaryId { get; set; }
        [Required]
        public Guid UserId { get; set; }
        [ForeignKey("PayrateId")]
        [Required]
        public Guid PayrateId { get; set; }
        [Required]
        public decimal TotalSalary { get; set; }
        public User User { get; set; }
        public PayRate PayRate { get; set; }

        public bool IsDeleted { get; set; }
    }
}
