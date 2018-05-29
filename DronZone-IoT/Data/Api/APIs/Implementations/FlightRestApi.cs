using System;
using System.Globalization;
using System.Threading.Tasks;
using DronZone_IoT.Data.Api.Rest;
using DronZone_IoT.Models.Flights;

namespace DronZone_IoT.Data.Api.APIs.Implementations
{
    public class FlightRestApi : RestApiBase, IFlightRestApi
    {
        private const string BaseApiAddress = ApiRouting.BaseApiUrl;
        private const string ControllerPath = "Flight";

        public FlightRestApi() : base(new Uri(BaseApiAddress)) { }
        
        public Task<DroneFlight> StartFlightAsync(string droneId)
        {
            return Url($"{ControllerPath}/startFlight/{droneId}")
                .FormUrlEncoded()
                .PostAsync<DroneFlight>();
        }

        public Task<bool> TryMovingAsync(string currentFlightId, double requestedLatitude, double requestedLongitude)
        {
            return Url($"{ControllerPath}/tryMove")
                .FormUrlEncoded()
                .Param("flightId", currentFlightId)
                .Param("latitude", requestedLatitude.ToString(CultureInfo.InvariantCulture))
                .Param("longitude", requestedLongitude.ToString(CultureInfo.InvariantCulture))
                .PostAsync<bool>();
        }
    }
}
