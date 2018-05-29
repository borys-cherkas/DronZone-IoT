using System.Collections.Generic;
using System.Threading.Tasks;
using DronZone_IoT.Data.Api.APIs;
using DronZone_IoT.Models.Drone;
using DronZone_IoT.Models.Flights;

namespace DronZone_IoT.Business.Services.Implementations
{
    internal class FlightService : ServiceBase, IFlightService
    {
        private readonly IFlightRestApi _flightRestApi;

        public FlightService(IFlightRestApi flightRestApi)
        {
            _flightRestApi = flightRestApi;
        }

        public async Task<DroneFlight> StartFlightAsync(string droneId)
        {
            return await ExecuteSafeApiRequestAsync(
                async () => await _flightRestApi.StartFlightAsync(droneId));
        }

        public async Task<bool> TryMovingAsync(string currentFlightId, double requestedLatitude, double requestedLongitude)
        {
            return await ExecuteSafeApiRequestAsync(
                async () => await _flightRestApi.TryMovingAsync(currentFlightId, requestedLatitude, requestedLongitude));
        }
    }
}