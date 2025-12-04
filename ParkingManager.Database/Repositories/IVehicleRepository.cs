using ParkingManager.Domain.Entitites;

namespace ParkingManager.Database.Repositories;
public interface IVehicleRepository
{
    Task<Vehicle?> GetByRegistrationNumberAsync(string registrationNumber);
    Task<bool> AddAsync(Vehicle vehicle);
}
