using ParkingManager.Domain.Enums;

namespace ParkingManager.Domain.Helpers;

public class PriceCalculatorHelper : IPriceCalculatorHelper
{
    private const decimal AdditionalCost = 1;
    private const int AdditionalCostDurationInMinutes = 5;
    private static readonly Dictionary<VehicleType, decimal> _prices = new()
    {
        { VehicleType.Small, 0.10m },
        { VehicleType.Medium, 0.20m },
        { VehicleType.Large, 0.40m }
    };

    public decimal CalculatePrice(TimeSpan time, VehicleType vehicleType)
    {
        // Get elapsed time in minutes
        var timeInMinutes = (decimal)Math.Ceiling(time.TotalMinutes);
        _prices.TryGetValue(vehicleType, out var price);

        // Get base price 
        var basePrice = price * timeInMinutes;

        // Ger additional price for each extra 5 minutes
        var additionalPrice = Math.Floor(timeInMinutes / AdditionalCostDurationInMinutes) * AdditionalCost;

        // Return final price
        return basePrice + additionalPrice;
    }
}