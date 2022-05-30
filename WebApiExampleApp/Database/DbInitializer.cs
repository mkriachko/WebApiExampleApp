using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiExampleApp.Models;

namespace WebApiExampleApp.Database
{
    public static class DbInitializer
    {
        public static void Initialize(ApplicationDbContext context)
        {
            if (context.Database.EnsureCreated())
            {
                // Code to create initial data
                Seed(context);
            }
        }

        private static void Seed(ApplicationDbContext context)
        {
            var newsList = new List<NewsItem>()
            {
                new NewsItem() { Title = "First", Text = "Lorem ipsum", DateTime = DateTime.Now },
                new NewsItem() { Title = "Second", Text = "Gaudeamus igitur", DateTime = DateTime.Now },
                new NewsItem() { Title = "Third", Text = "С точки зрения банальной эрудиции", DateTime = DateTime.Now },
                new NewsItem() { Title = "Fourth", Text = "Как уважаемый профессор докторологии", DateTime = DateTime.Now },
                new NewsItem() { Title = "Fifth", Text = "Так выпьем няня где же кружка", DateTime = DateTime.Now },
            };
            context.News.AddRange(newsList);
            context.SaveChanges();
        }
    }
}
