using Microsoft.EntityFrameworkCore;
using StarChart.Models;

namespace StarChart.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<CelestialObjects> CelestialObjects { get; set; }

        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }
    }
}
