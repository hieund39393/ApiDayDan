using Authentication.Infrastructure.Properties;
using Authentication.Infrastructure.Repositories;
using EVN.Core.Exceptions;
using EVN.Core.Properties;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Authentication.Application.Commands.RoleCommand
{
    public record RoleDeteleCommand(Guid id) : IRequest<bool>;
    public class RoleDeleteCommandHandler : IRequestHandler<RoleDeteleCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        public RoleDeleteCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<bool> Handle(RoleDeteleCommand request, CancellationToken cancellationToken)
        {
            var data = (_unitOfWork.RoleRepository.GetQuery(c => c.Id == request.id)).FirstOrDefault();
            if (data == null)
            {
                throw new EvnException(string.Format(Resources.MSG_NOT_FOUND, "Vai trò"));
            }
            data.IsDeleted = true;
            _unitOfWork.RoleRepository.Update(data);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }

}
