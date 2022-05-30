using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiExampleApp.Models
{
    public class User
    {
        public User()
        {
            this.Friends = new HashSet<User>();
        }

        public int Id { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string AvatarUrl { get; set; }
        public string Role { get; set; }

        public virtual ICollection<User> Friends { get; set; }

    }
}
