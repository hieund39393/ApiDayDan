using Authentication.Infrastructure.Properties;
using Authentication.Infrastructure.Repositories;
using EVN.Core.Exceptions;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Authentication.Application.Commands.ModuleCommand
{
    public record ModuleDeteleCommand(Guid id) : IRequest<bool>;
    public class ModuleDeteleCommandHandler : IRequestHandler<ModuleDeteleCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        public ModuleDeteleCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<bool> Handle(ModuleDeteleCommand request, CancellationToken cancellationToken)
        {
            var data = (_unitOfWork.ModuleRepository.GetQuery(c => c.Id == request.id)).FirstOrDefault();
            if (data == null)
            {
                throw new EvnException(string.Format(Resources.MSG_NOT_FOUND, "Module"));
            }
            data.IsDeleted = true;
            _unitOfWork.ModuleRepository.Update(data);
            _unitOfWork.SaveChangesAsync();
            return true;
        }
    }

}
