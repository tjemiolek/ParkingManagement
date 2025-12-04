using System.ComponentModel.DataAnnotations;

namespace ParkingManager.Domain.Entitites;
public class ParkingSpace
{
    [Key]
    public int Id { get; set; }
    public int Number { get; set; }
    public ICollection<Reservation> Reservations { get; set; }
}
