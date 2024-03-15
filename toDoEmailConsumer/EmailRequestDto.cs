public class EmailRequestDto(string fromEmail, string targetEmail, string content, long toDoId)
{
    public string FromEmail {get; set;} = fromEmail;
    public string TargetEmail {get; set;} = targetEmail;
    public string Content {get; set;} = content;
    public long ToDoId {get; set;} = toDoId;
}