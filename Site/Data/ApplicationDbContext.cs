using Microsoft.EntityFrameworkCore;
using Site.Models;

namespace Site.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<GeneralForm> SearchRequests { get; set; }
    }
}
