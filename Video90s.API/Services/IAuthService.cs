using Video90s.API.Dtos;

namespace Video90s.API.Services
{
    public interface IAuthService
    {
        Task<bool> RegisterAsync(UserRegisterDto dto);
        Task<string> LoginAsync(UserLoginDto dto);
    }
}
