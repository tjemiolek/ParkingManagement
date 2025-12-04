using ParkingManager.Application.Requests;
using ParkingManager.Application.Responses;

namespace ParkingManager.Application.Services.ParkingService;
public interface IParkingService
{
    Task<ParkVehicleResponse> ParkVehicleAsync(ParkVehicleRequest request);
    Task<LeaveParkingSpaceResponse> LeaveParkingSpaceAsync(LeaveParkingSpaceRequest request);
    Task<ParkingStatusResponse> GetAvailableSpacesAsync();
}
