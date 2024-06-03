namespace CoffeeShop.DTOs
{
    public class ReceiptDetailDTO
    {
        public Guid ReceiptId {  get; set; }
        public int ProductId { get; set; }
        public int ProductQuantity { get; set; }
        public decimal ProductPrice { get; set; }
    }
}
