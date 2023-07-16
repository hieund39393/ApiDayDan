using Authentication.Application.Model.DonGiaVatLieu;
using Authentication.Infrastructure.Repositories;
using EVN.Core.Models;
using EVN.Core.SeedWork;
using Microsoft.EntityFrameworkCore;

namespace Authentication.Application.Queries.DonGiaVatLieuQuery
{

    public interface IDonGiaVatLieuQuery // tạo interface (Quy tắc : có chữ I ở đầu để biết nó là interface)
    {

        Task<List<DonGiaVatLieuResponse>> GetList(DonGiaVatLieuRequest request); // lấy danh sách có phân trang và tìm kiếm
        Task<List<SelectItem>> GetAll(); // lấy Tất cả danh sách trả về tên và value
    }
    public class DonGiaVatLieuQuery : IDonGiaVatLieuQuery // kế thừa interface vừa tạo
    {
        private readonly IUnitOfWork _unitOfWork; // khai báo 
        public DonGiaVatLieuQuery(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // lấy Tất cả danh sách trả về tên và value thường dùng cho combobox
        public async Task<List<SelectItem>> GetAll()
        {
            var query = await _unitOfWork.DonGiaVatLieuRepository.GetQuery().Select(x => new SelectItem
            {
                Name = x.VanBan,
                Value = x.Id.ToString(),
            }).AsNoTracking().ToListAsync();
            return query;
        }

        // lấy dữ liệu phân trang, tìm kiếm , số lượng
        public async Task<List<DonGiaVatLieuResponse>> GetList(DonGiaVatLieuRequest request)
        {
            //Tạo câu query
            var query = _unitOfWork.DonGiaVatLieuRepository.GetQuery()
                .Include(x => x.DM_VatLieu)
                .Select(x => new DonGiaVatLieuResponse()
                {
                    Id = x.Id,
                    IdVatLieu = x.IdVatLieu,
                    TenVatLieu = x.DM_VatLieu.TenVatLieu,
                    DonViTinh = x.DM_VatLieu.DonViTinh,
                    VanBan = x.VanBan,
                    DonGia = x.DonGia,
                    DinhMuc = x.DinhMuc,
                    CreatedDate = x.CreatedDate,
                    NgayTao = x.CreatedDate.ToString("dd/MM/yyyy"),
                });// select dữ liệu

            if (!string.IsNullOrEmpty(request.SearchTerm))
            {
                query = query.Where(x => x.TenVatLieu.Contains(request.SearchTerm.ToLower().Trim()) || x.VanBan.Contains(request.SearchTerm.ToLower().Trim()));
            }
            var rs = await query.OrderBy(x => x.IdVatLieu).ToListAsync();
            return rs;

            //var totalRow = query.Count(); // tổng số lượng
            //var queryPaging = query.Skip((request.PageIndex - 1) * request.PageSize).Take(request.PageSize); // phân trang
            //return await PagingResultSP<DonGiaVatLieuResponse>.CreateAsyncLinq(queryPaging, totalRow, request.PageIndex, request.PageSize);
        }
    }
}
