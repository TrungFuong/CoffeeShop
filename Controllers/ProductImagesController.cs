using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CoffeeShop;
using CoffeeShop.Models;
using CoffeeShop.Services;
using CoffeeShop.DTOs;

namespace CoffeeShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductImagesController : ControllerBase
    {
        private readonly IProductImageService _productImageService;

        public ProductImagesController(IProductImageService productImageService)
        {
            _productImageService = productImageService;
        }

        // GET: api/ProductImages
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductResponseDTO>>> GetProductImages()
        {
            var productImages = await _productImageService.GetAllProductImageAsync();
            return Ok(productImages);
        }

        // GET: api/ProductImages/5
        [HttpGet("{productImageId}")]
        public async Task<ActionResult<ProductImage>> GetProductImage(Guid productImageId)
        {
            var productImage = await _productImageService.GetProductImageByIdAsync(productImageId);
            if (productImage == null)
            {
                return NotFound();
            }
            return Ok(productImage);
        }

        // PUT: api/ProductImages/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{productImageId}")]
        public async Task<IActionResult> PutProductImage(Guid productImageId, ProductImageRequestDTO productImageDTO)
        {
            var existingProductImage = await _productImageService.GetProductImageByIdAsync(productImageId);
            if (existingProductImage == null)
            {
                return NotFound();
            }

            await _productImageService.UpdateProductImageAsync(productImageId, productImageDTO);
            return NoContent();
        }

        // POST: api/ProductImages
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<IActionResult> PostProductImage(ProductImageRequestDTO productImageDTO)
        {
            try
            {
                await _productImageService.AddProductImageAsync(productImageDTO);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while adding the product image.");
            }
        }

        // DELETE: api/ProductImages/5
        [HttpDelete("{productImageId}")]
        public async Task<IActionResult> DeleteProductImage(Guid productImageId)
        {
            await _productImageService.DeleteProductImageAsync(productImageId);
            return NoContent();
        }
    }
}
