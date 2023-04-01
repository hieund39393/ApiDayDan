using Authentication.Application.Model.Role;
using Authentication.Infrastructure.Repositories;
using EVN.Core.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Authentication.Infrastructure.AggregatesModel.UserAggregate;
using Authentication.Infrastructure.AggregatesModel.MenuAggregate;

namespace Authentication.Application.Queries.RoleQuery
{
    public interface IRoleQuery
    {
        Task<PagingResultSP<RoleResponse>> GetListRole(RoleRequest request);
        Task<List<GetUserRoleResponse>> GetUserRole(Guid roleId);
        Task<List<PermissionResponse>> GetPermission();
    }
    public class RoleQuery : IRoleQuery
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public RoleQuery(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<PagingResultSP<RoleResponse>> GetListRole(RoleRequest request)
        {
            var query = _unitOfWork.RoleRepository.GetQuery(x => (string.IsNullOrEmpty(request.SearchTerm)
            || x.Name.Contains(request.SearchTerm) || x.Description.Contains(request.SearchTerm)))
            .Select(x => new RoleResponse()
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                Permissions = x.RoleClaims.Select(x => x.ClaimValue).ToList(),
                CreatedDate = x.CreatedDate,
            });
            var totalRow = query.Count();
            var queryPaging = query.Skip((request.PageIndex - 1) * request.PageSize).Take(request.PageSize);
            return await PagingResultSP<RoleResponse>.CreateAsyncLinq(queryPaging, totalRow, request.PageIndex, request.PageSize);
        }

        public async Task<List<GetUserRoleResponse>> GetUserRole(Guid roleId)
        {
            var data = _unitOfWork.RoleRepository.GetQuery(x => x.Id == roleId).AsNoTracking()
                        .Include(x => x.UserRoles).ThenInclude(p => p.User).FirstOrDefault();
            var users = data?.UserRoles.Select(x => x.User).ToList();
            var result = _mapper.Map<List<User>, List<GetUserRoleResponse>>(users);
            return result;

        }


        public async Task<List<PermissionResponse>> GetPermission()
        {
            var listMenu = await _unitOfWork.ModuleRepository.GetQuery().AsNoTracking().Include(x => x.Menus).ThenInclude(x => x.Permissions)
                .Select(x => new PermissionResponse
                {
                    Title = x.Name,
                    Key = x.Code,
                    Children = x.Menus.Select(m => new PermissionResponse
                    {
                        Title = m.Name,
                        Key = m.Code,
                        Children = m.Permissions.Select(k => new PermissionResponse
                        {
                            Title = k.Name,
                            Key = k.Code,

                        }).ToList(),
                    }).ToList(),
                })
                .ToListAsync();


            return listMenu;

        }

        //private List<PermissionResponse> MenuChild(int parentId, List<Menu> listMenu)
        //{
        //    var data = listMenu.Where(x => x.ParentId == parentId).Select(x => new PermissionResponse
        //    {
        //        Title = x.Name,
        //        Key = x.Code,
        //        Children = GetActionsByMenu(x.Code)
        //    }).ToList();
        //    return data;
        //}

        //private List<PermissionResponse> GetActionsByMenu(string menuCode)
        //{
        //    var das = _unitOfWork.PermissionRepository.GetQuery().ToList();
        //    var data = _unitOfWork.PermissionRepository.GetQuery(x => x.MenuCode == menuCode).AsNoTracking().Select(x => new PermissionResponse
        //    {
        //        Title = x.Name,
        //        Key = x.Code,
        //    }).ToList();
        //    return data;
        //}
    }
}
