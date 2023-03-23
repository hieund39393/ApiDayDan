using EVN.Core.Implements.Http;
using EVN.Core.Interfaces.Jwt;
using EVN.Core.Models.Base;
using EVN.Core.Models.Settings;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using static EVN.Core.Common.Constants;

namespace EVN.Core.Implements.Jwt
{
    public class JwtProvider : IJwtProvider
    {
        private readonly IConfiguration _configuration;
        private readonly IJwtGenerator _jwtGenerator;

        public JwtProvider(IConfiguration configuration
            , IJwtGenerator jwtGenerator
            )
        {
            _configuration = configuration;
            _jwtGenerator = jwtGenerator;
        }

        public string GenerateJwtToken(BaseUser user, int donViId, bool isAdmin = false)
        {
            var tokenSettings = _configuration.GetSection(CONFIG_KEYS.JWT_TOKEN).Get<JwtTokenSetting>();
            var claims = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Actor, user.HoTen),        
                new Claim("IsAdmin", isAdmin.ToString()),
                new Claim("DonViId", donViId.ToString())
            });

            return _jwtGenerator.Generate(tokenSettings, claims);
        }

        public string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }

        public string GetPropertyValue(string claimType)
        {
            var claims = HttpAppContext.Current.User.Identity as ClaimsIdentity;
            var response = claims.Claims.FirstOrDefault(c => c.Type == claimType)?.Value;

            return response;
        }

        public bool ValidateAudience(string issuer
            , string secretKey
            , string audienceName
            , ref string errorMessage)
        {
            var tokenSettings = _configuration.GetSection(CONFIG_KEYS.JWT_TOKEN).Get<JwtTokenSetting>();

            if (tokenSettings.Issuer != issuer)
            {
                errorMessage = string.Format("You do not login permitted", nameof(tokenSettings.Issuer));
                return false;
            }

            if (tokenSettings.SecretKey != secretKey)
            {
                errorMessage = string.Format("You do not login permitted", nameof(tokenSettings.SecretKey));
                return false;
            }

            return true;
        }
    }
}
