using Authentication.Application.Model.DM_LoaiCap;
using Authentication.Infrastructure.Repositories;
using EVN.Core.Models;
using EVN.Core.SeedWork;
using Microsoft.EntityFrameworkCore;

namespace Authentication.Application.Queries.DM_LoaiCapQuery
{
    public interface IDM_LoaiCapQuery // tạo interface (Quy tắc : có chữ I ở đầu để biết nó là interface)
    {

        Task<PagingResultSP<DM_LoaiCapResponse>> GetList(DM_LoaiCapRequest request); // lấy danh sách có phân trang và tìm kiếm
        Task<List<SelectItem>> GetAll(); // lấy Tất cả danh sách trả về tên và value
    }
    public class DM_LoaiCapQuery : IDM_LoaiCapQuery // kế thừa interface vừa tạo
    {
        private readonly IUnitOfWork _unitOfWork; // khai báo 
        public DM_LoaiCapQuery(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // lấy Tất cả danh sách trả về tên và value thường dùng cho combobox
        public async Task<List<SelectItem>> GetAll()
        {
            var query = await _unitOfWork.DM_LoaiCapRepository.GetQuery().Select(x => new SelectItem
            {
                Name = x.TenLoaiCap,
                Value = x.Id.ToString(),
            }).AsNoTracking().ToListAsync();
            return query;
        }

        // lấy dữ liệu phân trang, tìm kiếm , số lượng
        public async Task<PagingResultSP<DM_LoaiCapResponse>> GetList(DM_LoaiCapRequest request)
        {
            //Tạo câu query
            var query = _unitOfWork.DM_LoaiCapRepository.GetQuery(x =>

            (string.IsNullOrEmpty(request.SearchTerm) || x.TenLoaiCap.Contains(request.SearchTerm)) &&  //Tìm kiếm
            (string.IsNullOrEmpty(request.SearchTerm) || x.MaLoaiCap.Contains(request.SearchTerm)))

                .Select(x => new DM_LoaiCapResponse()
                {
                    Id = x.Id,
                    TenLoaiCap = x.TenLoaiCap ,
                    MaLoaiCap = x.MaLoaiCap ,
                    DonViTinh = x.DonViTinh,
                    NgayTao = x.CreatedDate,
                });// select dữ liệu
            var totalRow = query.Count(); // tổng số lượng
            var queryPaging = query.Skip((request.PageIndex - 1) * request.PageSize).Take(request.PageSize); // phân trang
            return await PagingResultSP<DM_LoaiCapResponse>.CreateAsyncLinq(queryPaging, totalRow, request.PageIndex, request.PageSize);
        }
    }
}
