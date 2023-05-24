using BlazorApp.Application.RequestModels;

namespace BlazorApp.Application.Interfaces;

public interface IAuthenticationAdapter
{
    Task<string> RegisterUser(RegisterRequest request);
    Task<string> Login(LoginRequest request);
}