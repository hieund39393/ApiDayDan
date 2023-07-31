using Authentication.Application.Model.DonGiaNhanCong;
using Authentication.Application.Queries.CommonQuery;
using Authentication.Infrastructure.Repositories;
using EVN.Core.Models;
using EVN.Core.SeedWork;
using Microsoft.EntityFrameworkCore;

namespace Authentication.Application.Queries.DonGiaNhanCong_CapNgamQuery
{

    public interface IDonGiaNhanCong_CapNgamQuery // tạo interface (Quy tắc : có chữ I ở đầu để biết nó là interface)
    {

        Task<List<DonGiaNhanCongResponse>> GetList(DonGiaNhanCongRequest request); // lấy danh sách có phân trang và tìm kiếm
        Task<List<SelectItem>> GetAll();                                                                                 //  Task<List<SelectItem>> GetAll(); // lấy Tất cả danh sách trả về tên và value
    }
    public class DonGiaNhanCong_CapNgamQuery : IDonGiaNhanCong_CapNgamQuery // kế thừa interface vừa tạo
    {
        private readonly ICommonQuery _commonQuery;
        private readonly IUnitOfWork _unitOfWork; // khai báo 
        public DonGiaNhanCong_CapNgamQuery(IUnitOfWork unitOfWork, ICommonQuery commonQuery)
        {
            _unitOfWork = unitOfWork;
            _commonQuery = commonQuery;
        }

        public async Task<List<SelectItem>> GetAll()
        {
            var query = await _unitOfWork.DonGiaNhanCong_CapNgamRepository.GetQuery()
                .Include(x => x.NhanCong_CapNgam).ThenInclude(x => x.KhuVuc)
                .Select(x => new SelectItem
                {
                    Name = $"{x.NhanCong_CapNgam.CapBac} ({x.NhanCong_CapNgam.KhuVuc.TenKhuVuc})",
                    Value = x.Id.ToString(),
                }).AsNoTracking().ToListAsync();
            return query;
        }

        public async Task<List<DonGiaNhanCongResponse>> GetList(DonGiaNhanCongRequest request)
        {
            //Tạo câu query
            var query = _unitOfWork.DonGiaNhanCong_CapNgamRepository.GetQuery()
               .Include(x => x.NhanCong_CapNgam).ThenInclude(x => x.KhuVuc)
               .Select(x => new DonGiaNhanCongResponse()
               {
                   Id = x.Id,
                   PhanLoai = x.NhanCong_CapNgam.PhanLoai,
                   Nhom = $"Nhóm {x.NhanCong_CapNgam.PhanLoai}",
                   NhanCong = $"{x.NhanCong_CapNgam.CapBac} ({x.NhanCong_CapNgam.KhuVuc.TenKhuVuc})",
                   DonGia = x.DonGia,
                   IdNhanCong = x.IdNhanCong.Value,
                   DinhMuc = x.DinhMuc,
                   VungKhuVuc = x.VungKhuVuc.ToString(),
                   NgayTao = x.CreatedDate.ToString("dd/MM/yyyy"),
               });// select dữ liệu
            if (request.PhanLoai != 0)
            {
                query = query.Where(x => x.PhanLoai == request.PhanLoai);
            }
            if (!string.IsNullOrEmpty(request.SearchTerm))
            {
                query = query.Where(x => x.NhanCong.Contains(request.SearchTerm.ToLower().Trim()));
            }
            var rs = await query.OrderBy(x => x.IdNhanCong).ThenBy(x => x.VungKhuVuc).ToListAsync();
            return rs;
        }
    }
}
