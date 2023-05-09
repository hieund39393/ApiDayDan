using Authentication.Application.Model.GiaCap;
using Authentication.Infrastructure.Repositories;
using EVN.Core.Models;
using EVN.Core.SeedWork;
using Microsoft.EntityFrameworkCore;

namespace Authentication.Application.Queries.GiaCapQuery
{

    public interface IGiaCapQuery // tạo interface (Quy tắc : có chữ I ở đầu để biết nó là interface)
    {

        Task<PagingResultSP<GiaCapResponse>> GetList(GiaCapRequest request); // lấy danh sách có phân trang và tìm kiếm
        Task<List<SelectItem>> GetAll(); // lấy Tất cả danh sách trả về tên và value
    }
    public class GiaCapQuery : IGiaCapQuery // kế thừa interface vừa tạo
    {
        private readonly IUnitOfWork _unitOfWork; // khai báo 
        public GiaCapQuery(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // lấy Tất cả danh sách trả về tên và value thường dùng cho combobox
        public async Task<List<SelectItem>> GetAll()
        {
            var query = await _unitOfWork.GiaCapRepository.GetQuery().Select(x => new SelectItem
            {
                Name = x.VanBan,
                Value = x.Id.ToString(),
            }).AsNoTracking().ToListAsync();
            return query;
        }

        // lấy dữ liệu phân trang, tìm kiếm , số lượng
        public async Task<PagingResultSP<GiaCapResponse>> GetList(GiaCapRequest request)
        {
            //Tạo câu query
            var query = _unitOfWork.GiaCapRepository.GetQuery()
                .Include(x => x.DM_LoaiCap)
                .Select(x => new GiaCapResponse()
                {
                    Id = x.Id,
                    IdLoaiCap = x.IdLoaiCap,
                    TenLoaiCap = x.DM_LoaiCap.TenLoaiCap,
                    DonViTinh = x.DM_LoaiCap.DonViTinh,
                    VanBan = x.VanBan,
                    DonGia = x.DonGia,

                    NgayTao = x.CreatedDate,
                });// select dữ liệu

            if (!string.IsNullOrEmpty(request.SearchTerm))
            {
                query = query.Where(x => x.TenLoaiCap.Contains(request.SearchTerm.ToLower().Trim()) || x.VanBan.Contains(request.SearchTerm.ToLower().Trim()));
            }
            var totalRow = query.Count(); // tổng số lượng
            var queryPaging = query.Skip((request.PageIndex - 1) * request.PageSize).Take(request.PageSize); // phân trang
            return await PagingResultSP<GiaCapResponse>.CreateAsyncLinq(queryPaging, totalRow, request.PageIndex, request.PageSize);
        }
    }
}
