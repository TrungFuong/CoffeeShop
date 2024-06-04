﻿namespace CoffeeShop.DTOs
{
    public class AddCategoryDTO
    {
        public string CategoryName { get; set; }
    }

    public class UpdateCategoryDTO
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set;}
    }

    public class DeleteCategoryDTO { 
        public int CategoryId { get; set; }
        public string CategoryName { get; set;}
    }

    public class GetCategoryDTO
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set;}
    }
}
