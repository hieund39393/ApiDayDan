using Authentication.Application.Model.CauHinhChietTinh;
using Authentication.Infrastructure.Repositories;
using EVN.Core.Extensions;
using EVN.Core.Models;
using EVN.Core.SeedWork;
using Microsoft.EntityFrameworkCore;
using static EVN.Core.Common.AppEnum;

namespace Authentication.Application.Queries.CauHinhChietTinhQuery
{
    public interface ICauHinhChietTinhQuery // tạo interface (Quy tắc : có chữ I ở đầu để biết nó là interface)
    {
        Task<PagingResultSP<CauHinhChietTinhResponse>> GetList(CauHinhChietTinhRequest request); // lấy danh sách có phân trang và tìm kiếm
        Task<List<Guid>> GetVatLieuById(GetByIdAndPhanLoaiRequest request);
        Task<List<Guid>> GetNhanCongById(GetByIdAndPhanLoaiRequest request);
        Task<List<Guid>> GetMTCById(GetByIdAndPhanLoaiRequest request);
    }
    public class CauHinhChietTinhQuery : ICauHinhChietTinhQuery // kế thừa interface vừa tạo
    {
        private readonly IUnitOfWork _unitOfWork; // khai báo 
        public CauHinhChietTinhQuery(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // lấy dữ liệu phân trang, tìm kiếm , số lượng
        public async Task<PagingResultSP<CauHinhChietTinhResponse>> GetList(CauHinhChietTinhRequest request)
        {
            //Tạo câu query
            var query = _unitOfWork.CauHinhChietTinhRepository.GetQuery()
                .GroupBy(x => x.IdCongViec)
                .Select(x => new CauHinhChietTinhResponse()
                {
                    IdCongViec = x.Key,
                    TenCongViec = x.First().DM_CongViec.TenCongViec
                }).AsSplitQuery().AsNoTracking();
            if (!string.IsNullOrEmpty(request.SearchTerm))
            {
                query = query.Where(x => x.TenCongViec.ToLower().Contains(request.SearchTerm.ToLower().Trim()));
            }
            var totalRow = query.Count(); // tổng số lượng
            var queryPaging = query.Skip((request.PageIndex - 1) * request.PageSize).Take(request.PageSize); // phân trang
            return await PagingResultSP<CauHinhChietTinhResponse>.CreateAsyncLinq(queryPaging, totalRow, request.PageIndex, request.PageSize);
        }

        public async Task<List<Guid>> GetMTCById(GetByIdAndPhanLoaiRequest request)
        {
            var listData = await _unitOfWork.CauHinhChietTinhRepository.GetQuery(x => x.IdCongViec == request.IdCongViec && x.PhanLoai == (int)PhanLoaiChietTinhEnum.MTC)
              .AsNoTracking().Select(x => x.IdChiTiet.Value).ToListAsync();
         
            return listData;
        }

        public async Task<List<Guid>> GetNhanCongById(GetByIdAndPhanLoaiRequest request)
        {
            var listData = await _unitOfWork.CauHinhChietTinhRepository.GetQuery(x => x.IdCongViec == request.IdCongViec && x.PhanLoai == (int)PhanLoaiChietTinhEnum.NhanCong)
                .AsNoTracking().Select(x => x.IdChiTiet.Value).ToListAsync();
        
            return listData;
        }

        public async Task<List<Guid>> GetVatLieuById(GetByIdAndPhanLoaiRequest request)
        {
            var listData = await _unitOfWork.CauHinhChietTinhRepository.GetQuery(x => x.IdCongViec == request.IdCongViec && x.PhanLoai == (int)PhanLoaiChietTinhEnum.VatLieu)
                .AsNoTracking().Select(x => x.IdChiTiet.Value).ToListAsync();
          
            return listData;
        }

    }
}
