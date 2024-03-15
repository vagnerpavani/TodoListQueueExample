using firstproject.Exceptions;
using firstproject.Models.Domain;
using firstproject.Models.DTOs;
using firstproject.Repositories;

namespace firstproject.Services
{
    public class ToDoService 
    {
        public ToDoRepository _toDoRepository;
        public UserService _userService;
        public EmailService _emailService;

        public ToDoService (ToDoRepository toDoRepository, UserService userService, EmailService emailService)
        {
            _toDoRepository = toDoRepository;
            _userService = userService;
            _emailService = emailService;
        }

        public async Task<ToDo[]> GetAllTasks()
        {
            return await _toDoRepository.GetAll();
        }

        public async Task<ToDo> Create(CreateToDoDto data, long userId)
        {
            ToDo newToDo = data.toEntity();
            newToDo.UserId = userId;

            var result = await _toDoRepository.Create(newToDo);

            User? user = await _userService.FindById(userId);

            var emailDto = new SendEmailDto("WeLoveToDoList@email.com", user.Email, "You just created a new To Do!!", newToDo.Id);

            _emailService.SendEmail(emailDto);
            return result;
        }

        public async Task<List<ToDo>> GetAllFromUser(long userId)
        {
            return await _toDoRepository.GetAllFromUser(userId);
        }

        public async Task<ToDo> Toggle(ToggleToDoDto dto, long userId)
        {
            ToDo? toDo = await _toDoRepository.GetById(dto.ToDoId);

            if(toDo == null) throw new ToDoNotFoundException("To Do not found!");

            if(toDo.UserId != userId) throw new UnauthorizedException("You don't have the access!");

            return await _toDoRepository.Toggle(toDo);
        }

        public async Task<ToDo> Notify(ToggleToDoDto dto)
        {
            ToDo? toDo = await _toDoRepository.GetById(dto.ToDoId);

            if(toDo == null) throw new ToDoNotFoundException("To Do not found!");

            return await _toDoRepository.Notify(toDo);
        }
    }
}
