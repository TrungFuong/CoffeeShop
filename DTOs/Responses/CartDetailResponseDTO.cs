﻿namespace CoffeeShop.DTOs.Responses
{
    public class CartDetailResponseDTO
    {
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal ProductPrice { get; set; }
        public int ProductQuantity { get; set; }
    }
}
