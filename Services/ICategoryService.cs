using CoffeeShop.DTOs;
using CoffeeShop.DTOs.Request;
using CoffeeShop.DTOs.Responses;
using CoffeeShop.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoffeeShop.Services
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryResponseDTO>> GetAllCategoriesAsync();
        Task<CategoryResponseDTO> CreateCategoryAsync(CategoryRequestDTO categoryDTO);
        Task<CategoryResponseDTO> DeleteCategoryAsync(Guid categoryId);
    }
}