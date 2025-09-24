using SMARTLIBRARY.DTOs.Requests;
using SMARTLIBRARY.DTOs.Responses;
using SMARTLIBRARY.Interfaces.Repositories;
using SMARTLIBRARY.Interfaces.Services;
using SMARTLIBRARY.Models;

namespace SMARTLIBRARY.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtTokenService _jwtTokenService;

        public AuthService(IUserRepository userRepository, IJwtTokenService jwtTokenService)
        {
            _userRepository = userRepository;
            _jwtTokenService = jwtTokenService;
        }

        public async Task<AuthResponseDto?> LoginAsync(LoginRequestDto request)
        {
            var user = await _userRepository.GetByUserIdAsync(request.UserId);
            if (user == null || user.Password != request.Password)
                return null;

            var userDto = new UserResponseDto
            {
                UserId = user.UserId,
                FullName = user.FullName,
                Email = user.Email,
                RoleName = user.Role?.RoleName ?? "User", // fallback
                ImageUrl = user.ImageUrl,
                IsActive = user.IsActive,
                CreatedAt = user.CreatedAt
            };

            var token = _jwtTokenService.GenerateToken(userDto);

            return new AuthResponseDto
            {
                UserId = userDto.UserId,
                FullName = userDto.FullName,
                RoleName = userDto.RoleName,
                Token = token
            };
        }


        public async Task<UserResponseDto> RegisterAsync(RegisterUserRequestDto request)
        {
            // Only Student and Faculty can self-register
            string prefix;
            int roleId;

            switch (request.RoleName.ToLower())
            {
                case "faculty":
                    prefix = "KAF";
                    roleId = 4; // Faculty
                    break;

                case "student":
                    prefix = "KAS";
                    roleId = 3; // Student
                    break;

                case "librarian":
                    throw new Exception("Librarian accounts can only be created by Admin.");

                default:
                    throw new Exception("Invalid role. Allowed roles: Student, Faculty");
            }

            // Generate unique UserId
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

    }
    }