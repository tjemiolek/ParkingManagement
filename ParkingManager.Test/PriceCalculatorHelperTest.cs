using ParkingManager.Domain.Enums;
using ParkingManager.Domain.Helpers;

namespace ParkingManager.Test;

[TestClass]
public sealed class PriceCalculatorHelperTest
{
    [TestMethod]
    [DynamicData(nameof(TestData))]
    public void CalculatePrice(int elapsedInSeconds, VehicleType vehicleType, decimal expectedPrice)
    {
        // Arrange
        var priceCalculator = new PriceCalculatorHelper();
        var timeSpan = TimeSpan.FromSeconds(elapsedInSeconds);

        // Act
        var calculatedPrice = priceCalculator.CalculatePrice(timeSpan, vehicleType);

        // Assert
        Assert.AreEqual(expectedPrice, calculatedPrice);
    }

    public static IEnumerable<object[]> TestData =>
       [[300, VehicleType.Small, 1.50m],
        [716, VehicleType.Small, 3.20m],
        [3612, VehicleType.Medium, 24.20m],
        [114, VehicleType.Medium, 0.40m],
        [51, VehicleType.Large, 0.40m],
        [210, VehicleType.Large, 1.60m]];
}
