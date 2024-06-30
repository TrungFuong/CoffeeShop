﻿namespace CoffeeShop.Models.Responses
{
    public class GeneralGetResponse
    {
        public bool Success { get; set; } = true;
        public string Message { get; set; } = string.Empty;
        public object? Data { get; set; } = null;
    }
}