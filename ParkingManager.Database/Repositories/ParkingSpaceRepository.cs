using Microsoft.EntityFrameworkCore;
using ParkingManager.Database.Context;
using ParkingManager.Domain.Entitites;

namespace ParkingManager.Database.Repositories;
public class ParkingSpaceRepository : IParkingSpaceRepository
{
    private readonly ParkingManagerDbContext _dbContext;

    public ParkingSpaceRepository(ParkingManagerDbContext parkingManagerDbContext)
    {
        _dbContext = parkingManagerDbContext;
    }

    public async Task<IEnumerable<ParkingSpace>> GetAllAsync()
    {
        return await _dbContext
            .ParkingSpots
            .AsNoTracking()
            .Include(ps => ps.Reservations)
            .ToListAsync();
    }

    public async Task<ParkingSpace?> GetFirstAvailableAsync()
    {
        return await _dbContext.ParkingSpots
            .FirstOrDefaultAsync(ps => !ps.Reservations.Any(r => !r.EndDate.HasValue));
    }
}
