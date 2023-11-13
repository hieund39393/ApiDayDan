using Authentication.Application.Model.CauHinh;
using Authentication.Application.Model.Menu;
using Authentication.Infrastructure.Repositories;
using EVN.Core.Extensions;
using EVN.Core.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.Net.WebSockets;
using static EVN.Core.Common.AppEnum;

namespace Authentication.Application.Queries.CommonQuery
{
    public interface ICommonQuery
    {
        Task<List<SelectItem>> ListModule();
        Task<List<MenuItemResponse>> ListMenu();
        Task<List<SelectItem>> ListNhomQuyen();
        Task<List<SelectItem>> ListChucVu();

        Task<object> ListCauHinh(GetListCauHinhRequest request);
        Task<object> ListVanBanThongBao(VanBanThongBaoRequest request);

        List<SelectItem> ListVungKhuVuc();

        Task<string> GetVanBan(VanBanThongBaoRequest request);

    }
    public class CommonQuery : ICommonQuery
    {
        private readonly IUnitOfWork _unitOfWork;
        public CommonQuery(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<object> ListCauHinh(GetListCauHinhRequest request)
        {
            var data = _unitOfWork.CauHinhBieuGiaRepository.GetQuery()
                .Where(x => (string.IsNullOrEmpty(request.TenCauHinh) || x.TenCauHinh.ToLower().Contains(request.TenCauHinh.ToLower())))
                .Where(x => (request.PhanLoai == null || x.PhanLoaiCap == request.PhanLoai.Value))
                .Where(x => request.Nam == null || x.Nam == request.Nam)
                .Where(x => request.Quy == null || x.Quy == request.Quy)
                //.ToLookup(x => new { x.TenCauHinh, x.PhanLoaiCap })
                //.Select(x => x.OrderBy(x => x.Nam).ThenBy(x => x.Quy).Last())
                .Select(x => new GetListCauHinhResponse
                {
                    TenCauHinh = GetDescription((TenCauHinhEnum)int.Parse(x.TenCauHinh)),
                    GiaTri = x.GiaTri,
                    Quy = x.Quy,
                    Nam = x.Nam,
                    PhanLoaiCap = x.PhanLoaiCap,
                    TenPhanLoai = x.PhanLoaiCap == 1 ? "Cáp trên không" : "Cáp ngầm",
                    Id = x.Id
                }).OrderByDescending(x => x.Nam).ThenByDescending(x => x.Quy)
                .ToList();

            return data;
        }
        public static string GetDescription(Enum value)
        {
            var field = value.GetType().GetField(value.ToString());
            var attribute = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) as DescriptionAttribute;
            return attribute == null ? value.ToString() : attribute.Description;
        }

        public async Task<List<SelectItem>> ListChucVu()
        {
            var data = await _unitOfWork.PositionRepository.GetQuery().AsNoTracking()
               .Select(x => new SelectItem
               {
                   Name = x.Title,
                   Value = x.Id.ToString().ToLower(),
               }).ToListAsync();
            return data;
        }
        public async Task<List<MenuItemResponse>> ListMenu()
        {
            var isSupperAdmin = TokenExtensions.IsSuperAdmin();
            var listPermission = TokenExtensions.GetPermission();


            var data = await _unitOfWork.ModuleRepository.GetQuery().Include(x => x.Menus).ThenInclude(m => m.Menus).AsNoTracking().
                Select(x => new MenuItemResponse
                {
                    Id = x.Id,
                    Name = x.Name,
                    Code = x.Code,
                    Order = x.Order,
                    Icon = x.Icon,
                    SubItems = x.Menus.Where(x => (isSupperAdmin || listPermission.Contains(x.Code)) && x.ParenId == null).Select(y => new MenuItemResponse
                    {
                        Id = y.Id,
                        Name = y.Name,
                        Url = y.Url,
                        Code = y.Code,
                        Order = y.Order,
                        SubItems = !y.Menus.Where(u => u.ParenId == y.Id).Any() ? null : y.Menus.Where(u => u.ParenId == y.Id).Select(s => new MenuItemResponse
                        {
                            Id = s.Id,
                            Name = s.Name,
                            Url = s.Url,
                            Code = s.Code,
                            Order = s.Order,
                        }).OrderBy(s => s.Order).ToList(),
                    }).OrderBy(x => x.Order).ToList()
                }).OrderBy(x => x.Order).ToListAsync();

            return data.Where(x => x.SubItems.Any()).ToList();
        }

        public async Task<List<SelectItem>> ListModule()
        {
            var data = await _unitOfWork.ModuleRepository.GetQuery().AsNoTracking()
                .Select(x => new SelectItem
                {
                    Name = x.Name,
                    Value = x.Id.ToString(),
                }).ToListAsync();
            return data;
        }

        public async Task<List<SelectItem>> ListNhomQuyen()
        {
            var data = await _unitOfWork.RoleRepository.GetQuery().AsNoTracking()
                .Select(x => new SelectItem
                {
                    Name = x.Name,
                    Value = x.Id.ToString().ToLower(),
                }).ToListAsync();
            return data;
        }

        public List<SelectItem> ListVungKhuVuc()
        {
            var data = new List<SelectItem>();
            data.Add(new SelectItem { Name = "Vùng 1", Value = "1" });
            data.Add(new SelectItem { Name = "Vùng 2", Value = "2" });
            data.Add(new SelectItem { Name = "Vùng 3", Value = "3" });
            data.Add(new SelectItem { Name = "Vùng 4", Value = "4" });
            data.Add(new SelectItem { Name = "Vùng 5", Value = "5" });
            data.Add(new SelectItem { Name = "Vùng 6", Value = "6" });
            data.Add(new SelectItem { Name = "Vùng 7", Value = "7" });
            data.Add(new SelectItem { Name = "Vùng 8", Value = "8" });
            data.Add(new SelectItem { Name = "Vùng 9", Value = "9" });
            return data;
        }

        public async Task<object> ListVanBanThongBao(VanBanThongBaoRequest request)
        {
            var data = await _unitOfWork.VanBanThongBaoRepository.GetQuery(x =>
            (request.Nam == null || x.Nam == request.Nam) && (request.Quy == null || x.Quy == request.Quy))
                .Select(x => new VanBanThongBaoResponse
                {
                    Id = x.Id,
                    Nam = x.Nam,
                    Quy = x.Quy,
                    GhiChu = x.GhiChu,
                    Url = x.Url,
                }).ToListAsync();
            return data;
        }

        public async Task<string> GetVanBan(VanBanThongBaoRequest request)
        {
            return (await _unitOfWork.VanBanThongBaoRepository.FindOneAsync(x => x.Nam == request.Nam && x.Quy == request.Quy))?.Url;
        }
    }
}
