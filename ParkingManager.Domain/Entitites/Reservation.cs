using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ParkingManager.Domain.Entitites;
public class Reservation
{
    [Key]
    public int Id { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public int VehicleId { get; set; }
    [ForeignKey("VehicleId")]
    public Vehicle Vehicle { get; set; }
    public int ParkingSpaceId { get; set; }
    [ForeignKey("ParkingSpaceId")]
    public ParkingSpace ParkingSpace { get; set; }
    [Column(TypeName = "decimal(18,2)")]
    public decimal? Price { get; set; } 
}
