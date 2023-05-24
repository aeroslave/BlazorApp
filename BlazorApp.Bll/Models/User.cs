namespace BlazorApp.Bll.Models;

public class User : Entity
{
    public string Login { get; set; } = null!;
    public string PasswordHash { get; set; } = null!;
    public string Role { get; set; } = null!;
}