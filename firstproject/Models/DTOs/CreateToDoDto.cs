using System.ComponentModel.DataAnnotations;
using firstproject.Models.Domain;

namespace firstproject.Models.DTOs;

public class CreateToDoDto
{
    [Required]
    public required string Description { get; set; }


    public CreateToDoDto(string description)
    {
        Description = description;
       
    }

    public ToDo toEntity()
    {
        return new ToDo(Description);
    }
}