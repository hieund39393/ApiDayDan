using Authentication.Application.Model.Menu;
using Authentication.Infrastructure.Repositories;
using EVN.Core.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authentication.Application.Queries.CommonQuery
{
    public interface ICommonQuery
    {
        Task<List<SelectItem>> ListModule();
        Task<List<MenuItemResponse>> ListMenu();
    }
    public class CommonQuery : ICommonQuery
    {
        private readonly IUnitOfWork _unitOfWork;
        public CommonQuery(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<List<MenuItemResponse>> ListMenu()
        {
            var data = await _unitOfWork.ModuleRepository.GetQuery().Include(x => x.Menus).AsNoTracking().
                Select(x => new MenuItemResponse
                {
                    Id = x.Id,
                    Name = x.Name,
                    Code = x.Code,
                    Order = x.Order,
                    SubItems = x.Menus.Select(y => new MenuItemResponse
                    {

                        Id = y.Id,
                        Name = y.Name,
                        Url = y.Url,
                        Code = y.Code,
                        Order = y.Order,
                    }).OrderBy(x => x.Order).ToList()
                }).OrderBy(x => x.Order).ToListAsync();

            return data;
        }

        public async Task<List<SelectItem>> ListModule()
        {
            var data = await _unitOfWork.ModuleRepository.GetQuery().AsNoTracking()
                .Select(x=> new SelectItem
                {
                    Name = x.Name,
                    Value = x.Id.ToString(),
                }).ToListAsync();
            return data;
        }
    }
}
