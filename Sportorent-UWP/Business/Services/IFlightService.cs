using System.Collections.Generic;
using System.Threading.Tasks;
using DronZone_IoT.Models.Drone;
using DronZone_IoT.Models.Flights;

namespace DronZone_IoT.Business.Services
{
    public interface IFlightService
    {
        Task<DroneFlight> StartFlightAsync(string droneId);
        Task<bool> TryMovingAsync(string currentFlightId, double requestedLatitude, double requestedLongitude);
    }
}
