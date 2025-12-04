namespace ParkingManager.Application.Responses;
public sealed record ParkVehicleResponse(string VehicleReg, int SpaceNumber, DateTime TimeIn);