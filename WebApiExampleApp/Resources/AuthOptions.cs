using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApiExampleApp.Resources
{
    public static class AuthOptions
    {
        public const string ISSUER = "MyExampleApi";
        public const string AUDIENCE = "https://localhost:5001";
        const string KEY = "1234567890qwertyuiopasdfghj";
        public static TimeSpan TokenLifeTime = TimeSpan.FromMinutes(5);
        public static TimeSpan SessionLifeTime = TimeSpan.FromMinutes(60);
        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
        }
    }
}
