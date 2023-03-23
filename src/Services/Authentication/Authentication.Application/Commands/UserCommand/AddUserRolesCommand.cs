using Authentication.Infrastructure.Repositories;
using Authentication.Infrastructure.AggregatesModel.UserAggregate;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Authentication.Application.Commands.UserCommand
{
    public class AddUserRolesCommand : IRequest<bool>
    {
        public Guid UserId { get; set; }
        public List<Guid> RoleId { get; set; }
    }
    public class AddUserRoleCommandHandler : IRequestHandler<AddUserRolesCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        public AddUserRoleCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<bool> Handle(AddUserRolesCommand request, CancellationToken cancellationToken)
        {
            var data = _unitOfWork.UserRoleRepository.GetQuery(x => x.UserId == request.UserId).ToList();
            if (data.Count > 0)
            {
                _unitOfWork.UserRoleRepository.RemoveRange(data);
                await _unitOfWork.SaveChangesAsync();
            }

            UserRole userRole = new UserRole();
            foreach (var item in request.RoleId)
            {
                userRole.UserId = request.UserId;
                userRole.RoleId = item;
                userRole.CreatedDate = DateTime.Now;
                _unitOfWork.UserRoleRepository.Add(userRole);
                await _unitOfWork.SaveChangesAsync();
            }
            return true;

        }
    }
}
