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
            builder.Entity<Session>().HasKey(s => s.RefreshToken);
            builder.Entity<User>().HasIndex(u => u.Login).IsUnique();
        }
    }
}
