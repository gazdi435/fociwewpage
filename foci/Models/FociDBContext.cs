using Microsoft.EntityFrameworkCore;

namespace foci.Models
{
    public class FociDBContext : DbContext
    {
        public FociDBContext(DbContextOptions<FociDBContext> options) : base(options)
        {
        }
        public DbSet<Meccs> Meccsek { get; set; }
    }
}
