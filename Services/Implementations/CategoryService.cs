﻿using CoffeeShop.DTOs;
using CoffeeShop.Models;
using CoffeeShop.Exceptions;
using CoffeeShop.Repositories.Interfaces;
using CoffeeShop.UnitOfWork;
using CoffeeShop.DTOs.Responses;
using CoffeeShop.DTOs.Request;

namespace CoffeeShop.Services.Implementations
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoryService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<CategoryResponseDTO> CreateCategoryAsync(CategoryRequestDTO categoryDTO)
        {
            var categoryNameExisted = await _unitOfWork.CategoryRepository.GetAsync(c => c.CategoryName == categoryDTO.CategoryName);
            if (categoryNameExisted != null)
            {
                throw new ArgumentException("Danh mục sản phẩm đã tồn tại. Vui lòng nhập danh mục khác!");
            }

            var newCategory = new Category
            {
                CategoryName = categoryDTO.CategoryName,
            };

            await _unitOfWork.CategoryRepository.AddAsync(newCategory);
            if (await _unitOfWork.CommitAsync() > 0)
            {
                var categoryResponse = new CategoryResponseDTO
                {
                    CategoryId = newCategory.CategoryId,
                    CategoryName = newCategory.CategoryName
                };
                return categoryResponse;
            }
            else
            {
                throw new Exception("Tạo danh mục thất bại!");
            }
        }

        public async Task<IEnumerable<CategoryResponseDTO>> GetAllCategoriesAsync()
        {
            var categories = await _unitOfWork.CategoryRepository.GetAllAsync(c => !c.IsDeleted);
            if (categories == null)
            {
                throw new KeyNotFoundException("Không tìm thấy danh mục!");
            }
            return categories.Select(c => new CategoryResponseDTO
            {
                CategoryId = c.CategoryId,
                CategoryName = c.CategoryName,
            });
        }

        public async Task<CategoryResponseDTO> DeleteCategoryAsync(Guid categoryId)
        {
            var category = await _unitOfWork.CategoryRepository.GetAsync(c => c.CategoryId == categoryId);

            if (category == null)
            {
                throw new KeyNotFoundException("Không tìm thấy danh mục!");
            }

            var products = await _unitOfWork.ProductRepository.GetAllAsync(p => !p.IsDeleted && p.CategoryId == categoryId);
            if (products.Any())
            {
                throw new Exception("Danh mục này đang chứa sản phẩm, không thể xóa!");
            }

            _unitOfWork.CategoryRepository.SoftDelete(category);

            if (_unitOfWork.Commit() > 0)
            {
                return new CategoryResponseDTO
                {
                    CategoryId = category.CategoryId,
                    CategoryName = category.CategoryName
                };
            }
            else
            {
                throw new Exception("Xóa danh mục thất bại!");
            }
        }
    }
}
