using Authentication.Application.Model.DonGiaMTC;
using Authentication.Infrastructure.Repositories;
using EVN.Core.Models;
using EVN.Core.SeedWork;
using Microsoft.EntityFrameworkCore;

namespace Authentication.Application.Queries.DonGiaMTC_CapNgamQuery
{

    public interface IDonGiaMTC_CapNgamQuery // tạo interface (Quy tắc : có chữ I ở đầu để biết nó là interface)
    {

        Task<PagingResultSP<DonGiaMTCResponse>> GetList(DonGiaMTCRequest request); // lấy danh sách có phân trang và tìm kiếm
        Task<List<SelectItem>> GetAll(); // lấy Tất cả danh sách trả về tên và value
    }
    public class DonGiaMTC_CapNgamQuery : IDonGiaMTC_CapNgamQuery // kế thừa interface vừa tạo
    {
        private readonly IUnitOfWork _unitOfWork; // khai báo 
        public DonGiaMTC_CapNgamQuery(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // lấy Tất cả danh sách trả về tên và value thường dùng cho combobox
        public async Task<List<SelectItem>> GetAll()
        {
            var query = await _unitOfWork.DonGiaMTC_CapNgamRepository.GetQuery().Select(x => new SelectItem
            {
                Name = x.VanBan,
                Value = x.Id.ToString(),
            }).AsNoTracking().ToListAsync();
            return query;
        }

        // lấy dữ liệu phân trang, tìm kiếm , số lượng
        public async Task<PagingResultSP<DonGiaMTCResponse>> GetList(DonGiaMTCRequest request)
        {
            //Tạo câu query
            var query = _unitOfWork.DonGiaMTC_CapNgamRepository.GetQuery()
                .Include(x => x.DM_MTC_CapNgam)
                .Select(x => new DonGiaMTCResponse()
                {
                    Id = x.Id,
                    IdMTC = x.IdMTC,
                    TenMTC = x.DM_MTC_CapNgam.TenMTC,
                    DonViTinh = x.DM_MTC_CapNgam.DonViTinh,
                    VanBan = x.VanBan,
                    DonGia = x.DonGia,
                    DinhMuc = x.DinhMuc,
                    NgayTao = x.CreatedDate.ToString("dd/MM/yyyy"),
                });// select dữ liệu

            if (!string.IsNullOrEmpty(request.SearchTerm))
            {
                query = query.Where(x => x.TenMTC.Contains(request.SearchTerm.ToLower().Trim()) || x.VanBan.Contains(request.SearchTerm.ToLower().Trim()));
            }

            var totalRow = query.Count(); // tổng số lượng
            var queryPaging = query.Skip((request.PageIndex - 1) * request.PageSize).Take(request.PageSize); // phân trang
            return await PagingResultSP<DonGiaMTCResponse>.CreateAsyncLinq(queryPaging, totalRow, request.PageIndex, request.PageSize);
        }
    }
}
