namespace BlazorApp.Bll.Models.DTOs;

public record ResumeDto
{
    public int UserId { get; set; }

    public string Description { get; set; } = string.Empty;
}