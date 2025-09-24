using SMARTLIBRARY.DTOs.Requests;
using SMARTLIBRARY.DTOs.Responses;

namespace SMARTLIBRARY.Interfaces.Services
{
    public interface IAuthService
    {
        Task<AuthResponseDto?> LoginAsync(LoginRequestDto request);
        Task<UserResponseDto> RegisterAsync(RegisterUserRequestDto request);
    }
}
