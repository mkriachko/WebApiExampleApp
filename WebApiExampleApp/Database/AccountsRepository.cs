using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiExampleApp.Models;
using WebApiExampleApp.Resources;

namespace WebApiExampleApp.Database
{
    public class AccountsRepository
    {
        private readonly DbContextOptions<ApplicationDbContext> _options;
        public AccountsRepository(DbContextOptions<ApplicationDbContext> options)
        {
            _options = options;
        }

        public async Task<User> Register(NewAccount newAccount)
        {
            var newUser = new User()
            {
                Login = newAccount.Login,
                Email = newAccount.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(newAccount.Password),
                Role = Roles.User
            };
            using ApplicationDbContext context = new(_options);
            var user = (await context.Users.AddAsync(newUser))?.Entity;
            await context.SaveChangesAsync();
            return user;
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

        public async Task<User> GetUser(string username)
        {
            using ApplicationDbContext context = new(_options);
            var user = await context.Users.FirstOrDefaultAsync(u => u.Login == username);
            if (user != null)
                return user;
            return null;
        }

        public async Task<IEnumerable<Session>> GetSessions()
        {
            using ApplicationDbContext context = new (_options);
            var sessions = await context.Sessions.ToListAsync();
            return sessions;
        }

        public async Task<Session> AddSession(Session newSession)
        {
            using ApplicationDbContext context = new(_options);
            var session = (await context.Sessions.AddAsync(newSession))?.Entity;
            await context.SaveChangesAsync();
            return session;
        }

        public async Task<Session> UpdateSession(string sessionId, Session newSession)
        {
            using ApplicationDbContext context = new(_options);
            var session = await context.Sessions.FirstOrDefaultAsync(s => s.RefreshToken == sessionId);
            if (session != null)
            {
                session.RefreshToken = newSession.RefreshToken;
                session.Login = newSession.Login;
                session.LoggedOn = newSession.LoggedOn;
                await context.SaveChangesAsync();
                return session;
            }
            return null;
        }
    }
}
