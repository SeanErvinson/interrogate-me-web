using InterrogateMe.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace InterrogateMe.Data
{
    public class InterrogateDbContext : DbContext
    {
        public InterrogateDbContext(DbContextOptions options) : base(options)
        {

        }
        
        public DbSet<IpAddress> IpAddresses { get; set; }
        public DbSet<Topic> Topics { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Link> Links { get; set; }
        public DbSet<ProfaneWord> ProfaneWords { get; set; }
    }
}
