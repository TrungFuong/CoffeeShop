﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoffeeShop.Models
{
    public class Cart
    {
        [Required]
        [Key]
        public Guid CartId { get; set; }
        [ForeignKey("CustomerId")]
        public Guid CustomerId { get; set; }
        public Customer Customer { get; set; }  
        [Required]
        public decimal Total { get; set; }
        public int Table { get; set; }
        public DateTime CartTime { get; set; }
        public ICollection<CartDetail> CartDetails{ get; set; }
    }
}
