
using System.ComponentModel.DataAnnotations;

namespace firstproject.Models.DTOs;

public class ToggleToDoDto
{
    [Required]
    public long ToDoId {get; set;}
}