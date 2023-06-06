using BlazorApp.Application.RequestModels;

namespace BlazorApp.Client.Services;

public interface IAuthenticationService
{
    Task<string> Login(LoginRequest loginRequest);
    Task Logout();
    Task<string> Register (RegisterRequest registerRequest);
}