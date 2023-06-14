namespace BlazorApp.Bll.Models;

public class JobSeeker : Entity
{
    public string FullName { get; set; }
    public string Address { get; set; } = null!;
    public string CellNumber { get; set; } = null!;
    public string Email { get; set; }

    public byte[] Photo { get; set; }

    //Ссылка на логин. По задумке можно оставить резюме не логинясь в системе, только оставив контактные данные.
    public int? UserId { get; set; }
}