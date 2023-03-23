using EVN.Core.ConfigurationSettings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using static EVN.Core.Common.AppConstants;

namespace EVN.Core.Common.JwtToken
{
    /// <summary>
    /// JwtHandler Interface
    /// </summary>
    public interface IJwtHandler
    {

        /// <summary>
        /// Create Token
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        string CreateToken(TokenModel model);

        /// <summary>
        /// Create Refresh Token
        /// </summary>
        /// <returns></returns>
        string CreateRefreshToken();

        /// <summary>
        /// Lấy thông tin Claim
        /// </summary>
        /// <param name="claimType"></param>
        /// <returns></returns>
        string GetClaim(string claimType);

        /// <summary>
        /// Get token from request
        /// </summary>
        /// <returns></returns>
        string GetTokenFromHeader();

        /// <summary>
        /// Get UserId from request
        /// </summary>
        /// <returns></returns>
        Guid? GetUserId();

        /// <summary>
        /// Get IsAdmin from request
        /// </summary>
        /// <returns></returns>
        bool IsSuperAdmin();

        /// <summary>
        /// Email
        /// </summary>
        /// <returns></returns>
        string GetEmail();

        /// <summary>
        /// PhoneNumber
        /// </summary>
        /// <returns></returns>
        string GetPhoneNumber();

        /// <summary>
        /// tUserName
        /// </summary>
        /// <returns></returns>
        string GetUserName();

        /// <summary>
        /// FullName
        /// </summary>
        /// <returns></returns>
        string GetFullName();

    }

    /// <summary>
    /// JwtHandler Implement
    /// </summary>
    public class JwtHandler : IJwtHandler
    {
        private readonly AppSettings _options;
        private readonly JwtSecurityTokenHandler _jwtSecurityTokenHandler;
        private readonly IHttpContextAccessor _httpContextAccessor;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="options"></param>
        /// <param name="httpContextAccessor"></param>
        public JwtHandler(IOptions<AppSettings> options, IHttpContextAccessor httpContextAccessor)
        {
            _options = options.Value;
            _jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            _httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// Create new Jwt token
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public string CreateToken(TokenModel model)
        {
            var expiredTime = DateTime.UtcNow.AddMinutes(_options.Jwt.TokenLifeTimeForWeb);
            var claims = new[]
            {
                new Claim(ClaimType.UserId, model.UserId ?? string.Empty),
                new Claim(ClaimType.Permissions, model.Permissions ?? string.Empty),
                new Claim(ClaimType.IsSuperAdmin, model.IsSuperAdmin.ToString()),
                new Claim(ClaimType.PhoneNumber, model.PhoneNumber ?? string.Empty),
                new Claim(ClaimType.Email, model.Email ?? string.Empty),
                new Claim(ClaimType.UserName, model.UserName ?? string.Empty),
                new Claim(ClaimType.Name, model.Name ?? string.Empty)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.Jwt.SecretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var jwt = new JwtSecurityToken(_options.Jwt.Issuer,
                _options.Jwt.Audience,
                claims,
                null,
                expiredTime,
                creds);

            return _jwtSecurityTokenHandler.WriteToken(jwt);
        }


        /// <summary>
        /// Refresh Token
        /// </summary>
        /// <returns></returns>
        public string CreateRefreshToken()
        {
            var expiredTime = DateTime.UtcNow.AddMinutes(_options.Jwt.RefreshTokenLifeTimeWeb);
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.Jwt.SecretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var jwt = new JwtSecurityToken(_options.Jwt.Issuer,
                _options.Jwt.Audience,
                null,
                null,
                expiredTime,
                creds);

            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }

        /// <summary>
        ///  Create new Jwt token server
        /// </summary>
        /// <param name="_options"></param>
        /// <returns></returns>
        public static string CreateTokenInternal(JwtConfig _options)
        {
            var expiredTime = DateTime.UtcNow.AddMinutes(_options.TokenLifeTimeForWeb);
            var claims = new[]
            {
                new Claim(ClaimType.UserId, "00000000-0000-0000-0000-000000000000"),
                new Claim(ClaimType.Permissions, ""),
                new Claim(ClaimType.IsSuperAdmin, true.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SecretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var jwt = new JwtSecurityToken(_options.Issuer,
                _options.Audience,
                claims,
                null,
                expiredTime,
                creds);

            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }

        public static bool IsValidToken(string token, JwtConfig _options)
        {
            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();

            try
            {
                jwtSecurityTokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SecretKey)),

                    ValidateIssuer = false,
                    ValidIssuer = _options.Issuer,

                    ValidateAudience = false,
                    ValidAudience = _options.Audience,

                    ValidateLifetime = false,
                    ClockSkew = TimeSpan.Zero
                }, out var _);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static string GetClaim(string token, string claimType)
        {
            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var claims = jwtSecurityTokenHandler.ReadJwtToken(token).Claims.ToList();
            return claims.FirstOrDefault(c => c.Type == claimType)?.Value;
        }

        /// <summary>
        /// Get claim from token
        /// </summary>
        /// <param name="claimType"></param>
        /// <returns></returns>
        public string GetClaim(string claimType)
        {
            var userPermissionClaims = _httpContextAccessor.HttpContext?.User?.FindFirst(claimType);
            return userPermissionClaims?.Value;
        }

        /// <summary>
        /// Get token from request
        /// </summary>
        /// <returns></returns>
        public string GetTokenFromHeader()
        {
            var headers = _httpContextAccessor.HttpContext?.Request?.Headers;
            return headers?["Authorization"].ToString().Substring(JwtBearerDefaults.AuthenticationScheme.Length).Trim();
        }

        /// <summary>
        /// Get UserId from request
        /// </summary>
        /// <returns></returns>
        public Guid? GetUserId()
        {
            var userIdStr = GetClaim(ClaimType.UserId);
            if (string.IsNullOrEmpty(userIdStr) || !Guid.TryParse(userIdStr, out var userId))
            {
                return null;
            }

            return userId;
        }

        /// <summary>
        /// Get IsAdmin from request
        /// </summary>
        /// <returns></returns>
        public bool IsSuperAdmin()
        {
            var isAdmin = GetClaim(ClaimType.IsSuperAdmin);
            return (isAdmin?.ToLower() == "true");
        }

        public string GetEmail()
        {
            return GetClaim(ClaimType.Email);
        }

        public string GetPhoneNumber()
        {
            return GetClaim(ClaimType.PhoneNumber);
        }

        public string GetUserName()
        {
            return GetClaim(ClaimType.UserName);
        }

        public string GetFullName()
        {
            return GetClaim(ClaimType.Name);
        }
    }
}
