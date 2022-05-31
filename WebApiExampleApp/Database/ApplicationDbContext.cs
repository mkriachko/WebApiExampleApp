using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiExampleApp.Models;

namespace WebApiExampleApp.Database
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<NewsItem> News { get; set; }
        public DbSet<Session> Sessions { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            InitialDBContext();
        }
        private void InitialDBContext()
        {
            DbInitializer.Initialize(this);
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<User>().HasIndex(u => u.Login).IsUnique();
            builder.Entity<User>().HasMany(u => u.Friends).WithMany(u => u.FriendsOf)
                .UsingEntity<UserFriendSet>(
                f => f.HasOne<User>().WithMany().HasForeignKey(f => f.FriendId),
                f => f.HasOne<User>().WithMany().HasForeignKey(f => f.FriendOfId));
        }
    }
}
