using Authentication.Application.Model.DM_MTC;
using Authentication.Infrastructure.Repositories;
using EVN.Core.Models;
using EVN.Core.SeedWork;
using Microsoft.EntityFrameworkCore;

namespace Authentication.Application.Queries.DM_MTCQuery
{
    public interface IDM_MTCQuery // tạo interface (Quy tắc : có chữ I ở đầu để biết nó là interface)
    {

        Task<PagingResultSP<DM_MTCResponse>> GetList(DM_MTCRequest request); // lấy danh sách có phân trang và tìm kiếm
        Task<List<SelectItem>> GetAll(); // lấy Tất cả danh sách trả về tên và value
    }
    public class DM_MTCQuery : IDM_MTCQuery // kế thừa interface vừa tạo
    {
        private readonly IUnitOfWork _unitOfWork; // khai báo 
        public DM_MTCQuery(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // lấy Tất cả danh sách trả về tên và value thường dùng cho combobox
        public async Task<List<SelectItem>> GetAll()
        {
            var query = await _unitOfWork.DM_MTCRepository.GetQuery().Select(x => new SelectItem
            {
                Name = x.TenMayThiCong,
                Value = x.Id.ToString(),
            }).AsNoTracking().ToListAsync();
            return query;
        }

        // lấy dữ liệu phân trang, tìm kiếm , số lượng
        public async Task<PagingResultSP<DM_MTCResponse>> GetList(DM_MTCRequest request)
        {
            //Tạo câu query
            var query = _unitOfWork.DM_MTCRepository.GetQuery(x =>

            (string.IsNullOrEmpty(request.SearchTerm) || x.TenMayThiCong.Contains(request.SearchTerm)) &&  //Tìm kiếm
            (string.IsNullOrEmpty(request.SearchTerm) || x.MaMTC.Contains(request.SearchTerm)))

                .Select(x => new DM_MTCResponse()
                {
                    Id = x.Id,
                    TenMTC = x.TenMayThiCong ,
                    MaMTC = x.MaMTC ,
                    DonViTinh = x.DonViTinh,
                    NgayTao = x.CreatedDate,
                });// select dữ liệu
            var totalRow = query.Count(); // tổng số lượng
            var queryPaging = query.Skip((request.PageIndex - 1) * request.PageSize).Take(request.PageSize); // phân trang
            return await PagingResultSP<DM_MTCResponse>.CreateAsyncLinq(queryPaging, totalRow, request.PageIndex, request.PageSize);
        }
    }
}
