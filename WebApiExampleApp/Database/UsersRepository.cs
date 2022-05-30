using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiExampleApp.Models;

namespace WebApiExampleApp.Database
{
    public class UsersRepository
    {
        private readonly DbContextOptions<ApplicationDbContext> _options;
        public UsersRepository(DbContextOptions<ApplicationDbContext> options)
        {
            _options = options;
        }

        public async Task<IEnumerable<User>> Get()
        {
            using ApplicationDbContext context = new(_options);
            var users = await context.Users.ToListAsync();
            return users;
        }

        public async Task<IEnumerable<User>> GetFriends(string login)
        {
            using ApplicationDbContext context = new(_options);
            var users = (await context.Users.FirstAsync(x => x.Login == login)).Friends.ToList();
            return users;
        }
    }
}
