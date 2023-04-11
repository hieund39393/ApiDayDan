using Authentication.Application.Model.DM_LoaiBieuGia;
using Authentication.Infrastructure.Repositories;
using EVN.Core.Models;
using EVN.Core.SeedWork;
using Microsoft.EntityFrameworkCore;

namespace Authentication.Application.Queries.DM_LoaiBieuGiaQuery
{
    public interface IDM_LoaiBieuGiaQuery // tạo interface (Quy tắc : có chữ I ở đầu để biết nó là interface)
    {

        Task<PagingResultSP<DM_LoaiBieuGiaResponse>> GetList(DM_LoaiBieuGiaRequest request); // lấy danh sách có phân trang và tìm kiếm
        Task<List<SelectItem>> GetAll(); // lấy Tất cả danh sách trả về tên và value
    }
    public class DM_LoaiBieuGiaQuery : IDM_LoaiBieuGiaQuery // kế thừa interface vừa tạo
    {
        private readonly IUnitOfWork _unitOfWork; // khai báo 
        public DM_LoaiBieuGiaQuery(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // lấy Tất cả danh sách trả về tên và value thường dùng cho combobox
        public async Task<List<SelectItem>> GetAll()
        {
            var query = await _unitOfWork.DM_LoaiBieuGiaRepository.GetQuery().Select(x => new SelectItem
            {
                Name = x.TenLoaiBieuGia,
                Value = x.Id.ToString(),
            }).AsNoTracking().ToListAsync();
            return query;
        }

        // lấy dữ liệu phân trang, tìm kiếm , số lượng
        public async Task<PagingResultSP<DM_LoaiBieuGiaResponse>> GetList(DM_LoaiBieuGiaRequest request) 
        {
            //Tạo câu query
            var query = _unitOfWork.DM_LoaiBieuGiaRepository.GetQuery(x =>

            (string.IsNullOrEmpty(request.TenLoaiBieuGia) || x.TenLoaiBieuGia.Contains(request.TenLoaiBieuGia)) &&  //Tìm kiếm
            (string.IsNullOrEmpty(request.MaLoaiBieuGia) || x.TenLoaiBieuGia.Contains(request.MaLoaiBieuGia))) 
                .AsSplitQuery()
                .Include(x => x.DM_Vung)
                .Include(x => x.DM_KhuVuc)
                .Select(x => new DM_LoaiBieuGiaResponse()
                {
                    Id = x.Id,
                    MaLoaiBieuGia = x.MaLoaiBieuGia,
                    TenLoaiBieuGia = x.TenLoaiBieuGia,
                    KhuVucID= x.KhuVucID,
                    VungID= x.VungID,
                    TenKhuVuc = x.DM_KhuVuc.TenKhuVuc,
                    TenVung = x.DM_Vung.TenVung,
                    NgayTao = x.CreatedDate,
                });// select dữ liệu
            var totalRow = query.Count(); // tổng số lượng
            var queryPaging = query.Skip((request.PageIndex - 1) * request.PageSize).Take(request.PageSize); // phân trang
            return await PagingResultSP<DM_LoaiBieuGiaResponse>.CreateAsyncLinq(queryPaging, totalRow, request.PageIndex, request.PageSize);
        }
    }
}
