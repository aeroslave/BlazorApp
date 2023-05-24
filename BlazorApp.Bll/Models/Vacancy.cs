namespace BlazorApp.Bll.Models;

public class Vacancy : Entity
{
    public string Title { get; set; } = string.Empty;
    public bool IsActive { get; set; }
    //Вместо описания может быть ссылка на файл
    public string Description { get; set; } = string.Empty;
}