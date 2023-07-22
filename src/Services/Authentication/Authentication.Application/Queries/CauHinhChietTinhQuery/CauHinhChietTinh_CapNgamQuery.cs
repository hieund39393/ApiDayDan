using Authentication.Application.Model.CauHinhChietTinh;
using Authentication.Infrastructure.Repositories;
using EVN.Core.Extensions;
using EVN.Core.SeedWork;
using Microsoft.EntityFrameworkCore;
using static EVN.Core.Common.AppEnum;

namespace Authentication.Application.Queries.CauHinhChietTinhQuery
{
    public interface ICauHinhChietTinh_CapNgamQuery // tạo interface (Quy tắc : có chữ I ở đầu để biết nó là interface)
    {
        Task<PagingResultSP<CauHinhChietTinhResponse>> GetList(CauHinhChietTinhRequest request); // lấy danh sách có phân trang và tìm kiếm
        Task<List<Guid>> GetVatLieuById(GetByIdAndPhanLoaiRequest request);
        Task<List<Guid>> GetNhanCongById(GetByIdAndPhanLoaiRequest request);
        Task<List<Guid>> GetMTCById(GetByIdAndPhanLoaiRequest request);
    }
    public class CauHinhChietTinh_CapNgamQuery : ICauHinhChietTinh_CapNgamQuery // kế thừa interface vừa tạo
    {
        private readonly IUnitOfWork _unitOfWork; // khai báo 
        public CauHinhChietTinh_CapNgamQuery(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // lấy dữ liệu phân trang, tìm kiếm , số lượng
        public async Task<PagingResultSP<CauHinhChietTinhResponse>> GetList(CauHinhChietTinhRequest request)
        {
            //Tạo câu query
            var query = _unitOfWork.CauHinhChietTinh_CapNgamRepository.GetQuery()
                .GroupBy(x => new { x.IdCongViec, x.VungKhuVuc })
                .Select(x => new CauHinhChietTinhResponse()
                {
                    IdCongViec = x.Key.IdCongViec,
                    TenCongViec = x.First().DM_CongViec_CapNgam.TenCongViec,
                    VungKhuVuc = x.Key.VungKhuVuc.ToString()
                }).AsSplitQuery().OrderBy(x => x.IdCongViec).ThenBy(x => x.VungKhuVuc).AsNoTracking();

            if (request.VungKhuVuc != 0)
            {
                query = query.Where(x => x.VungKhuVuc == request.VungKhuVuc.ToString());
            }

            var totalRow = query.Count(); // tổng số lượng
            var queryPaging = query.Skip((request.PageIndex - 1) * request.PageSize).Take(request.PageSize); // phân trang
            return await PagingResultSP<CauHinhChietTinhResponse>.CreateAsyncLinq(queryPaging, totalRow, request.PageIndex, request.PageSize);
        }

        public async Task<List<Guid>> GetMTCById(GetByIdAndPhanLoaiRequest request)
        {
            var listData = await _unitOfWork.CauHinhChietTinh_CapNgamRepository.GetQuery(x => x.IdCongViec == request.IdCongViec && x.PhanLoai == (int)PhanLoaiChietTinhEnum.MTC
                && x.VungKhuVuc == request.VungKhuVuc)
              .AsNoTracking().Select(x => x.IdChiTiet).ToListAsync();
            var result = await _unitOfWork.DM_MTC_CapNgamRepository.GetQuery(x => listData.Contains(x.Id))
                .Select(x => x.Id).ToListAsync();
            return result;
        }

        public async Task<List<Guid>> GetNhanCongById(GetByIdAndPhanLoaiRequest request)
        {
            var listData = await _unitOfWork.CauHinhChietTinh_CapNgamRepository.GetQuery(x => x.IdCongViec == request.IdCongViec && x.PhanLoai == (int)PhanLoaiChietTinhEnum.NhanCong
                && x.VungKhuVuc == request.VungKhuVuc)
                .AsNoTracking().Select(x => x.IdChiTiet).ToListAsync();
            var result = await _unitOfWork.DM_NhanCong_CapNgamRepository.GetQuery(x => listData.Contains(x.Id))
                .Select(x => x.Id).ToListAsync();
            return result;
        }

        public async Task<List<Guid>> GetVatLieuById(GetByIdAndPhanLoaiRequest request)
        {
            var listData = await _unitOfWork.CauHinhChietTinh_CapNgamRepository.GetQuery(x => x.IdCongViec == request.IdCongViec && x.PhanLoai == (int)PhanLoaiChietTinhEnum.VatLieu
                && x.VungKhuVuc == request.VungKhuVuc)
                .AsNoTracking().Select(x => x.IdChiTiet).ToListAsync();
            var result = await _unitOfWork.DM_VatLieu_CapNgamRepository.GetQuery(x => listData.Contains(x.Id))
                .Select(x => x.Id).ToListAsync();
            return result;
        }

    }
}
