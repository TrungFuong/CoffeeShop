﻿namespace CoffeeShop.DTOs.Request
{
    public class CategoryRequestDTO
    {
        public string CategoryName { get; set; } = string.Empty;
        public bool IsDeleted { get; set; }
    }

}
