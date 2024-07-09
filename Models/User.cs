using CoffeeShop.Models.Enums;
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
        public Guid UserId { get; set; } = Guid.NewGuid();
        [Required]
        [MaxLength(255)]
        public string Username { get; set; } = string.Empty;
        //[Required]
        [MaxLength(255)]
        public string FirstName { get; set; } = string.Empty;
        //[Required]
        [MaxLength(255)]
        public string LastName { get; set; } = string.Empty;
        [Required]
        public DateOnly DateOfBirth { get; set; }
        public EnumGender Gender { get; set; }
        public string PhoneNumber { get; set; } 
        public EnumRole Role { get; set; }
        //[Required]
        [MaxLength(255)]
        public string? HashPassword { get; set; }
        //[Required]
        [MaxLength(255)]
        public string? Salt { get; set; }
        //[Required]
        [MaxLength(255)]
        public EnumPostition UserPosition { get; set; }
        //[Required]
        [Column(TypeName = "tinyint")]
        public int? UserWorkingHour { get; set; }
        public ICollection<Receipt>? Receipts { get; set; }
        public ICollection<CheckTime>? CheckTimes { get; set; }
        public Salary? Salary { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsFirstLogin { get; set; } = true;
    }
}
