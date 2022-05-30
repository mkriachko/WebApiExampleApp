using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiExampleApp.Resources
{
    public static class Roles
    {
        public const string User = "User";
        public const string Admin = "Admin";

        public static string[] GetRoles()
        {
            return new string[] { User, Admin };
        }
    }
}
