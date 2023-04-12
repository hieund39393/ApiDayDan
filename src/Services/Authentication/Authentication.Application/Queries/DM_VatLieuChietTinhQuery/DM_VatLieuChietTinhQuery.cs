using Authentication.Application.Model.DM_VatLieuChietTinh;
using Authentication.Infrastructure.Repositories;
using EVN.Core.Models;
using EVN.Core.SeedWork;
using Microsoft.EntityFrameworkCore;

namespace Authentication.Application.Queries.DM_VatLieuChietTinhQuery
{
    public interface IDM_VatLieuChietTinhQuery // tạo interface (Quy tắc : có chữ I ở đầu để biết nó là interface)
    {

        Task<PagingResultSP<DM_VatLieuChietTinhResponse>> GetList(DM_VatLieuChietTinhRequest request); // lấy danh sách có phân trang và tìm kiếm
        Task<List<SelectItem>> GetAll(); // lấy Tất cả danh sách trả về tên và value
    }
    public class DM_VatLieuChietTinhQuery : IDM_VatLieuChietTinhQuery // kế thừa interface vừa tạo
    {
        private readonly IUnitOfWork _unitOfWork; // khai báo 
        public DM_VatLieuChietTinhQuery(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // lấy Tất cả danh sách trả về tên và value thường dùng cho combobox
        public async Task<List<SelectItem>> GetAll()
        {
            var query = await _unitOfWork.DM_VatLieuChietTinhRepository.GetQuery().Select(x => new SelectItem
            {
                Name = x.TenVatLieuChietTinh,
                Value = x.Id.ToString(),
            }).AsNoTracking().ToListAsync();
            return query;
        }

        // lấy dữ liệu phân trang, tìm kiếm , số lượng
        public async Task<PagingResultSP<DM_VatLieuChietTinhResponse>> GetList(DM_VatLieuChietTinhRequest request)
        {
            //Tạo câu query
            var query = _unitOfWork.DM_VatLieuChietTinhRepository.GetQuery(x =>

            (string.IsNullOrEmpty(request.SearchTerm) || x.TenVatLieuChietTinh.Contains(request.SearchTerm)) &&  //Tìm kiếm
            (string.IsNullOrEmpty(request.SearchTerm) || x.MaVatLieuChietTinh.Contains(request.SearchTerm)))

                .Select(x => new DM_VatLieuChietTinhResponse()
                {
                    Id = x.Id,
                    TenVatLieuChietTinh = x.TenVatLieuChietTinh,
                    MaVatLieuChietTinh = x.MaVatLieuChietTinh,
                    DonViTinh = x.DonViTinh,
                    NgayTao = x.CreatedDate,
                });// select dữ liệu
            var totalRow = query.Count(); // tổng số lượng
            var queryPaging = query.Skip((request.PageIndex - 1) * request.PageSize).Take(request.PageSize); // phân trang
            return await PagingResultSP<DM_VatLieuChietTinhResponse>.CreateAsyncLinq(queryPaging, totalRow, request.PageIndex, request.PageSize);
        }
    }
}
