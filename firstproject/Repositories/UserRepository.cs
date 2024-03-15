using firstproject.Data;
using firstproject.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace firstproject.Repositories;

public class UserRepository
{
    public UserRepository(AppDbContext context)
    {
        _context = context;
    }

    private readonly AppDbContext _context;

    public async Task<User> CreateUser(User user)
    {
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
        return user;
    }

    public async Task<User?> FindByEmail(string email)
    {
        User? user =await _context.Users.FirstOrDefaultAsync(u => u.Email.Equals(email));
        return user;
    }

    public async Task<User?> FindById(long userId)
    {
        User? user = await _context.Users.FirstOrDefaultAsync(u => u.Id.Equals(userId));
        return user;
    }
}