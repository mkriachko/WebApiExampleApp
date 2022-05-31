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
            var users = await context.Users.Include(x => x.Friends).ToListAsync();
            return users;
        }

        public async Task<IEnumerable<User>> GetFriends(string login)
        {
            using ApplicationDbContext context = new(_options);
            var user = await context.Users.Include(x => x.Friends).FirstAsync(x => x.Login == login);
            var friends = user.Friends.ToList();
            return friends;
        }

        public async Task Delete(int userId)
        {
            using ApplicationDbContext context = new(_options);
            var user = context.Users.FirstOrDefault(n => n.Id == userId);
            if (user != null)
            {
                context.Users.Remove(user);
                await context.SaveChangesAsync();
            }
        }
    }
}
