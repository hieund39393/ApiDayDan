using EVN.Core.Models.Base;

namespace EVN.Core.Interfaces.Jwt
{
    public interface IJwtProvider
    {
        string GenerateJwtToken(BaseUser user, int donViId, bool isAdmin = false);

        bool ValidateAudience(string issuer
            , string secretKey
            , string audienceName
            , ref string errorMessage);

        string GenerateRefreshToken();
        string GetPropertyValue(string claimType);
    }
}
