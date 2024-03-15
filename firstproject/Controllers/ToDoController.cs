using firstproject.Models.DTOs;
using firstproject.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using firstproject.Helpers;
using System.Security.Claims;
using firstproject.Models.Domain;

[ApiController]
[Route("[controller]")]

public class ToDoController : ControllerBase
{

    private readonly ToDoService _toDoService;
    public ToDoController(ToDoService toDoService, EmailService emailService)
    {
        _toDoService = toDoService;
    }

    [Authorize]
    [HttpGet("all")]
    public async Task<IActionResult> GetAll()
    {
        var toDos = await _toDoService.GetAllTasks();
        return Ok(toDos);
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> CreateToDo(CreateToDoDto createToDoDto)
    {
        long userId = TokenHelper.GetUserId(User.FindFirst(ClaimTypes.NameIdentifier));
        ToDo newTodo = await _toDoService.Create(createToDoDto, userId);

        return CreatedAtAction(null, null, newTodo);
    }

    [Authorize]
    [HttpGet]
    public async Task<IActionResult> GetFromUser()
    {
        long userId = TokenHelper.GetUserId(User.FindFirst(ClaimTypes.NameIdentifier));
        return Ok(await _toDoService.GetAllFromUser(userId));
    }

    [Authorize]
    [HttpPatch("/{ToDoId}")]
    public async Task<IActionResult> Toggle([FromRoute] ToggleToDoDto data)
    {
        long userId = TokenHelper.GetUserId(User.FindFirst(ClaimTypes.NameIdentifier));

        var toDo = await _toDoService.Toggle(data, userId);
        return Ok(toDo);
    }


    [HttpPatch("notify/{ToDoId}")]
    public async Task<IActionResult> NotifyToDo([FromRoute] ToggleToDoDto data)
    {
        var toDo = await _toDoService.Notify(data);
        return Ok(toDo);
    }
}