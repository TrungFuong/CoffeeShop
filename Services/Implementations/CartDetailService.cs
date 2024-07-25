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
    }
}
