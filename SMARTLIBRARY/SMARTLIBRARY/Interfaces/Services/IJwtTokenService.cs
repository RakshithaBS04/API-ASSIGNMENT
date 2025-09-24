using SMARTLIBRARY.DTOs.Responses;

namespace SMARTLIBRARY.Interfaces.Services
{
    public interface IJwtTokenService
    {
        string GenerateToken(UserResponseDto user);
    }
}
