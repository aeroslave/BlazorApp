using System.ComponentModel.DataAnnotations;
using BlazorApp.Bll.Enums;

namespace BlazorApp.Application.RequestModels;

public record RegisterRequest
{
    [Required] 
    public string Login { get; set; } = null!;

    [Required] 
    public string Password { get; set; } = null!;

    public Roles Role { get; set; } = Roles.User; //Добавлять пользователя с ролью админ может только админ
}