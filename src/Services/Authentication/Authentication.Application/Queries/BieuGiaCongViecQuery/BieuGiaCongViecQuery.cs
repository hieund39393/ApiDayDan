using Authentication.Application.Model.BieuGiaCongViec;
using Authentication.Infrastructure.Migrations;
using Authentication.Infrastructure.Repositories;
using EVN.Core.SeedWork;
using Microsoft.EntityFrameworkCore;

namespace Authentication.Application.Queries.BieuGiaCongViecQuery
{
    public interface IBieuGiaCongViecQuery // tạo interface (Quy tắc : có chữ I ở đầu để biết nó là interface)
    {
        Task<PagingResultSP<BieuGiaCongViecResponse>> GetList(BieuGiaCongViecRequest request); // lấy danh sách có phân trang và tìm kiếm
    }
    public class BieuGiaCongViecQuery : IBieuGiaCongViecQuery // kế thừa interface vừa tạo
    {
        private readonly IUnitOfWork _unitOfWork; // khai báo 
        public BieuGiaCongViecQuery(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // lấy dữ liệu phân trang, tìm kiếm , số lượng
        public async Task<PagingResultSP<BieuGiaCongViecResponse>> GetList(BieuGiaCongViecRequest request)
        {
            //Tạo câu query
            var query = _unitOfWork.BieuGiaCongViecRepository.GetQuery()
                .AsSplitQuery()      // sử dụng include thì khai báo AsSplitQuery dể tăng tốc độ truy vấn
                .Include(x => x.DM_BieuGia)
                .Include(x => x.DM_CongViec)
                .Select(x => new BieuGiaCongViecResponse()
                {
                    Id = x.Id,
                    IdCongViec = x.IdCongViec,
                    IdBieuGia = x.IdBieuGia,
                    IdLoaiBieuGia = x.DM_BieuGia.idLoaiBieuGia,
                    TenBieuGia = x.DM_BieuGia.TenBieuGia,
                    TenCongViec = x.DM_CongViec.TenCongViec,
                    CongViecChinh = x.CongViecChinh
                }).AsNoTracking();// select dữ liệu
            var totalRow = query.Count(); // tổng số lượng
            var queryPaging = query.Skip((request.PageIndex - 1) * request.PageSize).Take(request.PageSize); // phân trang
            return await PagingResultSP<BieuGiaCongViecResponse>.CreateAsyncLinq(queryPaging, totalRow, request.PageIndex, request.PageSize);
        }
    }
}
