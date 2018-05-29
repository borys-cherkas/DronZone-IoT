using System.Net.Http;

namespace DronZone_IoT.Data.Api.Rest
{
    public interface IRequestInterceptor
    {
        void RemoveInterceptorIfExist(string key);

        void Intercept(HttpClient httpClient);
    }
}
