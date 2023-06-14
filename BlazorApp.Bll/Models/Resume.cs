namespace BlazorApp.Bll.Models;

public class Resume : Entity
{
    public int UserId { get; set; }
    
    //Вместо описания может быть ссылка на файл
    public string Description { get; set; } = string.Empty;

    public byte[] File { get; set; }
}