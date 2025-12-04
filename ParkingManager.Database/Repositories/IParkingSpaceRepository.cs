using ParkingManager.Domain.Entitites;

namespace ParkingManager.Database.Repositories;
public interface IParkingSpaceRepository
{
    Task<IEnumerable<ParkingSpace>> GetAllAsync();
    Task<ParkingSpace?> GetFirstAvailableAsync();
}
