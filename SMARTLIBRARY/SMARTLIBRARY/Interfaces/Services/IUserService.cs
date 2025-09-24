using SMARTLIBRARY.DTOs.Requests;
using SMARTLIBRARY.DTOs.Responses;

namespace SMARTLIBRARY.Interfaces.Services
{
    public interface IUserService
    {
        Task<IEnumerable<UserResponseDto>> GetAllUsersAsync();
        Task<IEnumerable<UserResponseDto>> GetUsersByRoleAsync(string roleName); // new explicit method
        Task<UserResponseDto?> GetUserByIdAsync(string userId);
        Task<UserResponseDto> CreateUserAsync(RegisterUserRequestDto request);
        Task<UserResponseDto> UpdateUserAsync(string userId, UserResponseDto updatedUser);
        Task DeleteUserAsync(string userId);
    }
}
