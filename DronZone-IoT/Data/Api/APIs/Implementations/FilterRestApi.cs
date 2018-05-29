using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using DronZone_IoT.Data.Api.Rest;
using DronZone_IoT.Models.AreaFilters;

namespace DronZone_IoT.Data.Api.APIs.Implementations
{
    public class FilterRestApi : RestApiBase, IFilterRestApi
    {
        private const string BaseApiAddress = ApiRouting.BaseApiUrl;
        private const string ControllerPath = "AreaFilters";

        public FilterRestApi() : base(new Uri(BaseApiAddress))
        {
        }

        public async Task<ICollection<AreaFilterDetailedModel>> GetFiltersByAreaIdAsync(string areaId)
        {
            return await Url($"{ControllerPath}/getAreaFilters/{areaId}")
                .GetAsync<ICollection<AreaFilterDetailedModel>>();
        }

        public Task<AreaFilterDetailedModel> GetFilterByIdAsync(int filterId)
        {
            return Url($"{ControllerPath}/getFilterById/{filterId}")
                .GetAsync<AreaFilterDetailedModel>();
        }

        public Task AddFilterAsync(AddFilterDetailedModel filterModel)
        {
            return Url($"{ControllerPath}/create")
                .FormUrlEncoded()
                .Param(nameof(filterModel.AreaId), filterModel.AreaId)
                .Param(nameof(filterModel.DroneType), filterModel.DroneType.ToString())
                .Param(nameof(filterModel.MaxAvailableWeigth), filterModel.MaxAvailableWeigth.ToString(CultureInfo.InvariantCulture))
                .Param(nameof(filterModel.MaxDroneSpeed), filterModel.MaxDroneSpeed.ToString(CultureInfo.InvariantCulture))
                .Param(nameof(filterModel.MaxDroneWeigth), filterModel.MaxDroneWeigth.ToString(CultureInfo.InvariantCulture))
                .PostAsync<object>();
        }
    }
}