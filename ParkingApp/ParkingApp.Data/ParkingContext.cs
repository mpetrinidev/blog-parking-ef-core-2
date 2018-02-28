using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using ParkingApp.Domain;

namespace ParkingApp.Data
{
    public class ParkingContext : DbContext
    {
        public static readonly LoggerFactory loggerFactory =
            new LoggerFactory(new[] {
                new ConsoleLoggerProvider((c, l) => 
                    c == DbLoggerCategory.Database.Command.Name && l == LogLevel.Information, true)
            });

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
                .UseLoggerFactory(loggerFactory)
                .EnableSensitiveDataLogging(true)
                .UseSqlServer(@"data source=.\SQLEXPRESS02;initial catalog=ParkingDB;integrated security=true;");
        }
    }
}
