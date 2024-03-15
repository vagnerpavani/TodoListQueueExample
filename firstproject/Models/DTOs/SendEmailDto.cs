namespace firstproject.Models.DTOs;

public class SendEmailDto(string from, string to, string content, long toDoId)
{
    public string FromEmail {get; set;} = from;
    public string TargetEmail {get; set;} = to;
    public string Content {get; set;} = content;
    public long ToDoId {get; set;} = toDoId;
}