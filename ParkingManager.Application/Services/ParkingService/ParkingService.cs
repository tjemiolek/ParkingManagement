using ParkingManager.Application.Requests;
using ParkingManager.Application.Responses;
using ParkingManager.Database.Repositories;
using ParkingManager.Domain.Entitites;
using ParkingManager.Domain.Enums;
using ParkingManager.Domain.Helpers;

namespace ParkingManager.Application.Services.ParkingService;

public class ParkingService : IParkingService
{
    private readonly IParkingSpaceRepository _parkingSpaceRepository;
    private readonly IPriceCalculatorHelper _priceCalculatorHelper;
    private readonly IReservationRepository _reservationRepository;
    private readonly TimeProvider _timeProvider;
    private readonly IVehicleRepository _vehicleRepository;

    public ParkingService(
        IPriceCalculatorHelper priceCalculatorHelper,
        IParkingSpaceRepository parkingSpaceRepository,
        IVehicleRepository vehicleRepository,
        IReservationRepository reservationRepository,
        TimeProvider timeProvider)
    {
        _priceCalculatorHelper = priceCalculatorHelper;
        _vehicleRepository = vehicleRepository;
        _reservationRepository = reservationRepository;
        _parkingSpaceRepository = parkingSpaceRepository;
        _timeProvider = timeProvider;
    }

    public async Task<ParkVehicleResponse> ParkVehicleAsync(ParkVehicleRequest request)
    {
        var vehicle = await GetOrAddVehicleAsync(request.VehicleReg, request.VehicleType);
        if (vehicle.Reservations.Any(r => r.EndDate == null)) throw new Exception("Car already parked");

        var availableParkingSpace = await _parkingSpaceRepository.GetFirstAvailableAsync();
        if (availableParkingSpace == null) throw new Exception("No available spots");

        var reservation = new Reservation
        {
            StartDate = _timeProvider.GetUtcNow().DateTime,
            ParkingSpaceId = availableParkingSpace.Id,
            Vehicle = vehicle
        };

        await _reservationRepository.AddAsync(reservation);

        return new ParkVehicleResponse(request.VehicleReg, availableParkingSpace.Number,
            reservation.StartDate);
    }

    public async Task<ParkingStatusResponse> GetAvailableSpacesAsync()
    {
        var allParkingSpaces = await _parkingSpaceRepository.GetAllAsync();

        return new ParkingStatusResponse(allParkingSpaces.Count(ps => !ps.Reservations.Any(r => !r.EndDate.HasValue)),
            allParkingSpaces.Count(ps => ps.Reservations.Any(r => !r.EndDate.HasValue)));
    }

    public async Task<LeaveParkingSpaceResponse> LeaveParkingSpaceAsync(LeaveParkingSpaceRequest request)
    {
        var vehicle = await _vehicleRepository.GetByRegistrationNumberAsync(request.VehicleReg) ??
                  throw new Exception("Vehicle not found");

        var reservation = vehicle.Reservations.FirstOrDefault(r => r.EndDate is null) ??
                          throw new Exception("No car with given registration number is parked");

        var endDate = _timeProvider.GetUtcNow().DateTime;
        var timeElapsed = endDate - reservation.StartDate;
        var finalPrice = CalculatePrice(timeElapsed, vehicle.VehicleType);

        reservation.EndDate = endDate;
        reservation.Price = finalPrice;

        await _reservationRepository.UpdateAsync(reservation);

        return new LeaveParkingSpaceResponse(request.VehicleReg, (double)finalPrice, reservation.StartDate,
            reservation.EndDate.Value);
    }

    private async Task<Vehicle> GetOrAddVehicleAsync(string registrationNumber, VehicleType vehicleType)
    {
        var existingVehicle = await _vehicleRepository.GetByRegistrationNumberAsync(registrationNumber);

        if (existingVehicle is not null) return existingVehicle;

        var newVehicle = new Vehicle
        {
            RegistrationNumber = registrationNumber,
            VehicleType = vehicleType,
            Reservations = []
        };

        await _vehicleRepository.AddAsync(newVehicle);

        return newVehicle;
    }

    private decimal CalculatePrice(TimeSpan time, VehicleType vehicleType)
    {
        return _priceCalculatorHelper.CalculatePrice(time, vehicleType);
    }
}