using Authentication.Application.Model.DM_CongViec;
using Authentication.Infrastructure.Repositories;
using EVN.Core.Models;
using EVN.Core.SeedWork;
using Microsoft.EntityFrameworkCore;

namespace Authentication.Application.Queries.DM_CongViec_CapNgamQuery
{
    public interface IDM_CongViec_CapNgamQuery // tạo interface (Quy tắc : có chữ I ở đầu để biết nó là interface)
    {

        Task<PagingResultSP<DM_CongViecResponse>> GetList(DM_CongViecRequest request); // lấy danh sách có phân trang và tìm kiếm
        Task<List<SelectItem>> GetAll(); // lấy Tất cả danh sách trả về tên và value
    }
    public class DM_CongViec_CapNgamQuery : IDM_CongViec_CapNgamQuery // kế thừa interface vừa tạo
    {
        private readonly IUnitOfWork _unitOfWork; // khai báo 
        public DM_CongViec_CapNgamQuery(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // lấy Tất cả danh sách trả về tên và value thường dùng cho combobox
        public async Task<List<SelectItem>> GetAll()
        {
            var query = await _unitOfWork.DM_CongViec_CapNgamRepository.GetQuery().Select(x => new SelectItem
            {
                Name = x.TenCongViec,
                Value = x.Id.ToString(),
            }).AsNoTracking().ToListAsync();
            return query;
        }

        // lấy dữ liệu phân trang, tìm kiếm , số lượng
        public async Task<PagingResultSP<DM_CongViecResponse>> GetList(DM_CongViecRequest request)
        {
            //Tạo câu query
            var query = _unitOfWork.DM_CongViec_CapNgamRepository.GetQuery(x =>

            (string.IsNullOrEmpty(request.TenCongViec) || x.TenCongViec.Contains(request.TenCongViec)) &&  //Tìm kiếm
            (string.IsNullOrEmpty(request.MaCongViec) || x.MaCongViec.Contains(request.MaCongViec)))

                .Select(x => new DM_CongViecResponse()
                {
                    Id = x.Id,
                    TenCongViec = x.TenCongViec,
                    MaCongViec = x.MaCongViec,
                    DonViTinh = x.DonViTinh,
                    NgayTao = x.CreatedDate,
                    ThuTuHienThi = x.ThuTuHienThi
                });// select dữ liệu
            var totalRow = query.Count(); // tổng số lượng
            var queryPaging = query.Skip((request.PageIndex - 1) * request.PageSize).Take(request.PageSize); // phân trang
            return await PagingResultSP<DM_CongViecResponse>.CreateAsyncLinq(queryPaging, totalRow, request.PageIndex, request.PageSize);
        }
    }
}
