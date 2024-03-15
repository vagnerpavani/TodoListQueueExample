using System.ComponentModel.DataAnnotations;

public class MailBodyDto
{
    [EmailAddress]
    [Required]
    public required string FromEmail {get; set;}
    [EmailAddress]
    [Required]
    public required string TargetEmail {get; set;}
    [Required]
    public required string Content {get; set;}
    public MailBodyDto()
    {}
}