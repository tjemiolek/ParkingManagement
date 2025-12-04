namespace ParkingManager.Application.Responses;
public sealed record ParkingStatusResponse(int AvailableSpaces, int OccupiedSpaces);