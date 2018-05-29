using System.Collections.Generic;
using System.Threading.Tasks;
using DronZone_IoT.Models.Drone;

namespace DronZone_IoT.Business.Services
{
    public interface IDroneService
    {
        Task<ICollection<DroneDetailedModel>> GetUserDronesAsync();

        Task<DroneDetailedModel> GetDetailedDroneAsync(string droneId);

        Task AttachDroneAsync(string code);
    }
}
