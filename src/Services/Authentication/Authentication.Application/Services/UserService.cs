using Authentication.Infrastructure.AggregatesModel.UserAggregate;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Authentication.Infrastructure.Repositories;
using System.Collections.Generic;
using EVN.Core.ConfigurationSettings;
using EVN.Core.Common;
using static EVN.Core.Common.AppConstants;
using Microsoft.AspNetCore.Mvc;
using Authentication.Application.Queries;
using EVN.Core.Exceptions;
using Authentication.Infrastructure.Properties;

namespace Authentication.Application.Services
{
    public interface IUserService
    {
        /// <summary>
        /// Tạo token
        /// </summary>
        /// <param name="user"></param>
        /// <param name="tokenSession"></param>
        /// <param name="expires"></param>
        /// <param name="timezoneOffset"></param>
        /// <returns></returns>
        string GenerateJwtToken(User user, string tokenSession, DateTime expires, int timezoneOffset);

        /// <summary>
        /// Tạo chuỗi refresh token
        /// </summary>
        /// <returns></returns>
        string GenerateRefreshToken();
        Task<bool> CheckUserName(Guid id, string username);
        Task<string> GetName(Guid? id);
        Task<List<User>> GetUsersAsync(List<Guid> users);
        Task<bool> CheckUserPermission(Guid userId, string[] permissions);
        Task<User> GetUser(Guid id);
    }

    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly AppSettings _appSettings;
        private readonly IUnitOfWork _unitOfWork;

        public UserService(UserManager<User> userManager,
            IOptions<AppSettings> appSettings,
            IUnitOfWork unitOfWork
            )
        {
            _userManager = userManager;
            _appSettings = appSettings.Value;
            _unitOfWork = unitOfWork;
        }

        public string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }

        public string GenerateJwtToken(User user, string tokenSession, DateTime expires, int timezoneOffset)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Jwt.SecretKey);
            var permissions = user.UserRoles.SelectMany(x => x.Role.RoleClaims.Select(d => d.ClaimValue)).Distinct().ToList();

            var subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Actor, user.Name),
                new Claim(ClaimTypes.Role, user.IsSuperAdmin ? AppConstants.SuperAdminRole : AppConstants.UserRole),
                new Claim(Auth.TimezoneOffset, timezoneOffset.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, tokenSession),
                new Claim(Permissions.ClaimType, string.Join(",",permissions))
            });

            var tokenDescription = new SecurityTokenDescriptor
            {
                Subject = subject,
                Expires = expires,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature),
                Issuer = _appSettings.Jwt.Issuer,
                Audience = _appSettings.Jwt.Issuer,
            };

            var token = tokenHandler.CreateToken(tokenDescription);
            return tokenHandler.WriteToken(token);
        }


        public async Task<bool> CheckUserName(Guid id, string username)
        {
            return await _userManager.Users
                .AnyAsync(x => x.UserName == username && x.Id != id);
        }

        public async Task<string> GetName(Guid? id)
        {
            var user = await _unitOfWork.UserRepository.FindOneAsync(x => x.Id == id);
            return user?.Name;
        }

        public async Task<User> GetUser(Guid id)
        {
            return await _unitOfWork.UserRepository.FindOneAsync(x => x.Id == id);
        }

        public async Task<List<User>> GetUsersAsync(List<Guid> users)
        {
            return await _unitOfWork.UserRepository.GetQuery(x => users.Contains(x.Id)).ToListAsync();
        }


        public async Task<bool> CheckUserPermission(Guid userId, string[] permissions)
        {
            return await _unitOfWork.UserRepository
               .GetQuery(x => x.Id == userId && x.UserRoles.Any(d => d.Role.RoleClaims.Any(cl => permissions.Contains(cl.ClaimValue))))
               .Include(x => x.UserRoles).ThenInclude(x => x.Role).ThenInclude(x => x.RoleClaims).AnyAsync();
        }

    }
}
