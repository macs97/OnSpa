using Microsoft.EntityFrameworkCore;
using OnSpa.Web.Data.Entities;

namespace OnSpa.Web.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<Appointment> Appointments { get; set; }

        public DbSet<City> Cities { get; set; }

        public DbSet<Country> Countries { get; set; }

        
        public DbSet<Campus> Campuses { get; set; }

        public DbSet<Service> Services { get; set; }
       

    }
}
