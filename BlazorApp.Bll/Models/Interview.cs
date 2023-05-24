using BlazorApp.Bll.Enums;

namespace BlazorApp.Bll.Models;

public class Interview : Entity
{
    public DateTime? Date { get; set; }
    public InterviewStatuses Status { get; set; }
    public int ResumeId { get; set; }
    public int VacancyId { get; set; }
    public string? Note { get; set; }
}