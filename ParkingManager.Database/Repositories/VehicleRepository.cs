using Microsoft.EntityFrameworkCore;
using ParkingManager.Database.Context;
using ParkingManager.Domain.Entitites;

namespace ParkingManager.Database.Repositories;
public class VehicleRepository : IVehicleRepository
{
    private readonly ParkingManagerDbContext _dbContext;

    public VehicleRepository(ParkingManagerDbContext parkingManagerDbContext)
    {
        _dbContext = parkingManagerDbContext;
    }

    public async Task<bool> AddAsync(Vehicle vehicle)
    {
        _dbContext.Vehicles.Add(vehicle);
        await _dbContext.SaveChangesAsync();
        return true;
    }

    public async Task<Vehicle?> GetByRegistrationNumberAsync(string registrationNumber)
    {
        return await _dbContext
            .Vehicles
            .AsNoTracking()
            .Include(x => x.Reservations)
            .FirstOrDefaultAsync(x => x.RegistrationNumber == registrationNumber);
    }
}
