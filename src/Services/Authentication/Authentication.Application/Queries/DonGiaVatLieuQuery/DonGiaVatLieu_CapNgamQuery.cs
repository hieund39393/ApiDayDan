using Authentication.Application.Model.DonGiaVatLieu;
using Authentication.Application.Queries.CommonQuery;
using Authentication.Infrastructure.Repositories;
using EVN.Core.Common;
using EVN.Core.Models;
using EVN.Core.SeedWork;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml.Style;
using OfficeOpenXml;
using Microsoft.AspNetCore.Http;
using Authentication.Infrastructure.AggregatesModel.DonGiaVatLieuAggregate;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Math;

namespace Authentication.Application.Queries.DonGiaVatLieu_CapNgamQuery
{

    public interface IDonGiaVatLieu_CapNgamQuery // tạo interface (Quy tắc : có chữ I ở đầu để biết nó là interface)
    {

        Task<List<DonGiaVatLieuResponse>> GetList(DonGiaVatLieuRequest request); // lấy danh sách có phân trang và tìm kiếm
        Task<List<SelectItem>> GetAll(); // lấy Tất cả danh sách trả về tên và value

        Task<byte[]> Export();
        Task<bool> Import(IFormFile file);
    }
    public class DonGiaVatLieu_CapNgamQuery : IDonGiaVatLieu_CapNgamQuery // kế thừa interface vừa tạo
    {
        private readonly ICommonQuery _commonQuery;
        private readonly IUnitOfWork _unitOfWork; // khai báo 
        public DonGiaVatLieu_CapNgamQuery(IUnitOfWork unitOfWork, ICommonQuery commonQuery)
        {
            _unitOfWork = unitOfWork;
            _commonQuery = commonQuery;
        }

        public async Task<byte[]> Export()
        {
            var data = await _unitOfWork.DonGiaVatLieu_CapNgamRepository.GetQuery().Include(x => x.DM_VatLieu_CapNgam)
                .GroupBy(x => x.IdVatLieu).Select(x => x.OrderByDescending(x => x.CreatedDate).First())
                .Select(x => new
                {
                    IdVatLieu = x.IdVatLieu,
                    MaVatLieu = x.DM_VatLieu_CapNgam.MaVatLieu,
                    TenVatLieu = x.DM_VatLieu_CapNgam.TenVatLieu,
                    VanBan = x.VanBan,
                    DonGia = x.DonGia,
                })
                .AsNoTracking().ToListAsync();

            var templatePath = RootPathConfig.TemplatePath.GetTemplate + "DonGiaVatLieu.xlsx";
            var excelPackage = new ExcelPackage(new FileInfo(templatePath), true);
            var workbook = excelPackage.Workbook;
            var sheet1 = workbook.Worksheets["Sheet1"];
            var currentRow = 2;

            if (data.Any())
            {
                int stt = 1;
                foreach (var model in data)
                {
                    sheet1.Cells[$"A{currentRow}"].Value = stt;
                    sheet1.Cells[$"B{currentRow}"].Value = model.MaVatLieu;
                    sheet1.Cells[$"C{currentRow}"].Value = model.TenVatLieu;
                    sheet1.Cells[$"D{currentRow}"].Value = model.IdVatLieu;
                    sheet1.Cells[$"E{currentRow}"].Value = model.VanBan;
                    sheet1.Cells[$"F{currentRow}"].Value = model.DonGia;
                    currentRow++;
                    stt++;
                }

                var endRow = currentRow - 1;
                sheet1.Cells[$"A3:F{endRow}"].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                sheet1.Cells[$"A3:F{endRow}"].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                sheet1.Cells[$"A3:F{endRow}"].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                sheet1.Cells[$"A3:F{endRow}"].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            }
            return excelPackage.GetAsByteArray();
        }

        // lấy Tất cả danh sách trả về tên và value thường dùng cho combobox
        public async Task<List<SelectItem>> GetAll()
        {
            var query = await _unitOfWork.DonGiaVatLieu_CapNgamRepository.GetQuery().Select(x => new SelectItem
            {
                Name = x.VanBan,
                Value = x.Id.ToString(),
            }).AsNoTracking().ToListAsync();
            return query;
        }

        // lấy dữ liệu phân trang, tìm kiếm , số lượng
        public async Task<List<DonGiaVatLieuResponse>> GetList(DonGiaVatLieuRequest request)
        {
            //Tạo câu query
            var query = _unitOfWork.DonGiaVatLieu_CapNgamRepository.GetQuery()
                .Include(x => x.DM_VatLieu_CapNgam)
                .Select(x => new DonGiaVatLieuResponse()
                {
                    Id = x.Id,
                    IdVatLieu = x.IdVatLieu,
                    TenVatLieu = x.DM_VatLieu_CapNgam.TenVatLieu,
                    DonViTinh = x.DM_VatLieu_CapNgam.DonViTinh,
                    VanBan = x.VanBan,
                    DonGia = x.DonGia,
                    DinhMuc = x.DinhMuc,
                    VungKhuVuc = x.VungKhuVuc.ToString(),
                    NgayTao = x.CreatedDate.ToString("dd/MM/yyyy"),
                });// select dữ liệu
            if (request.VungKhuVuc != 0)
            {
                query = query.Where(x => x.VungKhuVuc == request.VungKhuVuc.ToString());
            }
            if (!string.IsNullOrEmpty(request.SearchTerm))
            {
                query = query.Where(x => x.TenVatLieu.Contains(request.SearchTerm.ToLower().Trim()) || x.VanBan.Contains(request.SearchTerm.ToLower().Trim()));
            }
            return await query.OrderBy(x => x.IdVatLieu).ToListAsync();
        }

        public async Task<bool> Import(IFormFile file)
        {
            var listData = new List<DonGiaVatLieu_CapNgam>();
            using (var stream = new MemoryStream())
            {
                file.CopyTo(stream);

                using (var package = new ExcelPackage(stream))
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets["Sheet1"]; // Lấy ra worksheet đầu tiên

                    int rowCount = worksheet.Dimension.Rows;

                    for (int row = 2; row <= rowCount; row++)
                    {
                        var data = new DonGiaVatLieu_CapNgam();
                        data.IdVatLieu = Guid.Parse(worksheet.Cells[$"D{row}"].Value.ToString());
                        data.VanBan = worksheet.Cells[$"E{row}"].Value.ToString();
                        data.DonGia = string.IsNullOrEmpty(worksheet.Cells[$"F{row}"].Value.ToString()) ? 0 : decimal.Parse(worksheet.Cells[$"F{row}"].Value.ToString());
                        listData.Add(data);
                    }
                }
            }
            _unitOfWork.DonGiaVatLieu_CapNgamRepository.AddRange(listData);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}
