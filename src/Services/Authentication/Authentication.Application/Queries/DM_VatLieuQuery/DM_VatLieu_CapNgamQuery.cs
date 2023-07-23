using Authentication.Application.Model.DM_VatLieu;
using Authentication.Infrastructure.Repositories;
using EVN.Core.Models;
using EVN.Core.SeedWork;
using Microsoft.EntityFrameworkCore;

namespace Authentication.Application.Queries.DM_VatLieu_CapNgamQuery
{
    public interface IDM_VatLieu_CapNgamQuery // tạo interface (Quy tắc : có chữ I ở đầu để biết nó là interface)
    {

        Task<PagingResultSP<DM_VatLieuResponse>> GetList(DM_VatLieuRequest request); // lấy danh sách có phân trang và tìm kiếm
        Task<List<SelectItem>> GetAll(); // lấy Tất cả danh sách trả về tên và value
    }
    public class DM_VatLieu_CapNgamQuery : IDM_VatLieu_CapNgamQuery // kế thừa interface vừa tạo
    {
        private readonly IUnitOfWork _unitOfWork; // khai báo 
        public DM_VatLieu_CapNgamQuery(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // lấy Tất cả danh sách trả về tên và value thường dùng cho combobox
        public async Task<List<SelectItem>> GetAll()
        {
            var query = await _unitOfWork.DM_VatLieu_CapNgamRepository.GetQuery().Select(x => new SelectItem
            {
                Name = x.TenVatLieu,
                Value = x.Id.ToString(),
            }).AsNoTracking().ToListAsync();
            return query;
        }

        // lấy dữ liệu phân trang, tìm kiếm , số lượng
        public async Task<PagingResultSP<DM_VatLieuResponse>> GetList(DM_VatLieuRequest request)
        {
            //Tạo câu query
            var query = _unitOfWork.DM_VatLieu_CapNgamRepository.GetQuery(x =>

            (string.IsNullOrEmpty(request.SearchTerm) || x.TenVatLieu.Contains(request.SearchTerm)) &&  //Tìm kiếm
            (string.IsNullOrEmpty(request.SearchTerm) || x.MaVatLieu.Contains(request.SearchTerm)))

                .Select(x => new DM_VatLieuResponse()
                {
                    Id = x.Id,
                    TenVatLieu = x.TenVatLieu,
                    MaVatLieu = x.MaVatLieu,
                    DonViTinh = x.DonViTinh,
                    NgayTao = x.CreatedDate,
                    ThuTuHienThi = x.ThuTuHienThi
                });// select dữ liệu
            var totalRow = query.Count(); // tổng số lượng
            var queryPaging = query.Skip((request.PageIndex - 1) * request.PageSize).Take(request.PageSize); // phân trang
            return await PagingResultSP<DM_VatLieuResponse>.CreateAsyncLinq(queryPaging, totalRow, request.PageIndex, request.PageSize);
        }
    }
}
