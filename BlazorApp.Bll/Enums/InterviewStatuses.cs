namespace BlazorApp.Bll.Enums;

public enum InterviewStatuses
{
    //Создано, согласуется с соискателем
    Created = 0,
    //Прошло
    Completed = 1,
    //Отказ соискателю
    Rejected = 2,
    //Выставили офер
    Invitation = 3,
    //Офер принят
    Confirmed = 4
}