using Authentication.Application.Model.Auth;
using Authentication.Infrastructure.AggregatesModel.UserAggregate;
using Authentication.Infrastructure.Properties;
using Authentication.Infrastructure.Repositories;
using EVN.Core.Common.JwtToken;
using EVN.Core.ConfigurationSettings;
using EVN.Core.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using static EVN.Core.Common.AppConstants;

namespace Authentication.Application.Commands.AuthCommand
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, LoginResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<User> _userManager;
        private readonly IJwtHandler _jwtHandler;
        private readonly AppSettings _option;
        public LoginCommandHandler(IUnitOfWork unitOfWork,
            UserManager<User> userManager,
            IJwtHandler jwtHandler,
            IOptions<AppSettings> option)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _jwtHandler = jwtHandler;
            _option = option.Value;
        }
        public async Task<LoginResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByNameAsync(request.UserName);
            if (user == null)
            {
                throw new EvnException(Resources.MSG_INVALID_ACCOUNT);
            }

            var isValidAccount = await _userManager.CheckPasswordAsync(user, request.Password);
            if (!isValidAccount)
            {
                await _userManager.AccessFailedAsync(user);
                throw new EvnException(Resources.MSG_INVALID_ACCOUNT);
            }
            await _userManager.ResetAccessFailedCountAsync(user);

            var tokenModel = new TokenModel
            {
                UserId = user.Id.ToString(),
                Permissions = user.GetPermissions(ClaimType.Permissions),
                IsSuperAdmin = user.IsSuperAdmin,
                Email = user.Email,
                UserName = user.UserName,
                Name = user.Name,
                PhoneNumber = user.PhoneNumber
            };
            var accessToken = _jwtHandler.CreateToken(tokenModel);
            var refreshToken = _jwtHandler.CreateRefreshToken();

            if (user.UserTokens == null)
                user.UserTokens = new List<UserToken>();
            user.UserTokens.Add(new UserToken()
            {
                CreatedDate = DateTime.Now,
                UserId = user.Id,
                LoginProvider = LoginProvider.Cms,
                Name = Auth.AccessToken,
                Value = accessToken
            });
            user.UserTokens.Add(new UserToken()
            {
                CreatedDate = DateTime.Now,
                UserId = user.Id,
                LoginProvider = LoginProvider.Cms,
                Name = Auth.RefreshToken,
                Value = refreshToken
            });
            await _userManager.UpdateAsync(user);
            var result = new LoginResponse(accessToken, _option.Jwt.TokenLifeTimeForWeb, refreshToken,
                user.Id, user.UserName, user.Name, user.PhoneNumber, user.Email);
            return result;
        }
    }
}
