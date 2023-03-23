using Authentication.Application.Commands.UserCommand;
using Authentication.Application.Model.User;
using Authentication.Infrastructure.EF;
using Authentication.Infrastructure.Properties;
using Authentication.Infrastructure.Repositories;
using EVN.Core.Exceptions;
using EVN.Core.Extensions;
using EVN.Core.SeedWork;
using Authentication.Infrastructure.AggregatesModel.UserAggregate;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Authentication.Application.Queries.UserQuery
{

    public interface IUserQuery
    {
        Task<PagingResultSP<UserResponse>> GetListUser(UserRequest request);
        Task<UserProfile> GetProfile();

        /// <summary>
        /// Phân quyền chức năng lấy danh sách các quyền theo từng module
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<List<GetModuleRoleResponse>> GetModuleRole(GetUpModuleRoleRequest request);

        Task<bool> ForgotPassword(ForgotPassword model);
    }
    public class UserQuery : IUserQuery
    {
        private readonly ExOneDbContext _context;
        private readonly UserManager<User> _userManager;
        private readonly IUnitOfWork _unitOfWork;

        public UserQuery(ExOneDbContext context, IUnitOfWork unitOfWork, UserManager<User> userManager)
        {
            _context = context;
            _unitOfWork = unitOfWork;
            _userManager = userManager;

        }
        public async Task<PagingResultSP<UserResponse>> GetListUser(UserRequest request)
        {
            var unit = await _unitOfWork.UnitRepository.FindOneAsync(x => x.Id == request.UnitId);
            var query = _unitOfWork.UserRepository.GetQuery(x => (x.Unit == unit || unit == null)
            )
                .AsNoTracking()
                .Include(x => x.Unit)
                .Include(x => x.Team)
                .Include(x => x.UserRoles).ThenInclude(x => x.Role)
                .Select(x => new UserResponse()
                {
                    Id = x.Id,
                    UnitId = x.Unit.Id,
                    Name = x.Name,
                    UserName = x.UserName,
                    Actived = x.Actived,
                    CreatedDate = x.CreatedDate,
                    CMIS_CODE = x.CMIS_CODE,
                    RoleName = x.UserRoles.Select(ur => ur.Role.Name).ToList()
                });
            if (!string.IsNullOrEmpty(request.SearchTerm))
            {
                query = query.Where(x => x.UserName.Contains(request.SearchTerm) || x.CMIS_CODE.Contains(request.SearchTerm) || x.Name.Contains(request.SearchTerm));
            }
            var totalRow = query.Count();
            var queryPaging = query.Skip((request.PageIndex - 1) * request.PageSize).Take(request.PageSize);
            return await PagingResultSP<UserResponse>.CreateAsyncLinq(queryPaging, totalRow, request.PageIndex, request.PageSize);
        }

        public async Task<UserProfile> GetProfile()
        {
            var id = Guid.Parse(TokenExtensions.GetUserId());
            var user = _unitOfWork.UserRepository.GetQuery(x => x.Id.Equals(id))
                .Include(p => p.UserRoles).ThenInclude(p => p.Role)
                .Include(p => p.Unit).FirstOrDefault();
            if (user == null)
            {
                throw new EvnException(Resources.MSG_NOT_FOUND, "Người dùng");
            }
            UserProfile result = new UserProfile()
            {
                Id = user.Id,
                Name = user.Name,
                PhoneNumber = user.PhoneNumber,
                Username = user.UserName,
                Avatar = user.Avatar,
                RoleNames = user.UserRoles.Select(x => x.Role.Name).ToList(),
                IsAdministrator = user.IsSuperAdmin,
                IsActived = user.Actived,
                Position = user.Position
            };
            return result;
        }


        /// <summary>
        /// Lấy danh sách quyền theo từng Module
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<List<GetModuleRoleResponse>> GetModuleRole(GetUpModuleRoleRequest request)
        {
            var module = await _unitOfWork.ModuleRepository.FindOneAsync(x => x.Id == request.ModuleId);
            var data = _unitOfWork.RoleRepository.GetQuery(x => x.Module == module || module == null).AsNoTracking()
                .Include(p => p.Module)
                .Select(x => new GetModuleRoleResponse()
                {
                    RoleId = x.Id,
                    RoleName = x.Name,
                    Description = x.Description,
                    ModuleName = x.Module.ModuleName,
                }).ToList();
            return data;
        }

        public async Task<bool> ForgotPassword(ForgotPassword request)
        {
            var data = (_unitOfWork.UserRepository.GetQuery(x => x.UserName == request.UserName)).FirstOrDefault();
            if (data == null)
            {
                throw new EvnException(string.Format(Resources.MSG_NOT_FOUND, "Người dùng"));
            }
            await _userManager.RemovePasswordAsync(data);
            var createResult = await _userManager.AddPasswordAsync(data, "EVNHaNoi@12345");
            if (createResult.Succeeded)
            {
                await _unitOfWork.SaveChangesAsync();
            }
            // 
            return true;
        }

    }
}
