using System.Collections.Generic;
using System.Threading.Tasks;
using DronZone_IoT.Models.Area;

namespace DronZone_IoT.Data.Api.APIs
{
    public interface IAreaRestApi
    {
        Task<ICollection<AreaDetailedModel>> GetCurrentUserAreasAsync();

        Task<AreaDetailedModel> GetDetailedAreaAsync(string areaId);
    }
}
