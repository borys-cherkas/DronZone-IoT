using System.Threading.Tasks;
using DronZone_IoT.Models.Auth;

namespace DronZone_IoT.Data.Api.APIs
{
    public interface IAuthRestApi
    {
        Task RegisterAsync(RegistrationModel registrationModel);

        Task<GetTokenModel> RetrieveTokenAsync(string username, string password, bool rememberme);

        Task<UserInfoModel> GetCurrentUserAsync();
    }
}
