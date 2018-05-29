using System.Threading.Tasks;
using DronZone_IoT.Models.Flights;

namespace DronZone_IoT.Data.Api.APIs
{
    public interface IFlightRestApi
    {
        Task<DroneFlight> StartFlightAsync(string droneId);

        Task<bool> TryMovingAsync(string currentFlightId, double requestedLatitude, double requestedLongitude);
    }
}
