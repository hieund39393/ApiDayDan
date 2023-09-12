using Authentication.Application.Model.DM_BieuGia;
using Authentication.Infrastructure.Repositories;
using EVN.Core.Models;
using EVN.Core.SeedWork;
using Microsoft.EntityFrameworkCore;

namespace Authentication.Application.Queries.DM_BieuGia_CapNgamQuery
{
    public interface IDM_BieuGia_CapNgamQuery // tạo interface (Quy tắc : có chữ I ở đầu để biết nó là interface)
    {
        Task<PagingResultSP<DM_BieuGiaResponse>> GetList(DM_BieuGiaRequest request); // lấy danh sách có phân trang và tìm kiếm
        Task<List<SelectItem>> GetAll(); // lấy Tất cả danh sách trả về tên và value
        Task<List<SelectItem>> GetBieuGiaByLoaiBieuGia(Guid IdLoaiBieuGia); // lấy Tất cả danh sách trả về tên và value
    }
    public class DM_BieuGia_CapNgamQuery : IDM_BieuGia_CapNgamQuery // kế thừa interface vừa tạo
    {
        private readonly IUnitOfWork _unitOfWork; // khai báo 
        public DM_BieuGia_CapNgamQuery(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // lấy Tất cả danh sách trả về tên và value thường dùng cho combobox
        public async Task<List<SelectItem>> GetAll()
        {
            var query = await _unitOfWork.DM_BieuGia_CapNgamRepository.GetQuery().Select(x => new SelectItem
            {
                Name = x.TenBieuGia,
                Value = x.Id.ToString(),
            }).AsNoTracking().ToListAsync();
            return query;
        }

        public async Task<List<SelectItem>> GetBieuGiaByLoaiBieuGia(Guid IdLoaiBieuGia)
        {
            return await _unitOfWork.DM_BieuGia_CapNgamRepository.GetQuery(x => x.idLoaiBieuGia == IdLoaiBieuGia).Select(x => new SelectItem
            {
                Name = x.TenBieuGia,
                Value = x.Id.ToString(),
            }).AsNoTracking().ToListAsync();
        }

        // lấy dữ liệu phân trang, tìm kiếm , số lượng
        public async Task<PagingResultSP<DM_BieuGiaResponse>> GetList(DM_BieuGiaRequest request)
        {
            //Tạo câu query
            var query = _unitOfWork.DM_BieuGia_CapNgamRepository.GetQuery()
              .Include(x => x.DM_LoaiBieuGia_CapNgam.DM_KhuVuc) // đoạn này join để lấy tên loại biểu giá 
              .Select(x => new DM_BieuGiaResponse()
              {
                  Id = x.Id,
                  MaBieuGia = x.MaBieuGia,
                  TenBieuGia = x.TenBieuGia,
                  TenKhuVuc = x.DM_LoaiBieuGia_CapNgam.DM_KhuVuc.TenKhuVuc,
                  TenLoaiBieuGia = x.DM_LoaiBieuGia_CapNgam.TenLoaiBieuGia, // đoạn này mapping tên loại biểu giá
                  idLoaiBieuGia = x.idLoaiBieuGia, // đoạn này mapping tên loại biểu giá
                  CreatedDate = x.CreatedDate,
                  idVung = x.DM_LoaiBieuGia_CapNgam.IdKhuVuc
              });// select dữ liệu

            if (request.IdKhuVuc != null)
            {
                query = query.Where(x => x.idVung == request.IdKhuVuc);

            }
            if (request.IdLoaiBieuGia != null)
            {
                query = query.Where(x => x.idLoaiBieuGia == request.IdLoaiBieuGia);
            }


            var totalRow = query.Count(); // tổng số lượng
            var queryPaging = query.Skip((request.PageIndex - 1) * request.PageSize).Take(request.PageSize); // phân trang
            return await PagingResultSP<DM_BieuGiaResponse>.CreateAsyncLinq(queryPaging, totalRow, request.PageIndex, request.PageSize);
        }
    }
}
