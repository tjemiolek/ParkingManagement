namespace ParkingManager.Application.Responses;
public sealed record LeaveParkingSpaceResponse(string VehicleReg, double VehicleCharge, DateTime TimeIn, DateTime TimeOut);
