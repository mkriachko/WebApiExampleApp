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
    public class UsersController : ControllerBase
    {
        private readonly ILogger<UsersController> _logger;
        private readonly UsersRepository _repo;

        public UsersController(ILogger<UsersController> logger, UsersRepository repo)
        {
            _logger = logger;
            _repo = repo;
        }

        [CustomAuthorizeFilter(new string[] { Roles.Admin })]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return Ok( await _repo.Get());
        }

        [CustomAuthorizeFilter]
        [HttpGet("friends")]
        public async Task<ActionResult<IEnumerable<User>>> GetFriends()
        {
            var login = User.Identity.Name;
            var friends = await _repo.GetFriends(login);
            var result = friends.Select(f => new { Login = f.Login });
            return Ok(result);
        }

        [CustomAuthorizeFilter(new string[] { Roles.Admin })]
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUser(int id)
        {
            await _repo.Delete(id);
            return Ok();
        }
    }
}
