namespace CoffeeShop.Models.Responses
{
    public class GeneralBoolResponse
    {
        public bool Success { get; set; } = true;
        public string Message { get; set; } = string.Empty;
    }
}
