using InterrogateMe.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace InterrogateMe.Data
{
    public class TodoDbContext : DbContext
    {
        public TodoDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Topic> Topics { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Link> Links { get; set; }
    }
}
