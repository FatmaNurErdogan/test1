using Microsoft.EntityFrameworkCore;
using test1.Models;

namespace test1.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        { }
        public DbSet<Book> Books { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet <Borrowed> Borrowed { get; set; }
    }
}
