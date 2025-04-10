using Microsoft.EntityFrameworkCore;
using ReactWithAsp.Server.Models;

namespace ReactWithAsp.Server.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Employees> Employees { get; set; }
    
    }
}
