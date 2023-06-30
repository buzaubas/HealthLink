using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace HealthLink.API.Models
{
    public class AuthOptions
    {
        public const string ISSUER = "https://www.itstep.com";
        public const string AUDIENCE = "https://www.itstep.com";
        const string KEY = "eXKArMn5kvFy9XnZstxzUmPARHNYbBus\r\n1sVyR4eyU35y4AN6Cm3l5Akbbm0eO9HG\r\nFUU1usSf8WTbmtbr0MaxfYzQT8b0EVEn\r\nV0RWUXCuP8BOB39CMxLd8PfmDnioMRV4";
        public const int LIFETIME = 60;
        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(KEY));
        }
    }
}
