using Authentication.Application.Model.DM_BieuGia;
using Authentication.Application.Model.DM_LoaiBieuGia;
using Authentication.Infrastructure.Repositories;
using EVN.Core.Models;
using EVN.Core.SeedWork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authentication.Application.Queries.DM_BieuGiaQuery
{
    public interface IDM_BieuGiaQuery // tạo interface (Quy tắc : có chữ I ở đầu để biết nó là interface)
    {
        Task<PagingResultSP<DM_BieuGiaResponse>> GetList(DM_BieuGiaRequest request); // lấy danh sách có phân trang và tìm kiếm
        Task<List<SelectItem>> GetAll(); // lấy Tất cả danh sách trả về tên và value
    }
    public class DM_BieuGiaQuery : IDM_BieuGiaQuery // kế thừa interface vừa tạo
    {
        private readonly IUnitOfWork _unitOfWork; // khai báo 
        public DM_BieuGiaQuery(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // lấy Tất cả danh sách trả về tên và value thường dùng cho combobox
        public async Task<List<SelectItem>> GetAll()
        {
            var query = await _unitOfWork.DM_BieuGiaRepository.GetQuery().Select(x => new SelectItem
            {
                Name = x.TenBieuGia,
                Value = x.Id.ToString(),
            }).AsNoTracking().ToListAsync();
            return query;
        }

        // lấy dữ liệu phân trang, tìm kiếm , số lượng
        public async Task<PagingResultSP<DM_BieuGiaResponse>> GetList(DM_BieuGiaRequest request)
        {
            //Tạo câu query
            var query = _unitOfWork.DM_BieuGiaRepository.GetQuery(x =>

            (string.IsNullOrEmpty(request.TenBieuGia) || x.TenBieuGia.Contains(request.TenBieuGia)) &&  //Tìm kiếm
            (string.IsNullOrEmpty(request.MaBieuGia) || x.TenBieuGia.Contains(request.MaBieuGia)))

              .Include(x => x.DM_LoaiBieuGia) // đoạn này join để lấy tên loại biểu giá 
              .Select(x => new DM_BieuGiaResponse()
                {
                    Id = x.Id,
                    MaBieuGia = x.MaBieuGia,
                    TenBieuGia = x.TenBieuGia,
                    TenLoaiBieuGia = x.DM_LoaiBieuGia.TenBieuGia, // đoạn này mapping tên loại biểu giá
                    CreatedDate = x.CreatedDate,
                });// select dữ liệu
            var totalRow = query.Count(); // tổng số lượng
            var queryPaging = query.Skip((request.PageIndex - 1) * request.PageSize).Take(request.PageSize); // phân trang
            return await PagingResultSP<DM_BieuGiaResponse>.CreateAsyncLinq(queryPaging, totalRow, request.PageIndex, request.PageSize);
        }
    }
}
