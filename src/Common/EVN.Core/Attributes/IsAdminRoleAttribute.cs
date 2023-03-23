using EVN.Core.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace EVN.Core.Attributes
{
    public class IsAdminRoleAttribute : AuthorizeAttribute, IAsyncAuthorizationFilter
    {
        public IsAdminRoleAttribute()
        {
        }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            var rule = context.HttpContext.User?.FindFirstValue(ClaimTypes.Role);
            var isAdmin = string.Equals(rule, AppConstants.SuperAdminRole, StringComparison.InvariantCultureIgnoreCase);
            if (!isAdmin)
            {
                context.Result = new ForbidResult();
            }

            await Task.CompletedTask;
        }
    }
}
