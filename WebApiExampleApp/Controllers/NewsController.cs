using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiExampleApp.Database;
using WebApiExampleApp.Filters;
using WebApiExampleApp.Models;
using WebApiExampleApp.Resources;

namespace WebApiExampleApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NewsController : ControllerBase
    {
        private readonly ILogger<NewsController> _logger;
        private readonly NewsRepository _repo;

        public NewsController(ILogger<NewsController> logger, NewsRepository repo)
        {
            _logger = logger;
            _repo = repo;
        }

        [CustomAuthorizeFilter]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<NewsItem>>> GetNews(int offset = 0, int count = 10)
        {
            return Ok(await _repo.Get(offset, count));
        }

        [CustomAuthorizeFilter(new string[] { Roles.Admin })]
        [HttpPost]
        public async Task<ActionResult> AddNewsItem([FromBody] NewsItem newsItem)
        {
            var result = await _repo.Add(newsItem);
            return Created($"api/{RouteData.Values["controller"]}/{result.Id}", result);
        }

        [CustomAuthorizeFilter(new string[] { Roles.Admin })]
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteNewsItem(int id)
        {
            await _repo.Delete(id);
            return Ok();
        }
    }
}
