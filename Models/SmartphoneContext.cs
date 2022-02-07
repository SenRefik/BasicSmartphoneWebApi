using Microsoft.EntityFrameworkCore;

namespace FirstWeekProject.Models
{
    public class SmartphoneContext : DbContext
    {
        public SmartphoneContext(DbContextOptions<SmartphoneContext> options)
            : base(options)
        {
        }

        public DbSet<Smartphone> Smartphones { get; set; }
    }
}
