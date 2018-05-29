using System.Collections.Generic;
using System.Threading.Tasks;
using DronZone_IoT.Models.AreaFilters;

namespace DronZone_IoT.Data.Api.APIs
{
    public interface IFilterRestApi
    {
        Task<ICollection<AreaFilterDetailedModel>> GetFiltersByAreaIdAsync(string areaId);

        Task<AreaFilterDetailedModel> GetFilterByIdAsync(int filterId);

        Task AddFilterAsync(AddFilterDetailedModel filterModel);
    }
}
