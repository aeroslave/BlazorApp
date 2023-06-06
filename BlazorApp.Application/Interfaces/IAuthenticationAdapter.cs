using BlazorApp.Application.RequestModels;
using System.Security.Claims;

namespace BlazorApp.Application.Interfaces;

public interface IAuthenticationAdapter
{
    Task<string> RegisterUser(RegisterRequest request);
    Task<string> Login(LoginRequest request);
    public IEnumerable<Claim> ParseClaimsFromJwt(string jwt);
}