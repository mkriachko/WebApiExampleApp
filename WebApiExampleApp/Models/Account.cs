using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiExampleApp.Models
{
    public class Account
    {
        public string Login { get; set; }
        public string Password { get; set; }
    }

    public class NewAccount : Account
    {
        public string Email { get; set; }
    }

    public class Token
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }

    public class Session
    {
        public string Login { get; set; }
        public DateTime LoggedOn { get; set; }
        public string RefreshToken { get; set; }
    }
}
