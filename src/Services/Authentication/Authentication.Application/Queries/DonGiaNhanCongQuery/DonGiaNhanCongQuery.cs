using Authentication.Application.Model.DonGiaNhanCong;
using Authentication.Infrastructure.Repositories;
using EVN.Core.Models;
using EVN.Core.SeedWork;
using Microsoft.EntityFrameworkCore;

namespace Authentication.Application.Queries.DonGiaNhanCongQuery
{

    public interface IDonGiaNhanCongQuery // tạo interface (Quy tắc : có chữ I ở đầu để biết nó là interface)
    {

        Task<List<DonGiaNhanCongResponse>> GetList(DonGiaNhanCongRequest request); // lấy danh sách có phân trang và tìm kiếm
        Task<List<SelectItem>> GetAll();
    }
    public class DonGiaNhanCongQuery : IDonGiaNhanCongQuery // kế thừa interface vừa tạo
    {
        private readonly IUnitOfWork _unitOfWork; // khai báo 
        public DonGiaNhanCongQuery(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<SelectItem>> GetAll()
        {
            var query = await _unitOfWork.DonGiaNhanCongRepository.GetQuery().Include(x => x.KhuVuc).AsNoTracking()
            .ToListAsync();

            var result = query.GroupBy(x => new { x.IdKhuVuc, x.HeSo, x.CapBac }).Select(x => x.OrderByDescending(y => y.CreatedDate).First()).ToList()
                .Select(x => new SelectItem
                {
                    Name = $"{x.CapBac} ({x.KhuVuc.TenKhuVuc})",
                    Value = x.IdKhuVuc.ToString(),
                }).ToList();
            return result;
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
        public async Task<List<DonGiaNhanCongResponse>> GetList(DonGiaNhanCongRequest request)
        {
            //Tạo câu query
            var query = _unitOfWork.DonGiaNhanCongRepository.GetQuery()
                .Include(x => x.KhuVuc)
                .Select(x => new DonGiaNhanCongResponse()
                {
                    Id = x.Id,
                    CapBac = x.CapBac,
                    HeSo = x.HeSo,
                    IdKhuVuc = x.IdKhuVuc,
                    DonGia = x.DonGia,
                    DinhMuc = x.DinhMuc,
                    VungKhuVuc = x.KhuVuc.TenKhuVuc,
                    NgayTao = x.CreatedDate.ToString("dd/MM/yyyy"),
                });// select dữ liệu

            if (!string.IsNullOrEmpty(request.SearchTerm))
            {
                query = query.Where(x => x.CapBac.Contains(request.SearchTerm.ToLower().Trim()) || x.HeSo.Contains(request.SearchTerm.ToLower().Trim()) || x.VungKhuVuc.ToLower().Contains(request.SearchTerm.ToLower().Trim()));
            }
            var rs = await query.OrderBy(x => x.IdKhuVuc).ToListAsync();
            return rs;
        }
    }
}
