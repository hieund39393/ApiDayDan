using Authentication.Application.Model.DonGiaChietTinh;
using Authentication.Infrastructure.Repositories;
using EVN.Core.Extensions;
using EVN.Core.SeedWork;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using static EVN.Core.Common.AppEnum;

namespace Authentication.Application.Queries.DonGiaChietTinhQuery
{

    public interface IDonGiaChietTinhQuery // tạo interface (Quy tắc : có chữ I ở đầu để biết nó là interface)
    {

        Task<PagingResultSP<DonGiaChietTinhResponse>> GetList(DonGiaChietTinhRequest request); // lấy danh sách có phân trang và tìm kiếm
                                                                                               //  Task<List<SelectItem>> GetAll(); // lấy Tất cả danh sách trả về tên và value
    }
    public class DonGiaChietTinhQuery : IDonGiaChietTinhQuery // kế thừa interface vừa tạo
    {
        private readonly IUnitOfWork _unitOfWork; // khai báo 
        public DonGiaChietTinhQuery(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // lấy Tất cả danh sách trả về tên và value thường dùng cho combobox
        //public async Task<List<SelectItem>> GetAll()
        //{
        //    var query = await _unitOfWork.DonGiaChietTinhRepository.GetQuery().Select(x => new SelectItem
        //    {
        //        Name = x.,
        //        Value = x.Id.ToString(),
        //    }).AsNoTracking().ToListAsync();
        //    return query;
        //}

        // lấy dữ liệu phân trang, tìm kiếm , số lượng
        public async Task<PagingResultSP<DonGiaChietTinhResponse>> GetList(DonGiaChietTinhRequest request)
        {
            //Tạo câu query
            var query = _unitOfWork.DonGiaChietTinhRepository.GetQuery()
                .Include(x => x.DM_VatLieuChietTinh)
                .Select(x => new DonGiaChietTinhResponse()
                {
                    Id = x.Id,
                    IdVatLieuChietTinh = x.IdVatLieuChietTinh,
                    TenVatLieuChietTinh = x.DM_VatLieuChietTinh.TenVatLieuChietTinh,
                    MaVatLieuChietTinh = x.DM_VatLieuChietTinh.MaVatLieuChietTinh,
                    DonViTinh = x.DM_VatLieuChietTinh.DonViTinh,
                    DonGia = x.DonGia,
                    PhanLoai = GetDescription((DonGiaChietTinhPhanLoai)x.IdPhanLoai),
                    IdPhanLoai = x.IdPhanLoai,
                    TongGia = x.TongGia,
                    NgayTao = x.CreatedDate,
                });// select dữ liệu

            if (!string.IsNullOrEmpty(request.SearchTerm))
            {
                query = query.Where(x => x.TenVatLieuChietTinh.Contains(request.SearchTerm.ToLower().Trim()) || x.MaVatLieuChietTinh.Contains(request.SearchTerm.ToLower().Trim()) || x.PhanLoai.Contains(request.SearchTerm.ToLower().Trim()));
            }

            var totalRow = query.Count(); // tổng số lượng
            var queryPaging = query.Skip((request.PageIndex - 1) * request.PageSize).Take(request.PageSize); // phân trang
            return await PagingResultSP<DonGiaChietTinhResponse>.CreateAsyncLinq(queryPaging, totalRow, request.PageIndex, request.PageSize);
        }

        public static string GetDescription(Enum value)
        {
            var field = value.GetType().GetField(value.ToString());
            var attribute = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) as DescriptionAttribute;
            return attribute == null ? value.ToString() : attribute.Description;
        }

    }
}
