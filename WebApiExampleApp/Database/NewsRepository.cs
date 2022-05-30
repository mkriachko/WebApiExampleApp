using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiExampleApp.Models;

namespace WebApiExampleApp.Database
{
    public class NewsRepository
    {
        private DbContextOptions<ApplicationDbContext> _options;
        public NewsRepository(DbContextOptions<ApplicationDbContext> options)
        {
            _options = options;
        }

        public async Task<IEnumerable<NewsItem>> Get(int offset = 0, int count = 10)
        {
            using (ApplicationDbContext context = new ApplicationDbContext(_options))
            {
                var news = await context.News.Skip(offset).Take(count).ToListAsync();
                return news;
            }
        }
    }
}
