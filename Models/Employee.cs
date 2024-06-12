using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoffeeShop.Models
{
    public class Employee
    {
        [Required]
        [Key]
        public Guid EmployeeId { get; set; }
        [Required]
        [MaxLength(255)]
        public string EmployeeName { get; set; }
        [Required]
        [MaxLength(255)]
        public string EmployeePosition { get; set; }
        [Required]
        [Column(TypeName = "tinyint")]
        public int EmployeeWorkingHour { get; set; }
        [Required]
        [ForeignKey("AccountId")]
        public Guid AccountId { get; set; }
        [Required]
        public Account Account { get; set; }
        public ICollection<Receipt>? Receipts { get; set; }
        public ICollection<CheckTime>? CheckTimes { get; set; }
        public Salary? Salary { get; set; }
        public bool IsDeleted { get; set; }
    }
}
