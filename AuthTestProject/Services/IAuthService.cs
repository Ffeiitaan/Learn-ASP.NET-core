using AuthTestProject.Entities;
using AuthTestProject.Models;


namespace AuthTestProject.Services
{
    public interface IAuthService
    {
        Task<User> RegisterAsync(UserDto request);
        Task<TokenResponseDto?> LoginAsync(UserDto request);
        Task<TokenResponseDto?> RefreshTokensAsync(RefreshTokenRequestDto request);
    }
}