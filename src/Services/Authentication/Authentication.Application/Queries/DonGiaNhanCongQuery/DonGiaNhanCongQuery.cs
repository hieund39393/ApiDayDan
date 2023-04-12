using Authentication.Application.Model.DonGiaNhanCong;
using Authentication.Infrastructure.Repositories;
using EVN.Core.Models;
using EVN.Core.SeedWork;
using Microsoft.EntityFrameworkCore;

namespace Authentication.Application.Queries.DonGiaNhanCongQuery
{

    public interface IDonGiaNhanCongQuery // tạo interface (Quy tắc : có chữ I ở đầu để biết nó là interface)
    {

        Task<PagingResultSP<DonGiaNhanCongResponse>> GetList(DonGiaNhanCongRequest request); // lấy danh sách có phân trang và tìm kiếm
                                                                                             //  Task<List<SelectItem>> GetAll(); // lấy Tất cả danh sách trả về tên và value
    }
    public class DonGiaNhanCongQuery : IDonGiaNhanCongQuery // kế thừa interface vừa tạo
    {
        private readonly IUnitOfWork _unitOfWork; // khai báo 
        public DonGiaNhanCongQuery(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // lấy Tất cả danh sách trả về tên và value thường dùng cho combobox
        //public async Task<List<SelectItem>> GetAll()
        //{
        //    var query = await _unitOfWork.DonGiaNhanCongRepository.GetQuery().Select(x => new SelectItem
        //    {
        //        Name = x.,
        //        Value = x.Id.ToString(),
        //    }).AsNoTracking().ToListAsync();
        //    return query;
        //}

        // lấy dữ liệu phân trang, tìm kiếm , số lượng
        public async Task<PagingResultSP<DonGiaNhanCongResponse>> GetList(DonGiaNhanCongRequest request)
        {
            //Tạo câu query
            var query = _unitOfWork.DonGiaNhanCongRepository.GetQuery(x =>

            (string.IsNullOrEmpty(request.SearchTerm) || x.HeSo.Contains(request.SearchTerm)) &&  //Tìm kiếm
            (string.IsNullOrEmpty(request.SearchTerm) || x.DonGia.ToString().Contains(request.SearchTerm)) &&
            (string.IsNullOrEmpty(request.SearchTerm) || x.CapBac.Contains(request.SearchTerm)))
                .Include(x => x.Vung)
                .Include(x => x.KhuVuc)
                .Select(x => new DonGiaNhanCongResponse()
                {
                    Id = x.Id,
                    CapBac = x.CapBac,
                    HeSo = x.HeSo,
                    IdVung = x.IdVung,
                    IdKhuVuc = x.IdKhuVuc,
                    DonGia = x.DonGia,
                    VungKhuVuc = x.Vung.TenVung + " / " + x.KhuVuc.TenKhuVuc,
                    NgayTao = x.CreatedDate,
                });// select dữ liệu
            var totalRow = query.Count(); // tổng số lượng
            var queryPaging = query.Skip((request.PageIndex - 1) * request.PageSize).Take(request.PageSize); // phân trang
            return await PagingResultSP<DonGiaNhanCongResponse>.CreateAsyncLinq(queryPaging, totalRow, request.PageIndex, request.PageSize);
        }
    }
}
