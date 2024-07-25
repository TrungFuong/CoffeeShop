using CoffeeShop.DTOs;
using CoffeeShop.DTOs.Request;
using CoffeeShop.Models;
using CoffeeShop.UnitOfWork;

namespace CoffeeShop.Services.Implementations
{
    public class CartDetailService : ICartDetailService
    {
        private readonly IUnitOfWork _unitOfWork;
        public CartDetailService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task AddCartDetailAsync(Guid cartId, List<CartDetailRequestDTO> cartDetailRequestDTO)
        {
            var cartDetails = cartDetailRequestDTO.Select(dto => new CartDetail
            {
                CartId = cartId,
                ProductId = dto.ProductId,
                ProductQuantity = dto.ProductQuantity
            }).ToList();

            await _unitOfWork.CartDetailRepository.AddRangeAsync(cartDetails);
        }

        public async Task DeleteCartDetailAsync(Guid id)
        {
            var cartDetail = await _unitOfWork.CartDetailRepository.GetAllAsync(c => c.CartId == id);
            if (cartDetail == null)
            {
                throw new KeyNotFoundException("Không tìm thấy chi tiết giỏ hàng!");
            }
            foreach (var item in cartDetail)
            {
                _unitOfWork.CartDetailRepository.Delete(item);
            }
        }
    }
}
