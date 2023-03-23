using Authentication.Infrastructure.Properties;
using Authentication.Infrastructure.Repositories;
using EVN.Core.Exceptions;
using EVN.Core.Properties;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Authentication.Application.Commands.UserCommand
{
    public record DeleteCommand(Guid id) : IRequest<bool>;
    public class DeleteCommandHandler : IRequestHandler<DeleteCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        public DeleteCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(DeleteCommand request, CancellationToken cancellationToken)
        {
            var data = (_unitOfWork.UserRepository.GetQuery(c => c.Id == request.id)).FirstOrDefault();
            if (data == null)
            {
                throw new EvnException(string.Format(Resources.MSG_NOT_FOUND, "Người dùng"));
            }
            data.IsDeleted = true;
            _unitOfWork.UserRepository.Update(data);
            _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}
