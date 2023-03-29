using Authentication.Application.Model.Menu;
using Authentication.Application.Model.Menu;
using Authentication.Application.Services;
using Authentication.Infrastructure.EF;
using Authentication.Infrastructure.Repositories;
using EVN.Core.SeedWork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Authentication.Application.Queries.MenuQuery
{
    public interface IMenuQuery
    {
        //Task<List<MenuResponse>> GetListMenu();
        Task<PagingResultSP<MenuResponse>> GetListMenu(MenuRequest request);
    }
    public class MenuQuery : BaseSortingService, IMenuQuery
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ExOneDbContext _context;

        public MenuQuery(IUnitOfWork unitOfWork, ExOneDbContext context)
        {
            _unitOfWork = unitOfWork;
            _context = context;
        }

        public async Task<PagingResultSP<MenuResponse>> GetListMenu(MenuRequest request)
        {
            var query = _unitOfWork.MenuRepository.GetQuery(x => request.ModuleId == null || x.ModuleId == request.ModuleId).AsNoTracking()
                .Include(x => x.Module)
                .Select(x => new MenuResponse()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Code = x.Code,
                    IsActive = x.IsActive,
                    ModuleName = x.Module.Name,
                    ModuleCode = x.Module.Code,
                    CreatedDate = x.CreatedDate,
                });

            var totalRow = query.Count();
            var queryPaging = PagingAndSorting(request, query);
            return await PagingResultSP<MenuResponse>.CreateAsyncLinq(queryPaging, totalRow, request.PageIndex, request.PageSize);
        }
    }
}
