using System.Collections.Generic;
using System.Threading.Tasks;
using DronZone_IoT.Models.Drone;

namespace DronZone_IoT.Data.Api.APIs
{
    public interface IDroneRestApi
    {
        Task<ICollection<DroneDetailedModel>> GetUserDronesAsync();

        Task<DroneDetailedModel> GetDetailedDroneAsync(string droneId);

        Task AttachDroneAsync(string code);
    }
}
