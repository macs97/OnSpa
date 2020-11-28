using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OnSpa.Web.Data.Entities;

namespace OnSpa.Web.Data
{
    public class DataContext : IdentityDbContext<User>
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<Appointment> Appointments { get; set; }

        public DbSet<Campus> Campuses { get; set; }

        public DbSet<City> Cities { get; set; }

        public DbSet<Department> Departments { get; set; }

        public DbSet<Service> Services { get; set; }

        public DbSet<ServiceImage> ServiceImages { get; set; }

        public DbSet<ServiceType> ServiceTypes { get; set; }

        public DbSet<ServiceTypeCampus> ServiceTypeCampuses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<Department>(deparment =>
            {
                deparment.HasIndex("Name").IsUnique();
                deparment.HasMany(d => d.Cities).WithOne(c => c.Department).OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<City>(city =>
            {
                city.HasIndex("Name").IsUnique();
                city.HasMany(c => c.Campuses).WithOne(c => c.City).OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Campus>(campus =>
            {
                campus.HasIndex("Name").IsUnique();
                campus.HasOne(c => c.City).WithMany(c => c.Campuses).OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<ServiceTypeCampus>()
            .HasKey(sc => new { sc.ServiceTypeId, sc.CampusId });

            modelBuilder.Entity<ServiceTypeCampus>()
            .HasOne(sc => sc.Campus)
            .WithMany(c => c.ServiceTypeCampuses)
            .HasForeignKey(sc => sc.CampusId);

            modelBuilder.Entity<ServiceTypeCampus>()
                .HasOne(sc => sc.ServiceType)
                .WithMany(s => s.ServiceTypeCampuses)
                .HasForeignKey(sc => sc.ServiceTypeId);

            modelBuilder.Entity<Service>()
                .HasIndex(t => t.Name)
                .IsUnique();

            modelBuilder.Entity<ServiceType>()
                .HasIndex(t => t.Name)
                .IsUnique();
        }
    }
}
