﻿using System.ComponentModel.DataAnnotations;

namespace CoffeeShop.Models
{
    public class RefreshToken
    {
        [Key]
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public string TokenHash { get; set; } = string.Empty;
        public string TokenSalt { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime ExpiredAt { get; set; } = DateTime.Now.AddMinutes(10);
        public User? User { get; set; }
    }
}
