using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace WebApiExampleApp.Filters
{
    public class CustomAuthorizeFilterAttribute : AuthorizeAttribute, IAuthorizationFilter
    {
        private readonly IEnumerable<string> _requiredRoles;

        public CustomAuthorizeFilterAttribute()
        {

        }

        public CustomAuthorizeFilterAttribute(string[] requiredRoles)
        {
            _requiredRoles = requiredRoles;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if(_requiredRoles != null && !context.HttpContext.User.Claims.Any(c => c.Type == ClaimsIdentity.DefaultRoleClaimType && _requiredRoles.Contains(c.Value)))
            {
                context.Result = new ForbidResult();
            }
        }
    }
}
