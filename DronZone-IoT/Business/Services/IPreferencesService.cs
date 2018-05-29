using System;
using DronZone_IoT.Models.Auth;

namespace DronZone_IoT.Business.Services
{
    public interface IPreferencesService
    {
        DateTime LastUpdateTokenTime { get; set; }

        GetTokenModel TokenInfo { get; set; }

        UserInfoModel UserInfo { get; set; }

        string AccessToken { get; }

        bool IsLoggedIn { get; }

        void Clear();
    }
}