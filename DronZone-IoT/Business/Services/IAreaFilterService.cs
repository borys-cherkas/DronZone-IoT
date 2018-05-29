using System.Collections.Generic;
using System.Threading.Tasks;
using DronZone_IoT.Models.AreaFilters;

namespace DronZone_IoT.Business.Services
{
    public interface IAreaFilterService
    {
        Task<ICollection<AreaFilterDetailedModel>> GetFiltersByAreaAsync(string areaId);

        Task CreateFilterAsync(AddFilterDetailedModel filterModel);

        Task<AreaFilterDetailedModel> GetFilterAsync(int filterId);
    }
}
