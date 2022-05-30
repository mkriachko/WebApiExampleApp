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
        private readonly DbContextOptions<ApplicationDbContext> _options;
        public NewsRepository(DbContextOptions<ApplicationDbContext> options)
        {
            _options = options;
        }

        public async Task <IEnumerable<NewsItem>> Get(int offset = 0, int count = 10)
        {
            using ApplicationDbContext context = new(_options);
            var news = await context.News.Skip(offset).Take(count).ToListAsync();
            return news;
        }

        public async Task<NewsItem> Add(NewsItem newNewsItem)
        {
            using ApplicationDbContext context = new(_options);
            var newsItem = (await context.News.AddAsync(newNewsItem))?.Entity;
            await context.SaveChangesAsync();
            return newsItem;
        }

        public async Task Delete(int newsItemId)
        {
            using ApplicationDbContext context = new(_options);
            var newsItem = context.News.FirstOrDefault(n => n.Id == newsItemId);
            if (newsItem != null)
            {
                context.News.Remove(newsItem);
                await context.SaveChangesAsync();
            }
        }
    }
}
