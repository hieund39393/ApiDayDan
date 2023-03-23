using EVN.Core.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace EVN.Core.Attributes
{
    public class HasPermissionAttribute : AuthorizeAttribute, IAsyncAuthorizationFilter
    {
        private readonly string[] _permissions;

        /// <summary>
        /// Has permission constructor
        /// </summary>
        /// <param name="permissions"></param>
        public HasPermissionAttribute(params string[] permissions)
        {
            _permissions = permissions;
        }

        /// <summary>
        /// On authorization
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            var userPermissionClaims = context.HttpContext.User.FindAll(AppConstants.Permissions.ClaimType);
            if (userPermissionClaims.Any())
            {
                var permissionClaims = userPermissionClaims.First().Value.Split(",");
                if (!permissionClaims.Any(p => _permissions.Contains(p)))
                {
                    context.Result = new ForbidResult();
                }
            }

            await Task.CompletedTask;
        }
    }
}
