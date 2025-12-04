using Moq;
using ParkingManager.Application.Requests;
using ParkingManager.Application.Responses;
using ParkingManager.Application.Services.ParkingService;
using ParkingManager.Domain.Enums;
using ParkingManager.Web.Controllers;

namespace ParkingManager.Test;

[TestClass]
public class ParkingControllerTest
{
    [TestMethod]
    public async Task ParkingController_ParkVehicle_Success()
    {
        // Arrange
        var request = new ParkVehicleRequest("REG11112", VehicleType.Medium);

        var service = new Mock<IParkingService>();
        service.Setup(ps => ps.ParkVehicleAsync(It.IsAny<ParkVehicleRequest>())).ReturnsAsync((ParkVehicleRequest request) => It.IsAny<ParkVehicleResponse>());
        var controller = new ParkingController(service.Object);

        // Act
        var result = await controller.ParkVehicle(request);

        // Assert
        Assert.IsNotNull(result);
    }
}
