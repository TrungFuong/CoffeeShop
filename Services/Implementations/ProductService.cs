using CoffeeShop.DTOs;
using CoffeeShop.DTOs.Request;
using CoffeeShop.DTOs.Responses;
using CoffeeShop.Exceptions;
using CoffeeShop.Models;
using CoffeeShop.UnitOfWork;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.Storage.V1;
using Microsoft.AspNetCore.Http.HttpResults;
using Mono.TextTemplating;
using NuGet.ContentModel;
using System.Linq.Expressions;

namespace CoffeeShop.Services.Implementations
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly CloudinaryService _cloudinaryService;

        public ProductService(IUnitOfWork unitOfWork, CloudinaryService cloudinaryService)

        {
            _unitOfWork = unitOfWork;
            _cloudinaryService = cloudinaryService;
        }
        public async Task<ProductResponseDTO> CreateProductAsync(ProductRequestDTO productRequest, IFormFile imageFile)
        {
            var category = await _unitOfWork.CategoryRepository.GetAsync(c => c.CategoryId == productRequest.CategoryId);
            if (category == null)
            {
                throw new KeyNotFoundException("Không tìm thấy danh mục!");
            }
            string imageUrl;
            if (imageFile != null && imageFile.Length > 0)
            {
                using (var stream = imageFile.OpenReadStream())
                {
                    imageUrl = await _cloudinaryService.UploadImageAsync(stream, imageFile.FileName);
                    productRequest.ImageUrl = imageUrl;
                }
            }
            else
            {
                productRequest.ImageUrl = null;
            }

            var product = new Product
            {
                ProductId = Guid.NewGuid(),
                ProductName = productRequest.ProductName,
                ProductPrice = productRequest.ProductPrice,
                ProductDescription = productRequest.ProductDescription,
                CategoryId = productRequest.CategoryId,
                ImageUrl = productRequest.ImageUrl
            };

            await _unitOfWork.ProductRepository.AddAsync(product);

            if (await _unitOfWork.CommitAsync() > 0)
            {
                return new ProductResponseDTO
                {
                    ProductId = product.ProductId,
                    ProductName = product.ProductName,
                    ProductPrice = product.ProductPrice,
                    ProductDescription = product.ProductDescription,
                    CategoryId = product.CategoryId,
                    ImageUrl = product.ImageUrl
                };
            }
            else
            {
                throw new Exception("Tạo sản phẩm thất bại!");
            }
        }

        public async Task<ProductResponseDTO> DeleteProductAsync(Guid productId)
        {
            var product = await _unitOfWork.ProductRepository.GetAsync(x => x.ProductId == productId);
            if (product == null)
            {
                throw new KeyNotFoundException("Không tìm thấy sản phẩm!");
            }

            _unitOfWork.ProductRepository.SoftDelete(product);
            if (await _unitOfWork.CommitAsync() > 0)
            {
                return new ProductResponseDTO
                {
                    ProductId = product.ProductId,
                    ProductName = product.ProductName,
                    ProductPrice = product.ProductPrice,
                    ProductDescription = product.ProductDescription,
                    CategoryId = product.CategoryId,
                    ImageUrl = product.ImageUrl
                };
            }
            else
            {
                throw new Exception("Xóa sản phẩm thất bại!");
            }
        }

        public async Task<ProductDetailResponseDTO> GetProductDetail(Guid productId)
        {
            var product = await _unitOfWork.ProductRepository.GetAsync(x => x.ProductId == productId, x => x.Category);
            if (product == null)
            {
                return null;
            }

            if (product.IsDeleted)
            {
                throw new KeyNotFoundException("Không tìm thấy sản phẩm!");
            }

            return new ProductDetailResponseDTO
            {
                ProductId = product.ProductId,
                ProductName = product.ProductName,
                ProductPrice = product.ProductPrice,
                ProductDescription = product.ProductDescription,
                CategoryId = product.CategoryId,
                CategoryName = product.Category.CategoryName,
                //ImageUrl = product.ImageUrl
            };
        }

        public async Task<ProductResponseDTO> UpdateProduct(Guid id, ProductUpdateRequestDTO productRequest, IFormFile imageFile)
        {
            var currentProduct = await _unitOfWork.ProductRepository.GetAsync(x => x.ProductId == id);
            if (currentProduct == null)
            {
                throw new KeyNotFoundException("Không tìm thấy sản phẩm!");
            }

            if (currentProduct.IsDeleted)
            {
                throw new KeyNotFoundException("Không tìm thấy sản phẩm!");
            }

            string imageUrl;
            if (imageFile != null && imageFile.Length > 0)
            {
                using (var stream = imageFile.OpenReadStream())
                {
                    imageUrl = await _cloudinaryService.UploadImageAsync(stream, imageFile.FileName);
                    productRequest.ImageUrl = imageUrl;
                }
            }
            else
            {
                productRequest.ImageUrl = null;
            }

            if (productRequest.ProductPrice != null)
            {
                currentProduct.ProductPrice = productRequest.ProductPrice.Value;
            }

            if (productRequest.ProductName != null && productRequest.ProductName != string.Empty)
            {
                currentProduct.ProductName = productRequest.ProductName;
            }
            if (productRequest.ProductDescription != null && productRequest.ProductDescription != string.Empty)
            {
                currentProduct.ProductDescription = productRequest.ProductDescription;
            }
            if (productRequest.CategoryId != null && productRequest.CategoryId != Guid.Empty)
            {
                currentProduct.CategoryId = (Guid)productRequest.CategoryId;
            }

            currentProduct.ImageUrl = productRequest.ImageUrl == null ? currentProduct.ImageUrl : productRequest.ImageUrl;

            _unitOfWork.ProductRepository.Update(currentProduct);
            await _unitOfWork.CommitAsync();
            return new ProductResponseDTO
            {
                ProductId = currentProduct.ProductId,
                ProductName = currentProduct.ProductName,
                ProductPrice = currentProduct.ProductPrice,
                ProductDescription = currentProduct.ProductDescription,
                CategoryId = currentProduct.CategoryId,
                ImageUrl = currentProduct.ImageUrl
            };
        }

        //private async Task<string> UploadFileToCloudAsync(FileUpload fileUpload)
        //{
        //    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(fileUpload.Name);
        //    string contentType = GetContentType(fileUpload.Name);

        //    using (var stream = new MemoryStream(fileUpload.FileContent))
        //    {
        //        var dataObject = await _storageClient.UploadObjectAsync(_bucketName, fileName, contentType, stream);
        //        return GenerateSignedUrl(_bucketName, fileName);
        //    }
        //}

        //private string GetContentType(string fileName)
        //{
        //    var extension = Path.GetExtension(fileName).ToLowerInvariant();
        //    return extension switch
        //    {
        //        ".jpg" => "image/jpeg",
        //        ".jpeg" => "image/jpeg",
        //        ".png" => "image/png",
        //        ".gif" => "image/gif",
        //        ".pdf" => "application/pdf",
        //        _ => "application/octet-stream",
        //    };
        //}

        //private string GenerateSignedUrl(string bucketName, string objectName)
        //{
        //    UrlSigner urlSigner = UrlSigner.FromServiceAccountCredential(GoogleCredential.GetApplicationDefault().UnderlyingCredential as ServiceAccountCredential);
        //    string url = urlSigner.Sign(bucketName, objectName, TimeSpan.FromHours(1), HttpMethod.Get);
        //    return url;
        //}


        public async Task<(IEnumerable<ProductResponseDTO> data, int totalCount)> GetAllProductsAsync(int pageNumber, Guid? category, string? search, string? sortOrder, string? sortBy = "productName", string includeProperties = "", string? newProductName = "")
        {
            Func<IQueryable<Product>, IOrderedQueryable<Product>>? orderBy = GetOrderQuery(sortOrder, sortBy);
            Expression<Func<Product, bool>> filter = await GetFilterQuery(category, search);
            Expression<Func<Product, bool>> prioritizeCondition = null;

            if (!string.IsNullOrEmpty(newProductName))
            {
                prioritizeCondition = p => p.ProductName == newProductName;
            }

            var products = await _unitOfWork.ProductRepository.GetAllAsync(pageNumber, filter, orderBy, includeProperties);

            return (products.items.Select(p => new ProductResponseDTO
            {
                ProductId = p.ProductId,
                ProductName = p.ProductName,
                ProductPrice = p.ProductPrice,
                ProductDescription = p.ProductDescription,
                CategoryId = p.CategoryId,
                CategoryName = p.Category.CategoryName,
                ImageUrl = p.ImageUrl,
            }), products.totalCount);
        }

        private async Task<Expression<Func<Product, bool>>>? GetFilterQuery(Guid? category, string? search)
        {
            // Determine the filtering criteria
            Expression<Func<Product, bool>>? filter = null;
            var parameter = Expression.Parameter(typeof(Product), "x");
            var conditions = new List<Expression>();

            // Add IsDelete
            var isDeletedCondition = Expression.Equal(Expression.Property(parameter, nameof(Product.IsDeleted)),
                Expression.Constant(false));
            conditions.Add(isDeletedCondition);

            // Add search conditions
            if (!string.IsNullOrEmpty(search))
            {
                var searchCondition =
                    Expression.Call(
                        Expression.Property(parameter, nameof(Product.ProductName)),
                        nameof(string.Contains),
                        Type.EmptyTypes,
                        Expression.Constant(search)
                    );
                conditions.Add(searchCondition);
            }

            // Add category condition if necessary
            if (category.HasValue)
            {
                var categoryCondition = Expression.Equal(
                    Expression.Property(parameter, nameof(Product.CategoryId)),
                    Expression.Constant(category)
                );
                conditions.Add(categoryCondition);
            }

            // Combine all conditions with AndAlso
            if (conditions.Any())
            {
                var combinedCondition = conditions.Aggregate((left, right) => Expression.AndAlso(left, right));
                filter = Expression.Lambda<Func<Product, bool>>(combinedCondition, parameter);
            }

            return filter;
        }

        private Func<IQueryable<Product>, IOrderedQueryable<Product>>? GetOrderQuery(string? sortOrder, string? sortBy)
        {
            Func<IQueryable<Product>, IOrderedQueryable<Product>>? orderBy;
            switch (sortBy?.ToLower())
            {
                case "productname":
                    orderBy = x => sortOrder != "desc" ? x.OrderBy(p => p.ProductName) : x.OrderByDescending(p => p.ProductName);
                    break;

                case "category":
                    orderBy = x => sortOrder != "desc" ? x.OrderBy(p => p.Category.CategoryName) : x.OrderByDescending(p => p.Category.CategoryName);
                    break;

                default:
                    orderBy = null;
                    break;
            }
            return orderBy;
        }

        // ...

        public async Task<(IEnumerable<ReportResponseDTO>, int count)> GetReports(DateTime? startDate, DateTime? endDate, int pageNumber, string? search, string? sortOrder, string? sortBy = "productName", string includeProperties = "")
        {
            Func<IQueryable<Product>, IOrderedQueryable<Product>>? orderBy = GetOrderQuery(sortOrder, sortBy);
            Expression<Func<Product, bool>> filter = await GetFilterQuery(null, search);

            includeProperties = "Receipts, ReceiptDetail";

            var products = await _unitOfWork.ProductRepository.GetAllAsync(p => !p.IsDeleted);
            var receiptDetails = await _unitOfWork.ReceiptDetailRepository.GetAllAsync(includeProperties: "Receipt");
            var receiptDetailResult = receiptDetails.items;
            var reportResults = new List<ReportResponseDTO>();
            if (startDate != null && startDate != DateTime.MinValue && endDate != null && endDate != DateTime.MinValue)
            {
                reportResults = products.Select(product => new ReportResponseDTO
                {
                    ProductName = product.ProductName,
                    Price = product.ProductPrice,
                    Quantity = receiptDetailResult
                        .Where(rd => rd.ProductId == product.ProductId && rd.Receipt.ReceiptDate >= startDate && rd.Receipt.ReceiptDate <= endDate)
                        .Sum(rd => rd.ProductQuantity),
                    Total = product.ReceiptDetails?.Where(rd => rd.Receipt.ReceiptDate >= startDate && rd.Receipt.ReceiptDate <= endDate)
                        .Sum(rd => rd.ProductQuantity * rd.Product.ProductPrice) ?? 0
                }).ToList();
            }
            else
            {
                reportResults = products.Select(product => new ReportResponseDTO
                {
                    ProductName = product.ProductName,
                    Price = product.ProductPrice,
                    Quantity = receiptDetailResult
                        .Where(rd => rd.ProductId == product.ProductId)
                        .Sum(rd => rd.ProductQuantity),
                    Total = product.ReceiptDetails?.Sum(rd => rd.ProductQuantity * rd.Product.ProductPrice) ?? 0
                }).ToList();
            }
            return (reportResults, products.Count());
        }
    }
}