using ParkingManager.Domain.Enums;

namespace ParkingManager.Application.Requests;
public sealed record ParkVehicleRequest(string VehicleReg, VehicleType VehicleType);
