using Authentication.Infrastructure.AggregatesModel.UserAggregate;
using Authentication.Infrastructure.Repositories;
using AutoMapper;
using EVN.Core.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using static EVN.Core.Common.AppConstants;
using Authentication.Infrastructure.Properties;

namespace Authentication.Application.Commands.RoleCommand
{
    public class RoleCreateOrUpdate : IRequest<bool>
    {
        public Guid Id { get; set; }
        public Guid ModuleId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<string> Permissions { get; set; }
        public UserPosition Type { get; set; }

    }
    public class RoleCreateOrUpdateHandler : IRequestHandler<RoleCreateOrUpdate, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly RoleManager<Role> _roleManager;
        public RoleCreateOrUpdateHandler(IUnitOfWork unitOfWork, IMapper mapper, RoleManager<Role> roleManager)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _roleManager = roleManager;
        }
        public async Task<bool> Handle(RoleCreateOrUpdate request, CancellationToken cancellationToken)
        {
            var module = _unitOfWork.ModuleRepository.GetQuery(x => x.Id == request.ModuleId).FirstOrDefault();
            if (request.Id == Guid.Empty)
            {
                var data = await _unitOfWork.RoleRepository.FindOneAsync(x => x.Module == module && x.Name.ToLower().Equals(request.Name.ToLower()));
                if (data != null)
                {
                    throw new EvnException(String.Format(Resources.MSG_IS_EXIST, "Vai trò"));
                }
                var role = new Role
                {
                    Name = request.Name,
                    Module = module,
                    Description = request.Description,
                    NormalizedName = request.Name,
                    Type = request.Type,
                    CreatedDate = DateTime.Now
                };
                var createResult = await _roleManager.CreateAsync(role);
                if (createResult.Succeeded)
                {
                    foreach (var permission in request.Permissions)
                    {
                        await _roleManager.AddClaimAsync(role, new Claim(Permissions.ClaimType, permission));
                    }
                }
                else
                {
                    var error = createResult.Errors.First();
                    throw new EvnException(error.Description, error.Code);
                }
                return true;
            }
            else
            {
                var roleQuery = await _roleManager.Roles
                .FirstOrDefaultAsync(p => p.Id.Equals(request.Id), cancellationToken);
                if (roleQuery == null)
                {
                    throw new EvnException(String.Format(Resources.MSG_NOT_FOUND, "Vai trò"));
                }

                if (request.Name != roleQuery.Name && await _unitOfWork.RoleRepository.GetAny(x => x.Name == request.Name && x.Module == module))
                {
                    throw new EvnException(String.Format(Resources.MSG_IS_EXIST, "Vai trò"));
                }

                roleQuery.Name = request.Name;
                roleQuery.Type = request.Type;
                roleQuery.Module = module;
                roleQuery.UpdatedDate = DateTime.Now;
                var updateResult = await _roleManager.UpdateAsync(roleQuery);
                if (updateResult.Succeeded)
                {
                    var removeClaims = _unitOfWork.RoleClaimRepository.Find(p => p.RoleId == roleQuery.Id).ToList();
                    _unitOfWork.RoleClaimRepository.RemoveRange(removeClaims);

                    var claims = request.Permissions
                        .Select(p => new RoleClaim
                        {
                            RoleId = roleQuery.Id,
                            ClaimType = Permissions.ClaimType,
                            ClaimValue = p,
                        });

                    _unitOfWork.RoleClaimRepository.AddRange(claims);
                    await _unitOfWork.SaveChangesAsync();

                }
                else
                {
                    var error = updateResult.Errors.First();
                    throw new EvnException(error.Description, error.Code);
                }
                return true;
            }
        }
    }
}
