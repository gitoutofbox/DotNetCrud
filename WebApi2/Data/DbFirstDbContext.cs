using Microsoft.EntityFrameworkCore;

namespace WebApi2.Data
{
    public class DbFirstDbContext: DbContext
    {
        public DbFirstDbContext(DbContextOptions<DbFirstDbContext> options): base(options) { 
        }
        public DbSet<Users> Users { get; set; }
    }
}
