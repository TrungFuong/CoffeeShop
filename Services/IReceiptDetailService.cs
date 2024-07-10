using CoffeeShop.DTOs;

namespace CoffeeShop.Services
{
    public interface IReceiptDetailService
    {
        void AddReceiptDetailAsync(ReceiptDetailDTO receiptDetailDTO);
    }
}
