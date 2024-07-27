﻿using CoffeeShop.DTOs;
using CoffeeShop.DTOs.Request;
using CoffeeShop.DTOs.Responses;
using CoffeeShop.Models;
using CoffeeShop.UnitOfWork;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Linq.Expressions;
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
                CartTime = DateTime.Now,
                CustomerId = cartRequest.CustomerId,
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
                    decimal total = 0;
                    foreach (var item in cartRequestDTO.CartDetails)
                    {
                        var product = await _unitOfWork.ProductRepository.GetAsync(p => p.ProductId == item.ProductId);
                        total += product.ProductPrice * item.ProductQuantity;
                    }

                    var customerId = Guid.NewGuid();
                    Task addCustomerTask = null;

                    if (!string.IsNullOrEmpty(cartRequestDTO.CustomerPhone))
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
                    var cart = await _unitOfWork.CartRepository.GetAsync(c => c.Table == cartRequestDTO.Table);

                    if (cart == null)
                    {
                        cartId = Guid.NewGuid();
                        await AddCartInfoAsync(cartId, cartRequestDTO);
                    }
                    else
                    {
                        cart.Total += total;
                        _unitOfWork.CartRepository.Update(cart);
                        cartId = cart.CartId;
                    }

                    // Add or update cart details
                    await AddOrUpdateCartDetailsAsync(cartId, cartRequestDTO.CartDetails);

                    var tasks = new List<Task>();
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
                        Total = total
                    };
                }
                catch (Exception)
                {
                    await transaction.RollbackAsync();
                    throw new ArgumentException("Thêm giỏ hàng thất bại!");
                }
            }
        }

        private async Task AddOrUpdateCartDetailsAsync(Guid cartId, IEnumerable<CartDetailRequestDTO> cartDetails)
        {
            foreach (var item in cartDetails)
            {
                var cartDetail = await _unitOfWork.CartDetailRepository.GetAsync(cd => cd.CartId == cartId && cd.ProductId == item.ProductId);
                if (cartDetail != null)
                {
                    cartDetail.ProductQuantity += item.ProductQuantity; // Update quantity
                    _unitOfWork.CartDetailRepository.Update(cartDetail);
                }
                else
                {
                    var newCartDetail = new CartDetail
                    {
                        CartId = cartId,
                        ProductId = item.ProductId,
                        ProductQuantity = item.ProductQuantity
                    };
                    await _unitOfWork.CartDetailRepository.AddAsync(newCartDetail);
                }
            }
        }
        public async Task<(IEnumerable<CartResponseDTO> data, int totalCount)> GetAllCartsAsync(
            int page,
            string? sortOrder,
            string? sortBy = "carttime",
            string includeProperties = "CartDetail")
        {
            Func<IQueryable<Cart>, IOrderedQueryable<Cart>>? orderBy = GetOrderQuery(sortOrder, sortBy);
            var carts = await _unitOfWork.CartRepository.GetAllAsync(page, filter: null, orderBy, includeProperties);
            var cartResponses = carts.items.Select(c => new CartResponseDTO
            {
                CartId = c.CartId,
                Total = c.Total,
                Table = c.Table,
                CartTime = c.CartTime,
            }).ToList();
            return (cartResponses, carts.totalCount);
        }
        public async Task<CartResponseDTO> GetCartDetailAsync(Guid id)
        {
            var cart = await _unitOfWork.CartRepository.GetCartDetailAsync(id);
            if (cart == null)
            {
                throw new KeyNotFoundException("Không tìm thấy giỏ hàng!");
            }
            return new CartResponseDTO
            {
                CartId = cart.CartId,
                Total = cart.Total,
                Table = cart.Table,
                CartTime = cart.CartTime,
                CustomerName = cart.Customer?.CustomerName,
                CustomerPhone = cart.Customer?.CustomerPhone,
                CartDetails = cart.CartDetails.Select(cd => new CartDetailResponseDTO
                {
                    ProductId = cd.ProductId,
                    ProductName = cd.Product.ProductName,
                    ProductPrice = cd.Product.ProductPrice,
                    ProductQuantity = cd.ProductQuantity
                }).ToList()
            };
        }

        public async Task DeleteCartAsync(string table)
        {
            var cart = await _unitOfWork.CartRepository.GetAsync(c => c.Table.Equals(table));
            if (cart == null)
            {
                throw new KeyNotFoundException("Không tìm thấy giỏ hàng!");
            }
            _unitOfWork.CartRepository.Delete(cart);
        }

        private Func<IQueryable<Cart>, IOrderedQueryable<Cart>>? GetOrderQuery(string? sortOrder, string? sortBy)
        {
            Func<IQueryable<Cart>, IOrderedQueryable<Cart>>? orderBy;
            switch (sortBy?.ToLower())
            {
                case "carttime":
                    orderBy = x => sortOrder != "desc" ? x.OrderBy(c => c.CartTime) : x.OrderByDescending(c => c.CartTime);
                    break;
                default:
                    orderBy = null;
                    break;
            }
            return orderBy;
        }
    }
}