using SMARTLIBRARY.DTOs.Requests;
using SMARTLIBRARY.DTOs.Responses;
using SMARTLIBRARY.Interfaces.Repositories;
using SMARTLIBRARY.Interfaces.Services;
using SMARTLIBRARY.Models;

namespace SMARTLIBRARY.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        // Get all users
        public async Task<IEnumerable<UserResponseDto>> GetAllUsersAsync()
        {
            var users = await _userRepository.GetAllUsersWithRolesAsync();
            return users.Select(u => new UserResponseDto
            {
                UserId = u.UserId,
                FullName = u.FullName,
                Email = u.Email,
                RoleName = u.Role?.RoleName ?? "",
                ImageUrl = u.ImageUrl,
                IsActive = u.IsActive,
                CreatedAt = u.CreatedAt
            });
        }

        // Get users by role
        public async Task<IEnumerable<UserResponseDto>> GetUsersByRoleAsync(string roleName)
        {
            var users = await _userRepository.GetUsersByRoleAsync(roleName);
            return users.Select(u => new UserResponseDto
            {
                UserId = u.UserId,
                FullName = u.FullName,
                Email = u.Email,
                RoleName = u.Role?.RoleName ?? "",
                ImageUrl = u.ImageUrl,
                IsActive = u.IsActive,
                CreatedAt = u.CreatedAt
            });
        }

        public async Task<UserResponseDto?> GetUserByIdAsync(string userId)
        {
            var user = await _userRepository.GetByUserIdAsync(userId);
            if (user == null) return null;

            return new UserResponseDto
            {
                UserId = user.UserId,
                FullName = user.FullName,
                Email = user.Email,
                RoleName = user.Role?.RoleName ?? "",
                ImageUrl = user.ImageUrl,
                IsActive = user.IsActive,
                CreatedAt = user.CreatedAt
            };
        }

        public async Task<UserResponseDto> CreateUserAsync(RegisterUserRequestDto request)
        {
            string prefix;
            int roleId;

            switch (request.RoleName.ToLower())
            {
                case "admin":
                    prefix = "ADM";
                    roleId = 1;
                    break;
                case "librarian":
                    prefix = "LBR";
                    roleId = 2;
                    break;
                case "faculty":
                    prefix = "KAF";
                    roleId = 4;
                    break;
                case "student":
                    prefix = "KAS";
                    roleId = 3;
                    break;
                default:
                    throw new Exception("Invalid role. Allowed roles: Admin, Librarian, Faculty, Student");
            }

            var existingUsers = await _userRepository.GetUsersByRoleAsync(request.RoleName);
            int nextId = existingUsers.Count() + 1;
            string userId = $"{prefix}{nextId:000}";

            var user = new User
            {
                UserId = userId,
                FullName = request.FullName,
                Email = request.Email,
                Password = request.Password,
                RoleId = roleId,
                ImageUrl = request.ImageUrl ?? "/Images/defaultuser.png",
                IsActive = true,
                CreatedAt = new DateTime(2025, 9, 24)
            };

            await _userRepository.AddAsync(user);
            await _userRepository.SaveChangesAsync();

            return new UserResponseDto
            {
                UserId = user.UserId,
                FullName = user.FullName,
                Email = user.Email,
                RoleName = request.RoleName,
                ImageUrl = user.ImageUrl,
                IsActive = user.IsActive,
                CreatedAt = user.CreatedAt
            };
        }

        public async Task<UserResponseDto> UpdateUserAsync(string userId, UserResponseDto updatedUser)
        {
            var user = await _userRepository.GetByUserIdAsync(userId);
            if (user == null) throw new Exception("User not found");

            user.FullName = updatedUser.FullName;
            user.Email = updatedUser.Email;
            user.ImageUrl = updatedUser.ImageUrl;
            user.IsActive = updatedUser.IsActive;

            _userRepository.Update(user);
            await _userRepository.SaveChangesAsync();

            return new UserResponseDto
            {
                UserId = user.UserId,
                FullName = user.FullName,
                Email = user.Email,
                RoleName = user.Role?.RoleName ?? "",
                ImageUrl = user.ImageUrl,
                IsActive = user.IsActive,
                CreatedAt = user.CreatedAt
            };
        }

        public async Task DeleteUserAsync(string userId)
        {
            var user = await _userRepository.GetByUserIdAsync(userId);
            if (user == null) throw new Exception("User not found");

            _userRepository.Delete(user);
            await _userRepository.SaveChangesAsync();
        }
    }
}
