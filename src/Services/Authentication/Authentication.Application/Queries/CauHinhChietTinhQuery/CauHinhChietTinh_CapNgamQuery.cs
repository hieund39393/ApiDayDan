using Authentication.Application.Model.CauHinhChietTinh;
using Authentication.Infrastructure.AggregatesModel.MenuAggregate;
using Authentication.Infrastructure.Migrations;
using Authentication.Infrastructure.Repositories;
using EVN.Core.Extensions;
using EVN.Core.Helpers;
using EVN.Core.Models;
using EVN.Core.SeedWork;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using System;
using static EVN.Core.Common.AppEnum;

namespace Authentication.Application.Queries.CauHinhChietTinh_CapNgamQuery
{
    public interface ICauHinhChietTinh_CapNgamQuery // tạo interface (Quy tắc : có chữ I ở đầu để biết nó là interface)
    {
        Task<PagingResultSP<CauHinhChietTinhResponse>> GetList(CauHinhChietTinhRequest request); // lấy danh sách có phân trang và tìm kiếm

        List<SelectItem> PhanLoai();
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
                .GroupBy(x => x.IdCongViec)
                .Select(x => new CauHinhChietTinhResponse()
                {
                    IdCongViec = x.Key,
                    TenCongViec = x.First().DM_CongViec.TenCongViec
                }).AsSplitQuery().AsNoTracking();

            var totalRow = query.Count(); // tổng số lượng
            var queryPaging = query.Skip((request.PageIndex - 1) * request.PageSize).Take(request.PageSize); // phân trang
            return await PagingResultSP<CauHinhChietTinhResponse>.CreateAsyncLinq(queryPaging, totalRow, request.PageIndex, request.PageSize);
        }

        public List<SelectItem> PhanLoai()
        {
            var data = new List<SelectItem>();
            data.Add(new SelectItem
            {
                Name = "Vật liệu (ĐM 4970)",
                Value = "1"
            });
            data.Add(new SelectItem
            {
                Name = "VL-NC-MTC theo ĐM 4970",
                Value = "2"
            });
            data.Add(new SelectItem
            {
                Name = "VL_NC_MTC theo TT10/2019",
                Value = "3"
            });
            data.Add(new SelectItem
            {
                Name = "VL_NC_MTC theo 22/2020/QĐ-UBND",
                Value = "4"
            });
            return data;
        }
    }
}
