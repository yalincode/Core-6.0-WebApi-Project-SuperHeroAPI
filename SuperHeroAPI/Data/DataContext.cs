using Microsoft.EntityFrameworkCore;
using SuperHeroAPI;

namespace Data.SuperHeroAPI
{
    public class DataContext:DbContext
    {
        public DataContext(DbContextOptions<DataContext> options):base(options)
        {
            
        }
        public DbSet<SuperHero> SuperHeroes { get; set; }
    }
}
