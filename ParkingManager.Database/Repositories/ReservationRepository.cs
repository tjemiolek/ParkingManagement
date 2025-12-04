using ParkingManager.Database.Context;
using ParkingManager.Domain.Entitites;

namespace ParkingManager.Database.Repositories;
public class ReservationRepository : IReservationRepository
{
    private readonly ParkingManagerDbContext _dbContext;

    public ReservationRepository(ParkingManagerDbContext parkingManagerDbContext)
    {
        _dbContext = parkingManagerDbContext;
    }
    public async Task<bool> AddAsync(Reservation reservation)
    {
        _dbContext.Reservations.Add(reservation);
        await _dbContext.SaveChangesAsync();
        return true;
    }

    public async Task<Reservation> UpdateAsync(Reservation reservation)
    {
        _dbContext.Reservations.Update(reservation);
        await _dbContext.SaveChangesAsync();
        return reservation;
    }
}
