using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DronZone_IoT.Data.Api.Rest;
using DronZone_IoT.Models.Drone;

namespace DronZone_IoT.Data.Api.APIs.Implementations
{
    public class DroneRestApi : RestApiBase, IDroneRestApi
    {
        private const string BaseApiAddress = ApiRouting.BaseApiUrl;
        private const string ControllerPath = "UserDrones";

        public DroneRestApi() : base(new Uri(BaseApiAddress)) { }

        public Task<ICollection<DroneDetailedModel>> GetUserDronesAsync()
        {
            return Url($"{ControllerPath}/GetUserDrones")
                .GetAsync<ICollection<DroneDetailedModel>>();
        }

        public Task<DroneDetailedModel> GetDetailedDroneAsync(string droneId)
        {
            return Url($"{ControllerPath}/GetById/{droneId}")
                .GetAsync<DroneDetailedModel>();
        }

        public Task AttachDroneAsync(string code)
        {
            return Url($"{ControllerPath}/registerByCode")
                .FormUrlEncoded()
                .Param("code", code)
                .PostAsync<object>();
        }
    }
}
