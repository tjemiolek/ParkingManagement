using Microsoft.EntityFrameworkCore;
using ParkingManager.Domain.Entitites;

namespace ParkingManager.Database.Context;
public class ParkingManagerDbContext : DbContext
{
    public ParkingManagerDbContext(DbContextOptions<ParkingManagerDbContext> options)
            : base(options)
    { }

    public DbSet<Vehicle> Vehicles { get; set; } = null!;
    public DbSet<ParkingSpace> ParkingSpots { get; set; } = null!;
    public DbSet<Reservation> Reservations { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ParkingSpace>().HasData(
            new ParkingSpace { Id = 1, Number = 1 },
            new ParkingSpace { Id = 2, Number = 2 },
            new ParkingSpace { Id = 3, Number = 3 },
            new ParkingSpace { Id = 4, Number = 4 },
            new ParkingSpace { Id = 5, Number = 5 },
            new ParkingSpace { Id = 6, Number = 6 },
            new ParkingSpace { Id = 7, Number = 7 },
            new ParkingSpace { Id = 8, Number = 8 },
            new ParkingSpace { Id = 9, Number = 9 },
            new ParkingSpace { Id = 10, Number = 10 },
            new ParkingSpace { Id = 11, Number = 11 },
            new ParkingSpace { Id = 12, Number = 12 });
    }
}