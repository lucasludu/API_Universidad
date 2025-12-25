using Application.DTOs._auth.Request;
using Application.DTOs._auth.Response;
using Application.Wrappers;

namespace Application.Interfaces
{
    public interface IAccountService
    {
        Task<Response<AuthenticationResponse>> AuthenticateAsync(AuthenticationRequest request, string ipAddress);
        Task<Response<string>> RegisterAsync(RegisterUserRequest request, string origin);
        Task<Response<AuthenticationResponse>> RefreshTokenAsync(string token, string ipAddress);
    }
}
