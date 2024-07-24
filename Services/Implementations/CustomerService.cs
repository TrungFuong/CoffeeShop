using CoffeeShop.DTOs.Request;
using CoffeeShop.DTOs.Responses;
using CoffeeShop.Models;
using CoffeeShop.UnitOfWork;
using NuGet.ContentModel;
using System.Linq.Expressions;

namespace CoffeeShop.Services.Implementations
{
    public class CustomerService : ICustomerService
    {
        public readonly IUnitOfWork _unitOfWork;
        public CustomerService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task AddCustomerAsync(CustomerRequestDTO customerDTO)
        {
            var customer = new Customer
            {
                CustomerId = customerDTO.CustomerId,
                CustomerName = customerDTO.CustomerName,
                CustomerPhone = customerDTO.CustomerPhone,
                CustomerBirthday = customerDTO.CustomerBirthday
            };

            await _unitOfWork.CustomerRepository.AddAsync(customer);
            //if (await _unitOfWork.CommitAsync() < 1)
            //{
            //    throw new ArgumentException("Thêm khách hàng không thành công!");
            //}
        }

        public async Task<CustomerResponseDTO> UpdateCustomer(string phone, CustomerRequestDTO customerRequest)
        {
            var currentCustomer = await _unitOfWork.CustomerRepository.GetAsync(c => c.CustomerPhone == phone);
            if (currentCustomer == null)
            {
                throw new KeyNotFoundException("Khách hàng chưa tồn tại, bạn có muốn thêm khách hàng mới không?");
            }
            currentCustomer.CustomerName = customerRequest.CustomerName == string.Empty ? currentCustomer.CustomerName : customerRequest.CustomerName;
            currentCustomer.CustomerPhone = customerRequest.CustomerPhone == string.Empty ? currentCustomer.CustomerPhone : customerRequest.CustomerPhone;
            currentCustomer.CustomerBirthday = customerRequest.CustomerBirthday == DateTime.MinValue ? currentCustomer.CustomerBirthday : customerRequest.CustomerBirthday;

            _unitOfWork.CustomerRepository.Update(currentCustomer);
            if (await _unitOfWork.CommitAsync() < 1)
            {
                throw new ArgumentException("Chỉnh sửa thông tin khách hàng thất bại");
            }
            return new CustomerResponseDTO
            {
                CustomerId = currentCustomer.CustomerId,
                CustomerName = currentCustomer.CustomerName,
                CustomerPhone = currentCustomer.CustomerPhone,
                CustomerBirthday = DateOnly.FromDateTime(currentCustomer.CustomerBirthday)
            };
        }

        public async Task<(IEnumerable<CustomerResponseDTO> data, int totalCount)> GetAllCustomersAsync(int pageNumber, string? search, string? sortOrder, string? sortBy = "customerName", string includeProperties = "")
        {
            Func<IQueryable<Customer>, IOrderedQueryable<Customer>>? orderBy = GetOrderQuery(sortOrder, sortBy);
            Expression<Func<Customer, bool>> filter = await GetFilterQuery(search);

            var customers = await _unitOfWork.CustomerRepository.GetAllAsync(pageNumber, filter, orderBy, includeProperties);

            return (customers.items.Select(p => new CustomerResponseDTO
            {
                CustomerId = p.CustomerId,
                CustomerName = p.CustomerName,
                CustomerPhone = p.CustomerPhone,
                CustomerBirthday = DateOnly.FromDateTime(p.CustomerBirthday)
            }), customers.totalCount); 
        }

        public async Task<CustomerResponseDTO> GetCustomerDetailAsync(string phone)
        {
            var customer = await _unitOfWork.CustomerRepository.GetAsync(c => c.CustomerPhone == phone, c => c.Receipts);
            if (customer == null || customer.IsDeleted == true)
            {
                return null;
            }

            var customerResponse = new CustomerResponseDTO
            {
                CustomerId = customer.CustomerId,
                CustomerName = customer.CustomerName,
                CustomerPhone = customer.CustomerPhone,
                CustomerBirthday = DateOnly.FromDateTime(customer.CustomerBirthday),
                Receipts = customer.Receipts.Select(r => new ReceiptResponseDTO
                {
                    ReceiptId = r.ReceiptId,
                    ReceiptDate = r.ReceiptDate,
                    ReceiptTotal = r.ReceiptTotal
                })
            };

            return customerResponse;
        }

        private async Task<Expression<Func<Customer, bool>>>? GetFilterQuery(string? search)
        {
            // Determine the filtering criteria
            Expression<Func<Customer, bool>>? filter = null;
            var parameter = Expression.Parameter(typeof(Customer), "x");
            var conditions = new List<Expression>();

            // Add IsDelete
            var isDeletedCondition = Expression.Equal(Expression.Property(parameter, nameof(Customer.IsDeleted)),
                Expression.Constant(false));
            conditions.Add(isDeletedCondition);

            // Add search conditions
            if (!string.IsNullOrEmpty(search))
            {
                var searchCondition = Expression.OrElse(
                    Expression.Call(
                        Expression.Property(parameter, nameof(Customer.CustomerPhone)),
                        nameof(string.Contains),
                        Type.EmptyTypes,
                        Expression.Constant(search)
                    ),
                    Expression.Call(
                        Expression.Property(parameter, nameof(Customer.CustomerName)),
                        nameof(string.Contains),
                        Type.EmptyTypes,
                        Expression.Constant(search)
                    )
                );
                conditions.Add(searchCondition);
            }

            if (conditions.Any())
            {
                var combinedCondition = conditions.Aggregate((left, right) => Expression.AndAlso(left, right));
                filter = Expression.Lambda<Func<Customer, bool>>(combinedCondition, parameter);
            }

            return filter;
        }

        private Func<IQueryable<Customer>, IOrderedQueryable<Customer>>? GetOrderQuery(string? sortOrder, string? sortBy)
        {
            Func<IQueryable<Customer>, IOrderedQueryable<Customer>>? orderBy;
            switch (sortBy?.ToLower())
            {
                case "customername":
                    orderBy = x => sortOrder != "desc" ? x.OrderBy(c => c.CustomerName) : x.OrderByDescending(c => c.CustomerName);
                    break;

                case "customerbirthday":
                    orderBy = x => sortOrder != "desc" ? x.OrderBy(c => c.CustomerBirthday) : x.OrderByDescending(c => c.CustomerBirthday);
                    break;

                default:
                    orderBy = null;
                    break;
            }
            return orderBy;
        }
    }
}
