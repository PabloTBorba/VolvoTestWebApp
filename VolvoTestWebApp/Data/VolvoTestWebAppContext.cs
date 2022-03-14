#nullable disable
using Microsoft.EntityFrameworkCore;

namespace VolvoTestWebApp.Data
{
    public class VolvoTestWebAppContext : DbContext
    {
        public VolvoTestWebAppContext(DbContextOptions<VolvoTestWebAppContext> options)
            : base(options)
        {
        }

        public DbSet<VolvoTestWebApp.Models.VolvoTruckModel> VolvoTrucks { get; set; }
    }
}
