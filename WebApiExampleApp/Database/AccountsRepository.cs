using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiExampleApp.Models;

namespace WebApiExampleApp.Database
{
    public class AccountsRepository
    {
        private readonly DbContextOptions<ApplicationDbContext> _options;
        public AccountsRepository(DbContextOptions<ApplicationDbContext> options)
        {
            _options = options;
        }

        public async Task <User> Logon(Account account)
        {
            using ApplicationDbContext context = new(_options);
            var user = await context.Users.FirstOrDefaultAsync(u => u.Login == account.Login);
            if (user != null)
            {
                if (BCrypt.Net.BCrypt.Verify(account.Password, user.PasswordHash))
                    return user;
            }
            return null;
        }
    }
}
