using firstproject.Data;
using firstproject.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace firstproject.Repositories;

public class ToDoRepository
{
    public ToDoRepository(AppDbContext context)
    {
        _context = context;
    }

    private readonly AppDbContext _context;


    public async Task<ToDo[]> GetAll()
    {
        return await _context.ToDos.ToArrayAsync();
    }

    public async Task<ToDo> Create(ToDo toDo)
    {
        _context.ToDos.Add(toDo);
        await _context.SaveChangesAsync();
        return toDo;
    }

    public async Task<List<ToDo>> GetAllFromUser(long userId)
    {
        var toDos = await _context.ToDos
        .Where(t => t.UserId.Equals(userId)).ToListAsync();
        return toDos;
    }

    public async Task<ToDo?> GetById(long id)
    {
        return await _context.ToDos.FirstOrDefaultAsync(t => t.Id.Equals(id));
    } 

    public async Task<ToDo> Toggle(ToDo toDo)
    {
        toDo.IsDone = !toDo.IsDone;
        await _context.SaveChangesAsync();
        return toDo;
    }

    public async Task<ToDo> Notify(ToDo toDo)
    {
        toDo.IsNotified = true;
        await _context.SaveChangesAsync();
        return toDo;
    }

}