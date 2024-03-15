using System.ComponentModel.DataAnnotations;
using firstproject.Models.Domain;

namespace firstproject.Models.DTOs;

public class CreateUserDto
{
    [Required]
    public required string Name { get; set; }

    [Required]
    [EmailAddress]
    public required string Email { get; set; }

    [Required]
    public required string Password { get; set; }

    public CreateUserDto(string name, string email, string password)
    {
        Name = name;
        Email = email;
        Password = password;
    }

    public User toEntity()
    {
        return new User(Name, Email, Password);
    }
}