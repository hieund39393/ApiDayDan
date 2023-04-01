using Authentication.Infrastructure.AggregatesModel.MenuAggregate;
using Authentication.Infrastructure.Properties;
using Authentication.Infrastructure.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authentication.Application.Commands.MenuCommand
{
    public class CreateOrEditMenuCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public Guid ModuleId { get; set; }
        public bool IsActive { get; set; }
    }
    public class MenuCommandHandler : IRequestHandler<CreateOrEditMenuCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public MenuCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(CreateOrEditMenuCommand request, CancellationToken cancellationToken)
        {
            if (request.Id == Guid.Empty)
            {
                var model = new Menu
                {
                    Name = request.Name,
                    Code = request.Code,
                    ModuleId = request.ModuleId,
                    IsActive = request.IsActive,
                };
                _unitOfWork.MenuRepository.Add(model);
            }
            else
            {
                var data = await _unitOfWork.MenuRepository.FindOneAsync(x => x.Id == request.Id);
                if (data == null) throw new Exception(string.Format(Resources.MSG_NOT_FOUND, "Trang"));
                data.Name = request.Name;
                data.ModuleId = request.ModuleId;
                data.IsActive = request.IsActive;
                _unitOfWork.MenuRepository.Update(data);
            }
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
};
