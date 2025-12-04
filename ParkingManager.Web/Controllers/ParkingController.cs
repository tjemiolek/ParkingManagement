using Microsoft.AspNetCore.Mvc;
using ParkingManager.Application.Requests;
using ParkingManager.Application.Responses;
using ParkingManager.Application.Services.ParkingService;

namespace ParkingManager.Web.Controllers;
[Route("parking")]
[ApiController]
public class ParkingController : ControllerBase
{
    private readonly IParkingService _parkingService;

    public ParkingController(IParkingService parkingService)
    {
        _parkingService = parkingService;
    }

    /// <summary>
    /// Returns information about parking spaces
    /// </summary>
    /// <returns>Information about available and occupied spaces</returns>
    [HttpGet]
    [ProducesResponseType(typeof(ParkingStatusResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> GetParkingStatus()
    {
        var result = await _parkingService.GetAvailableSpacesAsync();
        return Ok(result);
    }

    /// <summary>
    /// Leaves parking space
    /// </summary>
    /// <param name="request">LeaveParkingSpaceRequest</param>
    /// <returns>Information about reservation including price</returns>
    [HttpPost]
    [Route("exit")]
    [ProducesResponseType(typeof(LeaveParkingSpaceResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> LeaveParkingSpace(LeaveParkingSpaceRequest request)
    {
        var result = await _parkingService.LeaveParkingSpaceAsync(request);
        return Ok(result);
    }

    /// <summary>
    /// Parks a vehicle
    /// </summary>
    /// <param name="request">ParkVehicleRequest</param>
    /// <returns>Information about reservation</returns>
    [HttpPost]
    [ProducesResponseType(typeof(ParkVehicleResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> ParkVehicle(ParkVehicleRequest request)
    {
        var result = await _parkingService.ParkVehicleAsync(request);
        return Ok(result);
    }
}
