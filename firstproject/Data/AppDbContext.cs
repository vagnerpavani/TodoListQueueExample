using Microsoft.EntityFrameworkCore;
using firstproject.Models.Domain;

namespace firstproject.Data 

{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<ToDo> ToDos {get; set;}
        public DbSet<User> Users {get; set;}
    }
}