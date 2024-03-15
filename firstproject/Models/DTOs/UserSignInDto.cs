using System.ComponentModel.DataAnnotations;
using firstproject.Models.Domain;

namespace firstproject.Models.DTOs;

public class UserSignInDto
{
    [Required]
    [EmailAddress]
    public required string Email { get; set; }

    [Required]
    public required string Password { get; set; }
}