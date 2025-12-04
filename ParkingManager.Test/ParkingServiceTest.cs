using Microsoft.Extensions.Time.Testing;
using Moq;
using ParkingManager.Application.Requests;
using ParkingManager.Application.Responses;
using ParkingManager.Application.Services.ParkingService;
using ParkingManager.Database.Repositories;
using ParkingManager.Domain.Entitites;
using ParkingManager.Domain.Enums;
using ParkingManager.Domain.Helpers;

namespace ParkingManager.Test;

[TestClass]
public class ParkingServiceTest
{
    private IPriceCalculatorHelper _priceCalculatorHelper;
    private IParkingSpaceRepository _parkingSpotRepository;
    private IVehicleRepository _vehicleRepository;
    private IReservationRepository _reservationRepository;
    private FakeTimeProvider _fakeTimeProvider;
    private DateTimeOffset fixedDateTime = DateTimeOffset.UtcNow;

    [TestMethod]
    public async Task ParkVehicle_Success()
    {
        // Arrange
        var expectedResult = new ParkVehicleResponse("REG11100", 1, fixedDateTime.DateTime);

        PrepareMocks();
        var service = new ParkingService(_priceCalculatorHelper, _parkingSpotRepository, _vehicleRepository, _reservationRepository, _fakeTimeProvider);

        // Act
        var request = new ParkVehicleRequest("REG11100", VehicleType.Small);

        var result = await service.ParkVehicleAsync(request);

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual(expectedResult.VehicleReg, result.VehicleReg);
        Assert.AreEqual(expectedResult.TimeIn, result.TimeIn);
        Assert.AreEqual(expectedResult.SpaceNumber, result.SpaceNumber);
    }

    public void PrepareMocks()
    {
        var vehicle = new Vehicle()
        {
            Id = 1,
            RegistrationNumber = "REG11100",
            VehicleType = VehicleType.Small,
            Reservations = []
        };

        var availableSpace = new ParkingSpace()
        {
            Id = 1,
            Number = 1
        };

        _priceCalculatorHelper = new Mock<IPriceCalculatorHelper>().Object;

        var vehicleRepository = new Mock<IVehicleRepository>();
        vehicleRepository.Setup(vr => vr.GetByRegistrationNumberAsync(It.IsAny<string>())).ReturnsAsync((string registrationNumber) => vehicle);
        _vehicleRepository = vehicleRepository.Object;

        var parkingSpotRepository = new Mock<IParkingSpaceRepository>();
        parkingSpotRepository.Setup(pr => pr.GetFirstAvailableAsync()).ReturnsAsync(() => availableSpace);
        _parkingSpotRepository = parkingSpotRepository.Object;

        var reservationRepository = new Mock<IReservationRepository>();
        reservationRepository.Setup(rr => rr.AddAsync(It.IsAny<Reservation>())).ReturnsAsync(() => true);
        _reservationRepository = reservationRepository.Object;

        _fakeTimeProvider = new FakeTimeProvider();
        _fakeTimeProvider.SetUtcNow(fixedDateTime);
    }
}
