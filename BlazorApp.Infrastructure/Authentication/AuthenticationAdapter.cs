using BlazorApp.Application.Interfaces;
using BlazorApp.Application.RequestModels;
using BlazorApp.Infrastructure.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BlazorApp.Bll.Models;
using Microsoft.Extensions.Configuration;

namespace BlazorApp.Infrastructure.Authentication;

public class AuthenticationAdapter : IAuthenticationAdapter
{
    private readonly ApplicationDbContext _context;
    private readonly IConfiguration _configuration;

    public AuthenticationAdapter(ApplicationDbContext context, IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;
    }

    public async Task<string> RegisterUser(RegisterRequest request)
    {
        if (_context.Users.Any(it => it.Login == request.Login))
        {
            throw new InfrastructureException($"User with login {request.Login} already exist!");
        }

        //Проверять пароль на сложность.

        var user = new User
        {
            Login = request.Login,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
            Role = request.Role.ToString()
        };

        try
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            return GetToken(user);
        }
        catch (Exception exception)
        {
            throw new InfrastructureException($"Register failed: {exception.Message}");
        }
    }

    public async Task<string> Login(LoginRequest request)
    {
        var user = await _context.Users.FirstOrDefaultAsync(user => user.Login == request.Login);

        if (user == null)
            throw new InfrastructureException("User not found");

        if (!BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
            throw new InfrastructureException("Wrong password!");

        return GetToken(user);
    }

    private string GetToken(User user)
    {
        var claims = new List<Claim>
        {
            new(ClaimTypes.Name, user.Login),
            new(ClaimTypes.Role, user.Role)
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value!));

        var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

        var jwt = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.Now.AddHours(1),
            signingCredentials: cred);

        return new JwtSecurityTokenHandler().WriteToken(jwt);
    }
}