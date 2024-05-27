using MicroService.Domain.Class;
using Microsoft.EntityFrameworkCore;

namespace MicroService.Domain.Repository
{
    public class DatabaseContext : DbContext
    {

        public DbSet<Item> Items { get; set; }
        public DbSet<User> Users { get; set; }


        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
            Database.EnsureCreated();
        }


    }
}
