using Authentication.Application.Model.Module;
using Authentication.Infrastructure.EF;
using Authentication.Infrastructure.Repositories;
using EVN.Core.SeedWork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Authentication.Application.Queries.ModuleQuery
{
    public interface IModuleQuery
    {
        Task<List<ModuleResponse>> GetListModule();
    }
    public class ModuleQuery : IModuleQuery
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ExOneDbContext _context;

        public ModuleQuery(IUnitOfWork unitOfWork, ExOneDbContext context)
        {
            _unitOfWork = unitOfWork;
            _context = context;
        }

        public async Task<List<ModuleResponse>> GetListModule()
        {

            var query1 = _unitOfWork.ModuleRepository.GetQuery().Include(x => x.ModuleChilds).ToList();
            var query = _unitOfWork.ModuleRepository.GetQuery()
           .Select(x => new ModuleResponse()
           {
               Id = x.Id,
               ModuleName = x.ModuleName,
               ModuleCode = x.ModuleCode,
               NumberOrder = x.NumberOrder,
               Icon = x.Icon,
               Url = x.Url,
               ParentId = x.ParentId,
           }).ToList();

            //var data = from a in _context.Module
            //           join b in _context.Module on a.Id equals b.ParentId
            //           where a.ParentId == null
            //           select new ModuleResponse()
            //           {
            //               Id = a.Id,
            //               ModuleName = a.ModuleName,
            //               ModuleCode = a.ModuleCode,
            //               NumberOrder = a.NumberOrder,
            //               Icon = a.Icon,
            //               Url = a.Url,
            //               ParentId = a.ParentId,
            //               ModuleChild = b.
            //           }



            return query;
        }
    }
}
