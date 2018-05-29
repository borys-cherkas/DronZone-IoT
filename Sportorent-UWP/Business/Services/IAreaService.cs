using System.Collections.Generic;
using System.Threading.Tasks;
using DronZone_IoT.Models.Area;

namespace DronZone_IoT.Business.Services
{
    public interface IAreaService
    {
        Task<ICollection<AreaDetailedModel>> GetCurrentUserAreasAsync();
        
        Task<AreaDetailedModel> GetDetailedAreaAsync(string areaId);

        //Task<bool> AddReservationAsync(AddReservationModel model);
    }
}
