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
        Task<List<SelectItem>> GetBieuGiaByLoaiBieuGia(Guid IdLoaiBieuGia); // lấy Tất cả danh sách trả về tên và value
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

        public async Task<List<SelectItem>> GetBieuGiaByLoaiBieuGia(Guid IdLoaiBieuGia)
        {
            return await _unitOfWork.DM_BieuGiaRepository.GetQuery(x => x.idLoaiBieuGia == IdLoaiBieuGia).Select(x => new SelectItem
            {
                Name = x.TenBieuGia,
                Value = x.Id.ToString(),
            }).AsNoTracking().ToListAsync();
        }

        // lấy dữ liệu phân trang, tìm kiếm , số lượng
        public async Task<PagingResultSP<DM_BieuGiaResponse>> GetList(DM_BieuGiaRequest request)
        {
            //Tạo câu query
            var query = _unitOfWork.DM_BieuGiaRepository.GetQuery()
              .Include(x => x.DM_LoaiBieuGia.DM_KhuVuc) // đoạn này join để lấy tên loại biểu giá 
              .Select(x => new DM_BieuGiaResponse()
              {
                  Id = x.Id,
                  MaBieuGia = x.MaBieuGia,
                  TenBieuGia = x.TenBieuGia,
                  TenKhuVuc = x.DM_LoaiBieuGia.DM_KhuVuc.TenKhuVuc,
                  TenLoaiBieuGia = x.DM_LoaiBieuGia.TenLoaiBieuGia, // đoạn này mapping tên loại biểu giá
                  idLoaiBieuGia = x.idLoaiBieuGia, // đoạn này mapping tên loại biểu giá
                  CreatedDate = x.CreatedDate,
              });// select dữ liệu

            if (!string.IsNullOrEmpty(request.SearchTerm))
            {
                query = query.Where(x => x.TenKhuVuc.Contains(request.SearchTerm.ToLower().Trim()) || x.TenLoaiBieuGia.Contains(request.SearchTerm.ToLower().Trim()) || x.TenBieuGia.Contains(request.SearchTerm.ToLower().Trim()));

            }


            var totalRow = query.Count(); // tổng số lượng
            var queryPaging = query.Skip((request.PageIndex - 1) * request.PageSize).Take(request.PageSize); // phân trang
            return await PagingResultSP<DM_BieuGiaResponse>.CreateAsyncLinq(queryPaging, totalRow, request.PageIndex, request.PageSize);
        }
    }
}
