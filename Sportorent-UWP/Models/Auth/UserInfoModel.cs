using DronZone_IoT.Enums;

namespace DronZone_IoT.Models.Auth
{
    public class UserInfoModel
    {
        public string IdentityId { get; set; }

        public string PersonId { get; set; }

        public string UserName { get; set; }

        public PersonType PersonType { get; set; }

        public string Roles { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}