using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiExampleApp.Models;

namespace WebApiExampleApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly ILogger<AccountController> _logger;

        public AccountController(ILogger<AccountController> logger)
        {
            _logger = logger;
        }

        [HttpPost("register")]
        public IActionResult CreateAccount([FromBody] NewAccount account)
        {
            throw new NotImplementedException();
        }

        [HttpPost("logon")]
        public IActionResult Logon([FromBody] Account account)
        {
            throw new NotImplementedException();
        }

        [HttpPost("session")]
        public IActionResult RenewSession([FromBody] Session session)
        {
            throw new NotImplementedException();
        }
    }
}
