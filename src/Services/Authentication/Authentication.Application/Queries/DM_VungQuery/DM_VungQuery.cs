using Authentication.Application.Model.DM_Vung;
using Authentication.Infrastructure.Repositories;
using EVN.Core.Models;
using EVN.Core.SeedWork;
using Microsoft.EntityFrameworkCore;

namespace Authentication.Application.Queries.DM_VungQuery
{
    public interface IDM_VungQuery // tạo interface (Quy tắc : có chữ I ở đầu để biết nó là interface)
    {

        Task<PagingResultSP<DM_VungResponse>> GetList(DM_VungRequest request); // lấy danh sách có phân trang và tìm kiếm
        Task<List<SelectItem>> GetAll(); // lấy Tất cả danh sách trả về tên và value
    }
    public class DM_VungQuery : IDM_VungQuery // kế thừa interface vừa tạo
    {
        private readonly IUnitOfWork _unitOfWork; // khai báo 
        public DM_VungQuery(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // lấy Tất cả danh sách trả về tên và value thường dùng cho combobox
        public async Task<List<SelectItem>> GetAll()
        {
            var query = await _unitOfWork.DM_VungRepository.GetQuery().Select(x => new SelectItem
            {
                Name = x.TenVung,
                Value = x.Id.ToString(),
            }).AsNoTracking().ToListAsync();
            return query;
        }

        // lấy dữ liệu phân trang, tìm kiếm , số lượng
        public async Task<PagingResultSP<DM_VungResponse>> GetList(DM_VungRequest request)
        {
            //Tạo câu query
            var query = _unitOfWork.DM_VungRepository.GetQuery(x =>

            (string.IsNullOrEmpty(request.TenVung) || x.TenVung.Contains(request.TenVung)) &&  //Tìm kiếm
            (string.IsNullOrEmpty(request.GhiChu) || x.GhiChu.Contains(request.GhiChu)))

                .Select(x => new DM_VungResponse()
                {
                    Id = x.Id,
                    TenVung = x.TenVung,
                    GhiChu = x.GhiChu,
                    NgayTao = x.CreatedDate,
                });// select dữ liệu
            var totalRow = query.Count(); // tổng số lượng
            var queryPaging = query.Skip((request.PageIndex - 1) * request.PageSize).Take(request.PageSize); // phân trang
            return await PagingResultSP<DM_VungResponse>.CreateAsyncLinq(queryPaging, totalRow, request.PageIndex, request.PageSize);
        }
    }
}
