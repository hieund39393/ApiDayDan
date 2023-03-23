using Authentication.Application.Model.Module;
using Authentication.Infrastructure.Repositories;
using EVN.Core.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Authentication.Application.Queries.ModuleQuery
{
    public interface IModuleQuery
    {
        Task<List<ModuleResponse>> GetListModule(ModuleRequest request);
    }
    public class ModuleQuery : IModuleQuery
    {
        private readonly IUnitOfWork _unitOfWork;
        public ModuleQuery(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<List<ModuleResponse>> GetListModule(ModuleRequest request)
        {
            var query = _unitOfWork.ModuleRepository.GetQuery(x => (request.Id == Guid.Empty || x.Id == request.Id)
           && (string.IsNullOrEmpty(request.SearchTerm) || x.ModuleName.Contains(request.SearchTerm)))
           .Select(x => new ModuleResponse()
           {
               Id = x.Id,
               ModuleName = x.ModuleName,
               ModuleCode = x.ModuleCode,
               NumberOrder = x.NumberOrder,
               Icon = x.Icon,
               Url = x.Url,

           }).ToList();
            return query;
        }
    }
}
