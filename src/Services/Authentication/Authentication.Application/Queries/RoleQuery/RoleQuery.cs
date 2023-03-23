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

namespace Authentication.Application.Queries.RoleQuery
{
    public interface IRoleQuery
    {
        Task<PagingResultSP<RoleResponse>> GetListRole(RoleRequest request);
        Task<List<GetUserRoleResponse>> GetUserRole(Guid roleId);
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

    }
}
