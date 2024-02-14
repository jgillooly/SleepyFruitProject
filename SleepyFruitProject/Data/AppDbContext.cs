using Microsoft.EntityFrameworkCore;

namespace SleepyFruitProject.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {

        }

    }
}
