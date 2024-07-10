namespace CoffeeShop.DTOs
{
    public class ReceiptDetailDTO
    {
        public Guid ReceiptId { get; set; }
        public Guid ProductId { get; set; }
        public int ProductQuantity { get; set; }
    }
}
