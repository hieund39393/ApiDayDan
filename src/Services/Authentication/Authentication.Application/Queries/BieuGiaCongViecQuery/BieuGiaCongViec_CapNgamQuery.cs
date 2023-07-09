﻿using Authentication.Application.Model.BieuGiaCongViec;
using Authentication.Infrastructure.Migrations;
using Authentication.Infrastructure.Repositories;
using EVN.Core.Extensions;
using EVN.Core.SeedWork;
using Microsoft.EntityFrameworkCore;

namespace Authentication.Application.Queries.BieuGiaCongViec_CapNgamQuery
{
    public interface IBieuGiaCongViec_CapNgamQuery // tạo interface (Quy tắc : có chữ I ở đầu để biết nó là interface)
    {
        Task<PagingResultSP<BieuGiaCongViecResponse>> GetList(BieuGiaCongViecRequest request); // lấy danh sách có phân trang và tìm kiếm
    }
    public class BieuGiaCongViec_CapNgamQuery : IBieuGiaCongViec_CapNgamQuery // kế thừa interface vừa tạo
    {
        private readonly IUnitOfWork _unitOfWork; // khai báo 
        public BieuGiaCongViec_CapNgamQuery(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // lấy dữ liệu phân trang, tìm kiếm , số lượng
        public async Task<PagingResultSP<BieuGiaCongViecResponse>> GetList(BieuGiaCongViecRequest request)
        {
            //Tạo câu query
            var query = _unitOfWork.BieuGiaCongViec_CapNgamRepository.GetQuery()
                .Select(x => new BieuGiaCongViecResponse()
                {
                    Id = x.Id,
                    IdCongViec = x.IdCongViec,
                    PhanLoai = x.PhanLoai,
                    IdBieuGia = x.IdBieuGia,
                    IdLoaiBieuGia = x.DM_BieuGia_CapNgam.idLoaiBieuGia,
                    TenLoaiBieuGia = x.DM_BieuGia_CapNgam.DM_LoaiBieuGia_CapNgam.TenLoaiBieuGia,
                    TenBieuGia = x.DM_BieuGia_CapNgam.TenBieuGia,
                    TenCongViec = x.DM_CongViec_CapNgam.TenCongViec,
                    CongViecChinh = x.CongViecChinh,
                    IdKhuVuc = x.DM_BieuGia_CapNgam.DM_LoaiBieuGia_CapNgam.IdKhuVuc,
                    VungKhuVuc = x.DM_BieuGia_CapNgam.DM_LoaiBieuGia_CapNgam.DM_KhuVuc.TenKhuVuc,
                }).AsSplitQuery().AsNoTracking();

            if (request.IdKhuVuc.HasValue)
            {
                query = query.Where(x => x.IdKhuVuc.HasValue && x.IdKhuVuc == request.IdKhuVuc);
            }
            if (request.IdLoaiBieuGia.HasValue)
            {
                query = query.Where(x => x.IdLoaiBieuGia.HasValue && x.IdLoaiBieuGia == request.IdLoaiBieuGia);
            }
            if (request.IdBieuGia.HasValue)
            {
                query = query.Where(x => x.IdBieuGia.HasValue && x.IdBieuGia == request.IdBieuGia);
            }
            query = query.OrderBy(x => x.VungKhuVuc).ThenBy(x => x.TenLoaiBieuGia).ThenBy(x => x.TenBieuGia);

            var totalRow = query.Count(); // tổng số lượng
            var queryPaging = query.Skip((request.PageIndex - 1) * request.PageSize).Take(request.PageSize); // phân trang
            return await PagingResultSP<BieuGiaCongViecResponse>.CreateAsyncLinq(queryPaging, totalRow, request.PageIndex, request.PageSize);
        }
    }
}
