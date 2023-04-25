using Drones.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace Drones.Infrastructure.DataContext
{
    public class AppDBContext:DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options)
            :base(options)
        {

        }

        public DbSet<Drone> Drones { get; set; }
        public DbSet<Medication> Medications { get; set; }
    }
}
