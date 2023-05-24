using System.ComponentModel.DataAnnotations;

namespace BlazorApp.Application.RequestModels;

public record LoginRequest
{
    [Required]
    public string? Login { get; set; }

    [Required]
    public string? Password { get; set; }
}