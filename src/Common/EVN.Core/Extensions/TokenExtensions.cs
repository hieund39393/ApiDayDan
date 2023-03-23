using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using static EVN.Core.Common.AppConstants;

namespace EVN.Core.Extensions
{
    public static class TokenExtensions
    {
        private static IHttpContextAccessor _httpContextAccessor;
        public static void Configure(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// Get UserId
        /// </summary>
        /// <returns></returns>
        public static string GetUserId()
        {
            if (_httpContextAccessor == null) return Guid.Empty.ToString();
            var claims = _httpContextAccessor.HttpContext.GetClaims();
            var userid =  claims?.FirstOrDefault(c => c.Type.Contains("UserId"))?.Value;
            return userid;
        }

        /// <summary>
        /// Get UserId
        /// </summary>
        /// <returns></returns>
        public static string GetTokenSession()
        {
            if (_httpContextAccessor == null) return Guid.Empty.ToString();
            var claims = _httpContextAccessor.HttpContext.GetClaims();
            return claims?.FirstOrDefault(c => c.Type.Contains("jti"))?.Value;
        }

        /// <summary>
        /// Get UserId
        /// </summary>
        /// <returns></returns>
        public static string GetUnitId()
        {
            if (_httpContextAccessor == null) return Guid.Empty.ToString();
            var unitClaim = _httpContextAccessor.HttpContext.User.FindAll(JwtClaimTypes.UnitId);
            return unitClaim.First().Value;
        }

        public static bool CheckPermission(string[] permissions)
        {
            var userPermissionClaims = _httpContextAccessor.HttpContext.User.FindAll(Permissions.ClaimType);
            if (userPermissionClaims.Any())
            {
                var permissionClaims = userPermissionClaims.First().Value.Split(",");
                if (permissionClaims.Any(p => permissions.Contains(p)))
                {
                    return true;
                }
            }
            return false;
        }

        private static List<Claim> GetClaims(this HttpContext httpContext)
        {
            var token = httpContext.GetToken();
            if (string.IsNullOrEmpty(token))
            {
                return new List<Claim>();
            }
            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            try
            {
                return jwtSecurityTokenHandler.ReadJwtToken(token).Claims.ToList();
            }
            catch
            {
                return new List<Claim>();
            }
        }

        /// <summary>
        /// Get TimezoneOffset
        /// </summary>
        /// <returns></returns>
        public static int GetTimezoneOffset()
        {
            var claims = _httpContextAccessor.HttpContext.GetClaims();
            var value = claims?.FirstOrDefault(c => c.Type == Auth.TimezoneOffset)?.Value;
            return int.TryParse(value, out var result) ? result : 0;
        }

        private static string GetToken(this HttpContext httpContext)
        {
            if(httpContext == null)
            {
                return string.Empty;
            }

            string authHeader = httpContext.Request.Headers[Auth.Authorization];
            if (authHeader == null) return null;
            var bearerToken = authHeader.Substring(Auth.BearerHeader.Length)
                .Trim();
            return bearerToken;
        }

        public static string GetToken()
        {
            string authHeader = _httpContextAccessor.HttpContext.Request.Headers[Auth.Authorization];
            if (authHeader == null) return null;
            var bearerToken = authHeader.Substring(Auth.BearerHeader.Length)
                .Trim();

            return bearerToken;
        }
    }
}
