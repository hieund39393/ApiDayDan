using Authentication.Application.Model.DonGiaVatLieu;
using Authentication.Application.Queries.CommonQuery;
using Authentication.Infrastructure.Repositories;
using EVN.Core.Models;
using EVN.Core.SeedWork;
using Microsoft.EntityFrameworkCore;

namespace Authentication.Application.Queries.DonGiaVatLieu_CapNgamQuery
{

    public interface IDonGiaVatLieu_CapNgamQuery // tạo interface (Quy tắc : có chữ I ở đầu để biết nó là interface)
    {

        Task<List<DonGiaVatLieuResponse>> GetList(DonGiaVatLieuRequest request); // lấy danh sách có phân trang và tìm kiếm
        Task<List<SelectItem>> GetAll(); // lấy Tất cả danh sách trả về tên và value
    }
    public class DonGiaVatLieu_CapNgamQuery : IDonGiaVatLieu_CapNgamQuery // kế thừa interface vừa tạo
    {
        private readonly ICommonQuery _commonQuery;
        private readonly IUnitOfWork _unitOfWork; // khai báo 
        public DonGiaVatLieu_CapNgamQuery(IUnitOfWork unitOfWork, ICommonQuery commonQuery)
        {
            _unitOfWork = unitOfWork;
            _commonQuery = commonQuery;
        }

        // lấy Tất cả danh sách trả về tên và value thường dùng cho combobox
        public async Task<List<SelectItem>> GetAll()
        {
            var query = await _unitOfWork.DonGiaVatLieu_CapNgamRepository.GetQuery().Select(x => new SelectItem
            {
                Name = x.VanBan,
                Value = x.Id.ToString(),
            }).AsNoTracking().ToListAsync();
            return query;
        }

        // lấy dữ liệu phân trang, tìm kiếm , số lượng
        public async Task<List<DonGiaVatLieuResponse>> GetList(DonGiaVatLieuRequest request)
        {
            var listVungKhuVuc = _commonQuery.ListVungKhuVuc();
            //Tạo câu query
            var query = _unitOfWork.DonGiaVatLieu_CapNgamRepository.GetQuery()
                .Include(x => x.DM_VatLieu_CapNgam)
                .Select(x => new DonGiaVatLieuResponse()
                {
                    Id = x.Id,
                    IdVatLieu = x.IdVatLieu,
                    TenVatLieu = x.DM_VatLieu_CapNgam.TenVatLieu,
                    DonViTinh = x.DM_VatLieu_CapNgam.DonViTinh,
                    VanBan = x.VanBan,
                    DonGia = x.DonGia,
                    DinhMuc = x.DinhMuc,
                    VungKhuVuc = x.VungKhuVuc,
                    TenVungKhuVuc = listVungKhuVuc.FirstOrDefault(y => y.Value == x.VungKhuVuc.ToString()).Name,
                    NgayTao = x.CreatedDate.ToString("dd/MM/yyyy"),
                });// select dữ liệu
            if (request.VungKhuVuc != 0)
            {
                query = query.Where(x => x.VungKhuVuc == request.VungKhuVuc);
            }
            if (!string.IsNullOrEmpty(request.SearchTerm))
            {
                query = query.Where(x => x.TenVatLieu.Contains(request.SearchTerm.ToLower().Trim()) || x.VanBan.Contains(request.SearchTerm.ToLower().Trim()));
            }
            return query.ToList().GroupBy(x => x.IdVatLieu).Select(x => x.OrderByDescending(x => x.NgayTao).First()).ToList();
        }
    }
}
