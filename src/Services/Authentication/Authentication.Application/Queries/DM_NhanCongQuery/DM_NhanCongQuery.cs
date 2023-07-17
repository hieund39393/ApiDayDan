using Authentication.Application.Model.DonGiaNhanCong;
using Authentication.Infrastructure.Repositories;
using EVN.Core.Models;
using EVN.Core.SeedWork;
using Microsoft.EntityFrameworkCore;

namespace Authentication.Application.Queries.DM_NhanCongQuery
{
    public interface IDM_NhanCongQuery // tạo interface (Quy tắc : có chữ I ở đầu để biết nó là interface)
    {

        Task<PagingResultSP<DM_NhanCongResponse>> GetList(DM_NhanCongRequest request); // lấy danh sách có phân trang và tìm kiếm
        Task<List<SelectItem>> GetAll(); // lấy Tất cả danh sách trả về tên và value
    }
    public class DM_NhanCongQuery : IDM_NhanCongQuery // kế thừa interface vừa tạo
    {
        private readonly IUnitOfWork _unitOfWork; // khai báo 
        public DM_NhanCongQuery(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // lấy Tất cả danh sách trả về tên và value thường dùng cho combobox
        public async Task<List<SelectItem>> GetAll()
        {
            var query = await _unitOfWork.DM_NhanCongRepository.GetQuery().Include(x => x.KhuVuc).Select(x => new SelectItem
            {
                Name = $"{x.CapBac} ({x.KhuVuc.TenKhuVuc})",
                Value = x.Id.ToString(),
            }).AsNoTracking().ToListAsync();
            return query;
        }

        // lấy dữ liệu phân trang, tìm kiếm , số lượng
        public async Task<PagingResultSP<DM_NhanCongResponse>> GetList(DM_NhanCongRequest request)
        {
            //Tạo câu query
            var query = _unitOfWork.DM_NhanCongRepository.GetQuery()
                .Include(x=>x.KhuVuc)

                .Select(x => new DM_NhanCongResponse()
                {
                    Id = x.Id,
                    CapBac = x.CapBac,
                    HeSo = x.HeSo,
                    IdVung = x.IdKhuVuc,
                    VungKhuVuc = x.KhuVuc.TenKhuVuc,
                    NgayTao = x.CreatedDate.ToString("dd/MM/yyyy"),
                });// select dữ liệu
            var totalRow = query.Count(); // tổng số lượng
            var queryPaging = query.Skip((request.PageIndex - 1) * request.PageSize).Take(request.PageSize); // phân trang
            return await PagingResultSP<DM_NhanCongResponse>.CreateAsyncLinq(queryPaging, totalRow, request.PageIndex, request.PageSize);
        }
    }
}
