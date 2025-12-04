using ParkingManager.Domain.Entitites;

namespace ParkingManager.Database.Repositories;
public interface IReservationRepository
{
    Task<bool> AddAsync(Reservation reservation);
    Task<Reservation> UpdateAsync(Reservation reservation);
}
