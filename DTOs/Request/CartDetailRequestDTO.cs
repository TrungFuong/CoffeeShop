namespace CoffeeShop.DTOs.Request
{
    public class CartDetailRequestDTO
    {
        public Guid? CartId { get; set; }
        public Guid ProductId { get; set; }
        public int ProductQuantity { get; set; }
    }
}
