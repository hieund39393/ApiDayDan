using Authentication.Infrastructure.AggregatesModel.ModuleAggregate;
using Authentication.Infrastructure.Repositories;
using AutoMapper;
using EVN.Core.Exceptions;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Authentication.Infrastructure.Properties;
using Microsoft.AspNetCore.Http;

namespace Authentication.Application.Commands.ModuleCommand
{
    public class ModuleCreateOrUpdate : IRequest<bool>
    {
        public Guid Id { get; set; }
        public string ModuleName { get; set; }
        public string TenVietTat { get; set; }
        public string ModuleCode { get; set; }
        public string Url { get; set; }
        public int TrangThai { get; set; }
        public int NumberOrder { get; set; }
        public string Icon { get; set; }
        public bool TopMenu { get; set; }
        public IFormFile FileAnhModule { get; set; }
    }
    public class ModuleCreateOrUpdateHandler : IRequestHandler<ModuleCreateOrUpdate, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ModuleCreateOrUpdateHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<bool> Handle(ModuleCreateOrUpdate request, CancellationToken cancellationToken)
        {
            var module = _mapper.Map<Module>(request);
            if (module.Id == Guid.Empty)
            {
                module.CreatedDate = DateTime.Now;
                _unitOfWork.ModuleRepository.Add(module);
                await _unitOfWork.SaveChangesAsync();
                return true;
            }
            else
            {
                var data = _unitOfWork.ModuleRepository.GetQuery(x => x.Id == module.Id).FirstOrDefault();
                if (data == null)
                {
                    throw new EvnException(string.Format(Resources.MSG_NOT_FOUND, "Module"));
                }
                data.ModuleName = module.ModuleName ?? data.ModuleName;
                data.Icon = module.Icon ?? data.Icon;
                data.NumberOrder = module.NumberOrder;
                data.ModuleCode = module.ModuleCode;
                data.Url = module.Url ?? data.Url;
                data.UpdatedDate = DateTime.Now;
                _unitOfWork.ModuleRepository.Update(data);
                await _unitOfWork.SaveChangesAsync();
                return true;
            }
        }
    }
}
