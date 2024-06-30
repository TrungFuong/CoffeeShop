using CoffeeShop.DTOs.Request;
using CoffeeShop.DTOs.Responses;
using CoffeeShop.Models;
using CoffeeShop.Models.Enums;
using CoffeeShop.UnitOfWork;

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
                DateOfBirth = userRegisterRequest.DateOfBirth,
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
    }
}
