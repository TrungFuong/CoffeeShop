using CoffeeShop.DTOs;
using CoffeeShop.DTOs.Request;
using CoffeeShop.DTOs.Responses;
using CoffeeShop.Models;
using CoffeeShop.Models.Enums;
using CoffeeShop.UnitOfWork;
using NuGet.ContentModel;
using System.Configuration;
using System.Linq.Expressions;

namespace CoffeeShop.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<UserRegisterResponseDTO> AddUserAsync(UserRegisterRequestDTO userRegisterRequest)
        {
            var defaultPassword = $"{userRegisterRequest.Username}@{userRegisterRequest.DateOfBirth:ddMMyyyy}";
            var salt = BCrypt.Net.BCrypt.GenerateSalt();
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(defaultPassword, salt);
            var newUser = new User
            {
                Username = userRegisterRequest.Username,
                FirstName = userRegisterRequest.FirstName,
                LastName = userRegisterRequest.LastName,
                DateOfBirth = DateOnly.FromDateTime(userRegisterRequest.DateOfBirth),
                Gender = userRegisterRequest.Gender,
                PhoneNumber = userRegisterRequest.PhoneNumber,
                Role = userRegisterRequest.Role,
                HashPassword = hashedPassword,
                Salt = salt,
                UserPosition = userRegisterRequest.UserPosition,
                UserWorkingHour = null,
                Receipts = null,
                CheckTimes = null,
                Salary = null,
                IsDeleted = false,
                IsFirstLogin = true
            };
            await _unitOfWork.UserRepository.AddAsync(newUser);
            if (await _unitOfWork.CommitAsync() < 1)
            {
                throw new InvalidOperationException("An error occurred while registering the user.");
            }
            else
            {
                return new UserRegisterResponseDTO
                {
                    Username = newUser.Username,
                    FirstName = newUser.FirstName,
                    LastName = newUser.LastName,
                    DateOfBirth = newUser.DateOfBirth,
                    UserPosition = newUser.UserPosition,
                    Role = newUser.Role,
                    PhoneNumber = newUser.PhoneNumber
                };
            }
        }

        public async Task<bool> DeleteUser(Guid id)
        {
            var user = await _unitOfWork.UserRepository.GetAsync(u => u.UserId == id);
            if (user == null)
            {
                throw new KeyNotFoundException("User not found.");
            }
            user.IsDeleted = true;
            _unitOfWork.UserRepository.Update(user);

            var result = await _unitOfWork.CommitAsync();
            return result > 0;
        }

        public async Task<(IEnumerable<UserResponseDTO> data, int totalCount)> GetAllUsersAsync(
            int pageNumber,
            string? search,
            string? sortOrder,
            string? sortBy = "username",
            string includeProperties = "",
            string? newUsername = "")
        {
            Func<IQueryable<User>, IOrderedQueryable<User>>? orderBy = GetOrderQuery(sortOrder, sortBy);
            Expression<Func<User, bool>> filter = await GetFilterQuery(search);
            Expression<Func<User, bool>> prioritizeCondition = null;

            if (!string.IsNullOrEmpty(newUsername))
            {
                prioritizeCondition = u => u.Username == newUsername;
            }
            var users = await _unitOfWork.UserRepository.GetAllAsync(pageNumber, filter, orderBy, includeProperties);

            return (users.items.Select(p => new UserResponseDTO
            {
                Username = p.Username,
                FirstName = p.FirstName,
                LastName = p.LastName,
                DateOfBirth = p.DateOfBirth,
                UserPosition = p.UserPosition,
                Role = p.Role,
                PhoneNumber = p.PhoneNumber,
                //So gio lam viec
                //Muc luong
            }), users.totalCount);
        }

        public async Task<UserResponseDTO> GetUserDetailAsync(Guid userId)
        {
            var user = await _unitOfWork.UserRepository.GetAsync(u => u.UserId == userId);
            if (user == null)
            {
                throw new ArgumentException("User not found.");
            }
            return new UserResponseDTO
            {
                Username = user.Username,
                FirstName = user.FirstName,
                LastName = user.LastName,
                DateOfBirth = user.DateOfBirth,
                UserPosition = user.UserPosition,
                Role = user.Role,
                PhoneNumber = user.PhoneNumber,
                //So gio lam viec
                //Muc luong
            };
        }

        public async Task<UserResponseDTO> UpdateUser(Guid userId, UserUpdateRequestDTO userRequest)
        {
            var user = await _unitOfWork.UserRepository.GetAsync(u => u.UserId == userId);
            if (user == null)
            {
                throw new ArgumentException("User not found.");
            }
            user.PhoneNumber = userRequest.PhoneNumber;
            user.UserPosition = userRequest.UserPosition;
            user.Role = userRequest.Role;
            _unitOfWork.UserRepository.Update(user);
            if (await _unitOfWork.CommitAsync() < 0)
            {
                throw new ArgumentException("An error occurred while updating the user.");
            };
            return new UserResponseDTO
            {
                Username = user.Username,
                FirstName = user.FirstName,
                LastName = user.LastName,
                DateOfBirth = user.DateOfBirth,
                UserPosition = user.UserPosition,
                Role = user.Role,
                PhoneNumber = user.PhoneNumber
            };
        }

        private async Task<Expression<Func<User, bool>>>? GetFilterQuery(string? search)
        {
            // Determine the filtering criteria
            Expression<Func<User, bool>>? filter = null;
            var parameter = Expression.Parameter(typeof(Product), "x");
            var conditions = new List<Expression>();

            // Add IsDelete
            var isDeletedCondition = Expression.Equal(Expression.Property(parameter, nameof(User.IsDeleted)),
                Expression.Constant(false));
            conditions.Add(isDeletedCondition);

            // Add search conditions
            if (!string.IsNullOrEmpty(search))
            {
                var searchCondition = Expression.OrElse(
                    Expression.Call(
                        Expression.Property(parameter, nameof(User.FirstName)),
                        nameof(string.Contains),
                        Type.EmptyTypes,
                        Expression.Constant(search)
                    ),
                    Expression.Call(
                        Expression.Property(parameter, nameof(User.LastName)),
                        nameof(string.Contains),
                        Type.EmptyTypes,
                        Expression.Constant(search)
                    )
                );
                conditions.Add(searchCondition);
            }


            // Combine all conditions with AndAlso
            if (conditions.Any())
            {
                var combinedCondition = conditions.Aggregate((left, right) => Expression.AndAlso(left, right));
                filter = Expression.Lambda<Func<User, bool>>(combinedCondition, parameter);
            }

            return filter;
        }

        private Func<IQueryable<User>, IOrderedQueryable<User>>? GetOrderQuery(string? sortOrder, string? sortBy)
        {
            Func<IQueryable<User>, IOrderedQueryable<User>>? orderBy;
            switch (sortBy?.ToLower())
            {
                case "firstname":
                    orderBy = x => sortOrder != "desc" ? x.OrderBy(u => u.FirstName) : x.OrderByDescending(u => u.FirstName);
                    break;

                case "lastname":
                    orderBy = x => sortOrder != "desc" ? x.OrderBy(u => u.FirstName) : x.OrderByDescending(u => u.FirstName);
                    break;

                default:
                    orderBy = null;
                    break;
            }
            return orderBy;
        }
    }
}
