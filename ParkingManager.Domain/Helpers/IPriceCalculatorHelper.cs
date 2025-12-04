using ParkingManager.Domain.Enums;

namespace ParkingManager.Domain.Helpers;
public interface IPriceCalculatorHelper
{
    decimal CalculatePrice(TimeSpan time, VehicleType vehicleType);
}
