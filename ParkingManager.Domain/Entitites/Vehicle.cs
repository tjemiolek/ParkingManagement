using ParkingManager.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace ParkingManager.Domain.Entitites;
public class Vehicle
{
    [Key]
    public int Id { get; set; }
    public string RegistrationNumber { get; set; } = string.Empty;
    public VehicleType VehicleType { get; set; }
    public ICollection<Reservation> Reservations { get; set; }
}
