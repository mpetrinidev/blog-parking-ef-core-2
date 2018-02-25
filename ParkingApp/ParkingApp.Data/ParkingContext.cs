using Microsoft.EntityFrameworkCore;
using ParkingApp.Domain;

namespace ParkingApp.Data
{
    public class ParkingContext : DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<Parking> Parkings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasMany(p => p.Vehicles).WithOne(p => p.Category).HasForeignKey(p => p.CateogoryId);
            modelBuilder.Entity<Vehicle>().HasMany(p => p.Parkings).WithOne(p => p.Vehicle).HasForeignKey(p => p.VehicleId);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseSqlServer(@"");
        }
    }
}
