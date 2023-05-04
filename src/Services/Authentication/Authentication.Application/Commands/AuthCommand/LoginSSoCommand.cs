using Authentication.Application.Model;
using Authentication.Application.Model.Auth;
using Authentication.Application.Services;
using Authentication.Infrastructure.AggregatesModel.UserAggregate;
using Authentication.Infrastructure.Properties;
using Authentication.Infrastructure.Repositories;
using EVN.Core.Common;
using EVN.Core.Common.JwtToken;
using EVN.Core.ConfigurationSettings;
using EVN.Core.Exceptions;
using EVN.Core.Infrastructure.Factory;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using static EVN.Core.Common.AppConstants;

namespace Authentication.Application.Commands.AuthCommand
{
    public class LoginSSoCommand : IRequest<LoginResponse>
    {
        /// <summary>
        /// Tài khoản
        /// </summary>
        public string Ticket { get; set; }

        /// <summary>
        /// Mật khẩu
        /// </summary>
        public string AppCode { get; set; }
    }
    public class LoginSSoCommandHandler : IRequestHandler<LoginSSoCommand, LoginResponse>
    {
        private readonly IExOneHttpClientFactory _httpClientFactory;
        private readonly UserManager<User> _userManager;
        private readonly IJwtHandler _jwtHandler;
        private readonly AppSettings _option;
        private readonly IUnitOfWork _unitOfWork;

        public LoginSSoCommandHandler(IExOneHttpClientFactory httpClientFactory, UserManager<User> userManager, IJwtHandler jwtHandler, AppSettings option, IUnitOfWork unitOfWork)
        {
            _httpClientFactory = httpClientFactory;
            _userManager = userManager;
            _jwtHandler = jwtHandler;
            _option = option;
            _unitOfWork = unitOfWork;
        }

        public async Task<LoginResponse> Handle(LoginSSoCommand request, CancellationToken cancellationToken)
        {
            var httpClient = new BaseResponseService<ApiResultLoginSSO>(_httpClientFactory);
            var respon = await httpClient.GetResponseSSO($"http://10.9.171.42:3020/sso/serviceValidate?ticket={request.Ticket}&appCode={request.AppCode}");
            if (respon.Code != "API-000")
            {
                throw new EvnException(respon.Message);
            }

            var user = await _userManager.Users.Include(x => x.UserRoles).ThenInclude(x => x.Role).ThenInclude(x => x.RoleClaims)
               .FirstOrDefaultAsync(x => x.UserName == respon.Data.Identity.UserName && !x.IsDeleted, cancellationToken);


            if (user == null)
            {
                user = new User();
                user.Actived = true;
                user.UserName = respon?.Data?.Identity?.UserName;
                user.Name = respon?.Data?.Identity?.FullName;
                user.Email = respon?.Data?.Identity?.Email;
                user.PhoneNumber = respon?.Data?.Identity?.Phone;

                var createResult = await _userManager.CreateAsync(user, AppConstants.DefaulPass);
                await _unitOfWork.SaveChangesAsync();
            }
            else
            {
                user.Actived = true;
                user.UserName = respon.Data.Identity.UserName;
                user.Name = respon.Data.Identity.FullName;
                user.Email = respon.Data.Identity.Email;
                user.PhoneNumber = respon.Data.Identity.Phone;
                _unitOfWork.UserRepository.Update(user);
            }

            var permissions = user.UserRoles.SelectMany(x => x.Role.RoleClaims.Select(y => y.ClaimValue)).ToList();
            var tokenModel = new TokenModel()
            {
                UserId = user.Id.ToString(),
                Permissions = string.Join(",", permissions),
                IsSuperAdmin = user.IsSuperAdmin,
                Email = user.Email,
                UserName = user.UserName,
                Name = user.Name,
                PhoneNumber = user.PhoneNumber,
            };

            var accessToken = _jwtHandler.CreateToken(tokenModel);
            var refreshToken = _jwtHandler.CreateRefreshToken();


            if (user.UserTokens == null)
                user.UserTokens = new List<UserToken>();
            user.UserTokens.Add(new UserToken()
            {
                CreatedDate = DateTime.Now,
                UserId = user.Id,
                LoginProvider = LoginProvider.SSO,
                Name = Auth.RefreshToken,
                Value = refreshToken
            });
            await _userManager.UpdateAsync(user);
            await _unitOfWork.SaveChangesAsync();

            var result = new LoginResponse(accessToken, _option.Jwt.TokenLifeTimeForWeb, refreshToken,
                user.Id, user.UserName, user.Name, user.PhoneNumber, user.Email, tokenModel.Permissions, tokenModel.IsSuperAdmin);
            return result;


        }
    }
}
