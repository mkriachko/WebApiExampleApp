using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using WebApiExampleApp.Database;
using WebApiExampleApp.Models;
using WebApiExampleApp.Resources;

namespace WebApiExampleApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly ILogger<AccountController> _logger;
        private readonly AccountsRepository _repo;

        public AccountController(ILogger<AccountController> logger, AccountsRepository repo)
        {
            _logger = logger;
            _repo = repo;
        }

        [Authorize]
        [HttpGet]
        public IActionResult GetInfo()
        {
            return Ok(new { Logon = User.Identity.Name, Claims = User.Claims.Select(x => new { x.Type, x.Value }) });
        }

        [HttpPost("register")]
        public IActionResult CreateAccount([FromBody] NewAccount account)
        {
            throw new NotImplementedException();
        }

        [HttpPost("logon")]
        public async Task<ActionResult<Token>> Logon([FromBody] Account account)
        {
            var user = await _repo.Logon(account);
            var encodedJwt = GenerateAccessToken(user);
            var refreshToken = GenerateRefreshToken();
            var response = new Token { AccessToken = encodedJwt, RefreshToken = refreshToken };
            return Ok(response);
        }

        [HttpPost("session")]
        public IActionResult RenewSession([FromBody] Session session)
        {
            throw new NotImplementedException();
        }

        private string GetUserNameFromExpiredToken(string token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = AuthOptions.ISSUER,
                ValidateAudience = true,
                ValidAudience = AuthOptions.AUDIENCE,
                ValidateLifetime = false,
                IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
                ValidateIssuerSigningKey = true,
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);
            if (!(securityToken is JwtSecurityToken jwtSecurityToken) || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Invalid token");

            return principal.Identity.Name;
        }

        private string GenerateAccessToken(User user)
        {
            var identity = GetClaimsIdentity(user);
            var jwt = new JwtSecurityToken(issuer: AuthOptions.ISSUER,
                audience: AuthOptions.AUDIENCE,
                notBefore: DateTime.Now,
                claims: identity.Claims,
                expires: DateTime.Now.Add(AuthOptions.TokenLifeTime),
                signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
            return encodedJwt;
        }

        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[64];
            using var rng = RandomNumberGenerator.Create();
            using MD5 md5Hash = MD5.Create();
            rng.GetBytes(randomNumber);
            var data = md5Hash.ComputeHash(randomNumber);
            var sb = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
                sb.Append(data[i].ToString("x2"));
            return sb.ToString();
        }

        private ClaimsIdentity GetClaimsIdentity(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Login),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role)
            };
            return new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType);
        }
    }
}
