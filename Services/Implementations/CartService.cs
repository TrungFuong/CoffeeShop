using CoffeeShop.DTOs;
using CoffeeShop.DTOs.Request;
using CoffeeShop.DTOs.Responses;
using CoffeeShop.Models;
using CoffeeShop.UnitOfWork;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace CoffeeShop.Services.Implementations
{
    public class CartService : ICartService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICustomerService _customerService;
        private readonly ICartDetailService _cartDetailService;
        private readonly IProductService _productService;

        public CartService(IUnitOfWork unitOfWork, ICartDetailService cartDetailService, IProductService productService, ICustomerService customerService)
        {
            _unitOfWork = unitOfWork;
            _cartDetailService = cartDetailService;
            _productService = productService;
            _customerService = customerService;
        }

        public async Task AddCartInfoAsync(Guid cartId, CartRequestDTO cartRequest)
        {
            decimal total = 0;

            foreach (var item in cartRequest.CartDetails)
            {
                var product = await _unitOfWork.ProductRepository.GetAsync(p => p.ProductId == item.ProductId);
                total += product.ProductPrice * item.ProductQuantity;
            }

            var cart = new Cart
            {
                CartId = cartId,
                CustomerName = cartRequest.CustomerName,
                CustomerPhone = cartRequest.CustomerPhone,
                CustomerBirthday = cartRequest.CustomerBirthday,
                Table = cartRequest.Table,
                Total = total
            };

            await _unitOfWork.CartRepository.AddAsync(cart);
        }

        public async Task<CartResponseDTO> AddCartAsync(CartRequestDTO cartRequestDTO)
        {
            using (var transaction = await _unitOfWork.BeginTransactionAsync())
            {
                try
                {
                    Task addCustomerTask = null;
                    var customerId = Guid.NewGuid();
                    if (cartRequestDTO.CustomerPhone != null)
                    {
                        var existingCustomer = await _customerService.GetCustomerDetailAsync(cartRequestDTO.CustomerPhone);
                        if (existingCustomer == null)
                        {
                            var customer = new CustomerRequestDTO
                            {
                                CustomerId = customerId,
                                CustomerName = cartRequestDTO.CustomerName,
                                CustomerPhone = cartRequestDTO.CustomerPhone,
                                CustomerBirthday = cartRequestDTO.CustomerBirthday
                            };
                            addCustomerTask = _customerService.AddCustomerAsync(customer);
                            cartRequestDTO.CustomerId = customerId;
                        }
                        else
                        {
                            cartRequestDTO.CustomerId = existingCustomer.CustomerId;
                        }
                    }

                    var cartId = Guid.NewGuid();
                    var addCartTask = AddCartInfoAsync(cartId, cartRequestDTO);
                    var addCartDetailTask = _cartDetailService.AddCartDetailAsync(cartId, cartRequestDTO.CartDetails);

                    var tasks = new List<Task> { addCartTask, addCartDetailTask };
                    if (addCustomerTask != null)
                    {
                        tasks.Add(addCustomerTask);
                    }

                    await Task.WhenAll(tasks);
                    await _unitOfWork.CommitAsync();
                    await transaction.CommitAsync();

                    return new CartResponseDTO
                    {
                        CartId = cartId,
                        Table = cartRequestDTO.Table,
                        Total = cartRequestDTO.Total
                    };
                }
                catch (Exception)
                {
                    await transaction.RollbackAsync();
                    throw new ArgumentException("Thêm giỏ hàng thất bại!");
                }
            }
        }
    }
}