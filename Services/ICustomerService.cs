using CoffeeShop.DTOs.Request;
using CoffeeShop.DTOs.Responses;

namespace CoffeeShop.Services
{
    public interface ICustomerService
    {
        Task AddCustomerAsync(CustomerRequestDTO customerDTO);
        Task UpdateCustomer(string phone, CustomerRequestDTO customerRequest);
        Task<(IEnumerable<CustomerResponseDTO> data, int totalCount)> GetAllCustomersAsync(int pageNumber, string? search, string? sortOrder, string? sortBy = "customerName", string includeProperties = "");
        Task<CustomerResponseDTO> GetCustomerDetailAsync(string phone);
    }
}
