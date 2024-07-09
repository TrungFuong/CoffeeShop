using CoffeeShop.DTOs;
using CoffeeShop.DTOs.Request;
using CoffeeShop.DTOs.Responses;
using CoffeeShop.Models;
using CoffeeShop.UnitOfWork;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.Storage.V1;
using Mono.TextTemplating;
using NuGet.ContentModel;
using System.Linq.Expressions;

namespace CoffeeShop.Services.Implementations
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly StorageClient _storageClient;
        private readonly string _bucketName;
        public ProductService(IUnitOfWork unitOfWork, StorageClient storageClient, string bucketName)
        {
            _unitOfWork = unitOfWork;
            _storageClient = storageClient;
            _bucketName = bucketName;
        }
        //public async Task<ProductResponseDTO> CreateProductAsync(ProductRequestDTO productRequest, FileUpload fileUpload)
        //{
        //    var category = await _unitOfWork.CategoryRepository.GetAsync(c => c.CategoryId == productRequest.CategoryId);
        //    if (category == null)
        //    {
        //        throw new KeyNotFoundException("Category not found");
        //    }

        //    var client = StorageClient.Create();
        //    var obj = await client.UploadObjectAsync("coffee-shop-graduation-project", fileUpload.Name, fileUpload.Type, new MemoryStream(fileUpload.FileContent));

        //    string imageUrl = await UploadFileToCloudAsync(fileUpload);

        //    var product = new Product
        //    {
        //        ProductId = Guid.NewGuid(),
        //        ProductName = productRequest.ProductName,
        //        ProductPrice = productRequest.ProductPrice,
        //        ProductDescription = productRequest.ProductDescription,
        //        CategoryId = productRequest.CategoryId,
        //        ImageUrl = imageUrl
        //    };

        //    await _unitOfWork.ProductRepository.AddAsync(product);

        //    if (await _unitOfWork.CommitAsync() > 0)
        //    {
        //        var productResponse = new ProductResponseDTO
        //        {
        //            ProductId = product.ProductId,
        //            ProductName = product.ProductName,
        //            ProductPrice = product.ProductPrice,
        //            ProductDescription = product.ProductDescription,
        //            CategoryId = product.CategoryId,
        //            ImageUrl = product.ImageUrl
        //        };
        //        return productResponse;
        //    }
        //    else
        //    {
        //        throw new Exception("Failed to create product");
        //    }
        //}

        //public FileUpload ConvertToFileUpload(IFormFile formFile)
        //{
        //    using (var memoryStream = new MemoryStream())
        //    {
        //        formFile.CopyTo(memoryStream);

        //        return new FileUpload
        //        {
        //            Name = formFile.FileName,
        //            Type = formFile.ContentType,
        //            FileContent = memoryStream.ToArray()
        //        };
        //    }
        //}

        public async Task<ProductResponseDTO> DeleteProductAsync(Guid productId)
        {
            var product = await _unitOfWork.ProductRepository.GetAsync(x => x.ProductId == productId);
            if (product == null)
            {
                throw new Exception("Product not found");
            }

            _unitOfWork.ProductRepository.SoftDelete(product);
            if (await _unitOfWork.CommitAsync() > 0)
            {
                return new ProductResponseDTO {
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
                throw new Exception("Failed to delete product");
            }
        }

        public async Task<ProductResponseDTO> GetProductDetail(Guid productId)
        {
            var product = await _unitOfWork.ProductRepository.GetAsync(x => x.ProductId == productId, x => x.Category);
            if (product == null)
            {
                return null;
            }

            return new ProductResponseDTO
            {
                ProductId = product.ProductId,
                ProductName = product.ProductName,
                ProductPrice = product.ProductPrice,
                ProductDescription = product.ProductDescription,
                CategoryId = product.CategoryId,
                CategoryName = product.Category.CategoryName,
                ImageUrl = product.ImageUrl
            };
        }

        public async Task<ProductResponseDTO> UpdateProduct(Guid id, ProductRequestDTO productRequest)
        {
            var currentProduct = await _unitOfWork.ProductRepository.GetAsync(x => x.ProductId == id);
            if (currentProduct == null)
            {
                throw new ArgumentException("Product not found");
            }
            currentProduct.ProductName = productRequest.ProductName;
            currentProduct.ProductPrice = productRequest.ProductPrice;
            currentProduct.ProductDescription = productRequest.ProductDescription;
            currentProduct.CategoryId = productRequest.CategoryId;
            //currentProduct.ImageUrl = productRequest.ImageUrl;
            _unitOfWork.ProductRepository.Update(currentProduct);
            await _unitOfWork.CommitAsync();
            return new ProductResponseDTO
            {
                ProductId = currentProduct.ProductId,
                ProductName = currentProduct.ProductName,
                ProductPrice = currentProduct.ProductPrice,
                ProductDescription = currentProduct.ProductDescription,
                CategoryId = currentProduct.CategoryId,
                //ImageUrl = currentProduct.ImageUrl
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
    }
}