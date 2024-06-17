using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoffeeShop.Models
{
    public class User
    {
        [Required]
        [Key]
        public Guid UserId { get; set; }
        [Required]
        [MaxLength(255)]
        public string Username { get; set; }
        //[Required]
        [MaxLength(255)]
        public string? FirstName { get; set; }
                //[Required]
        [MaxLength(255)]
        public string? LastName { get; set; }
        //[Required]
        [MaxLength(255)]
        public string? HashPassword { get; set; }
        //[Required]
        [MaxLength(255)]
        public string? Salt { get; set; }
        //[Required]
        [MaxLength(255)]
        public string? UserPosition { get; set; }
        //[Required]
        [Column(TypeName = "tinyint")]
        public int? UserWorkingHour { get; set; }
        public ICollection<Receipt>? Receipts { get; set; }
        public ICollection<CheckTime>? CheckTimes { get; set; }
        public Salary? Salary { get; set; }
        public bool IsDeleted { get; set; }
        public Role Role { get; set; }
    }
    public enum Role
    {
        Employee = 0,
        Owner = 1
    }
}
