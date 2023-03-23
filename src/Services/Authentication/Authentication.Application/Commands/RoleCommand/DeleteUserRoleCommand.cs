using Authentication.Infrastructure.Properties;
using Authentication.Infrastructure.Repositories;
using EVN.Core.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Authentication.Application.Commands.RoleCommand
{
    public class DeleteUserRoleCommand : IRequest<bool>
    {
        public Guid UserId { get; set; }
        public Guid RoleId { get; set; }
    }
    public class DeleteRoleCommandHandler : IRequestHandler<DeleteUserRoleCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        public DeleteRoleCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<bool> Handle(DeleteUserRoleCommand request, CancellationToken cancellationToken)
        {
            var data = await _unitOfWork.UserRoleRepository.FindOneAsync(x => x.UserId == request.UserId && x.RoleId == request.RoleId);
            if (data == null)
            {
                throw new EvnException(Resources.MSG_NOT_FOUND, "Phân quyền");
            }
            _unitOfWork.UserRoleRepository.Remove(data);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}
