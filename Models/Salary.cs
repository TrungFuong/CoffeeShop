using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoffeeShop.Models
{
    public class Salary
    {
        [Key]
        public int SalaryId { get; set; }
        [Required]
        public Guid EmployeeId { get; set; }
        [ForeignKey("PayrateId")]
        [Required]
        public int PayrateId { get; set; }
        [Required]
        public decimal TotalSalary { get; set; }
        public Employee Employee { get; set; }
        public PayRate PayRate { get; set; }
    }
}
