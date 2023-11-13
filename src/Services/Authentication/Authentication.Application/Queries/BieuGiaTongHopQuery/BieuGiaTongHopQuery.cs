using Authentication.Application.Model.BieuGiaTongHop;
using Authentication.Infrastructure.Repositories;
using EVN.Core.Common;
using EVN.Core.Exceptions;
using EVN.Core.Extensions;
using EVN.Core.Interfaces.Offices;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using SendGrid;
using System.Globalization;
using System.Net.WebSockets;
using System.Text;
using static EVN.Core.Common.AppEnum;

namespace Authentication.Application.Queries.BieuGiaTongHopQuery
{
    public interface IBieuGiaTongHopQuery
    {
        Task<List<BieuGiaTongHopResponse>> GetList(BieuGiaTongHopRequest request);
        Task<byte[]> XuatExcel(BieuGiaTongHopRequest request);
        Task<object> ChiTietPDF(ChiTietPDFRequest request);

        Task<byte[]> BaoCaoExcel(ChiTietPDFRequest request);
        Task<List<GetBaoCaoResponse>> GetBaoCao(ChiTietPDFRequest request);

        Task<object> GetDuLieuDonGia(int Vung);

        Task<object> GetVanBan(GetVanBanRequest request);
    }
    public class BieuGiaTongHopQuery : IBieuGiaTongHopQuery
    {
        private readonly IUnitOfWork _unitOfWork;
        public BieuGiaTongHopQuery(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<byte[]> BaoCaoExcel(ChiTietPDFRequest request)
        {
            var query = await _unitOfWork.BieuGiaTongHopRepository.GetQuery(x => x.Nam == request.Nam && x.Quy == request.Quy)
                .Include(x => x.DM_BieuGia).ThenInclude(x => x.DM_LoaiBieuGia).ThenInclude(x => x.DM_KhuVuc)
                .Select(x => new
                {
                    IdBieuGia = x.IdBieuGia,
                    TenBieuGia = x.DM_BieuGia.TenBieuGia,
                    IdLoaiBieuGia = x.DM_BieuGia.idLoaiBieuGia,
                    PhanLoaiBieuGia = x.DM_BieuGia.DM_LoaiBieuGia.MaLoaiBieuGia,
                    IdKhuVuc = x.DM_BieuGia.DM_LoaiBieuGia.IdKhuVuc,
                    TenKhuVuc = x.DM_BieuGia.DM_LoaiBieuGia.DM_KhuVuc.TenKhuVuc,
                    DonGia = x.DonGia,
                    DonGia2 = x.DonGia2,
                    DonGia3 = x.DonGia3,
                    TinhTrang = x.TinhTrang
                }).AsNoTracking()
                .ToListAsync();

            var phanLoai = query.Where(x => x.PhanLoaiBieuGia == request.PhanLoaiBieuGia.ToString()).ToList();

            var groupBy = phanLoai.GroupBy(x => x.IdKhuVuc).Select(x => new { KhuVuc = x.Key, ListBieuGia = x.ToList() }).OrderBy(x => x.KhuVuc).ToList();

            var response = new List<CSKHResponse>();
            foreach (var item in groupBy)
            {
                var data = new CSKHResponse();
                data.TenKhuVuc = item.ListBieuGia.First().TenKhuVuc;
                data.ListBieuGiaChiTiet = new List<BGTHChiTiet>();
                int i = 1;
                foreach (var bieuGia in item.ListBieuGia)
                {
                    data.ListBieuGiaChiTiet.Add(new BGTHChiTiet
                    {
                        Stt = i,
                        TenBieuGia = bieuGia.TenBieuGia,
                        DonVi = "m",
                        DonGiaCot1 = bieuGia.DonGia.ToString(),
                        DonGiaCot2 = bieuGia.DonGia2.ToString(),
                        DonGiaCot3 = bieuGia.DonGia3.ToString()
                    });
                    i++;
                }
                response.Add(data);
            }

            var templatePath = RootPathConfig.TemplatePath.GetTemplate + "BaoCaoCapTrenKhong.xlsx";
            var excelPackage = new ExcelPackage(new FileInfo(templatePath), true);
            var workbook = excelPackage.Workbook;

            var sheet1 = workbook.Worksheets["Sheet1"];
            var currentRow = 4;
            if (response.Any())
            {
                foreach (var model in response)
                {
                    sheet1.Cells[$"A{currentRow}"].Value = model.TenKhuVuc;
                    sheet1.Cells[$"A{currentRow}:F{currentRow}"].Merge = true;
                    sheet1.Cells[$"A{currentRow}:F{currentRow}"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                    sheet1.Cells[$"A{currentRow}:F{currentRow}"].Style.Font.Bold = true;
                    currentRow++;
                    foreach (var item in model.ListBieuGiaChiTiet)
                    {
                        sheet1.Cells[$"A{currentRow}"].Value = item.Stt;
                        sheet1.Cells[$"B{currentRow}"].Value = item.TenBieuGia;
                        sheet1.Cells[$"C{currentRow}"].Value = item.DonVi;
                        sheet1.Cells[$"D{currentRow}"].Value = item.DonGiaCot1;
                        sheet1.Cells[$"E{currentRow}"].Value = item.DonGiaCot2;
                        sheet1.Cells[$"F{currentRow}"].Value = item.DonGiaCot3;
                        currentRow++;
                    }
                    //sheet1.InsertRow(currentRow, 1);
                }

                var endRow = currentRow - 1;
                sheet1.Cells[$"A3:F{endRow}"].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                sheet1.Cells[$"A3:F{endRow}"].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                sheet1.Cells[$"A3:F{endRow}"].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                sheet1.Cells[$"A3:F{endRow}"].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            }
            return excelPackage.GetAsByteArray();
        }

        public async Task<object> ChiTietPDF(ChiTietPDFRequest request)
        {
            var query = await _unitOfWork.BieuGiaTongHopRepository.GetQuery(x => x.Nam == request.Nam && x.Quy == request.Quy)
                .Include(x => x.DM_BieuGia).ThenInclude(x => x.DM_LoaiBieuGia).ThenInclude(x => x.DM_KhuVuc)
                .Select(x => new
                {
                    IdBieuGia = x.IdBieuGia,
                    TenBieuGia = x.DM_BieuGia.TenBieuGia,
                    IdLoaiBieuGia = x.DM_BieuGia.idLoaiBieuGia,
                    PhanLoaiBieuGia = x.DM_BieuGia.DM_LoaiBieuGia.MaLoaiBieuGia,
                    IdKhuVuc = x.DM_BieuGia.DM_LoaiBieuGia.IdKhuVuc,
                    TenKhuVuc = x.DM_BieuGia.DM_LoaiBieuGia.DM_KhuVuc.TenKhuVuc,
                    DonGia = x.DonGia,
                    DonGia2 = x.DonGia2,
                    DonGia3 = x.DonGia3,
                    TinhTrang = x.TinhTrang
                }).AsNoTracking()
                .ToListAsync();

            var phanLoai = query.Where(x => x.PhanLoaiBieuGia == request.PhanLoaiBieuGia.ToString()).ToList();

            var groupBy = phanLoai.GroupBy(x => x.IdKhuVuc).Select(x => new { KhuVuc = x.Key, ListBieuGia = x.ToList() }).ToList();

            var response = new List<CSKHResponse>();
            foreach (var item in groupBy)
            {
                var data = new CSKHResponse();
                data.TenKhuVuc = item.ListBieuGia.First().TenKhuVuc;
                data.ListBieuGiaChiTiet = new List<BGTHChiTiet>();
                int i = 1;
                foreach (var bieuGia in item.ListBieuGia)
                {
                    data.ListBieuGiaChiTiet.Add(new BGTHChiTiet
                    {
                        Stt = i,
                        TenBieuGia = bieuGia.TenBieuGia,
                        DonVi = "m",
                        DonGiaCot1 = bieuGia.DonGia.ToString("N0", CultureInfo.GetCultureInfo("en-US")),
                        DonGiaCot2 = bieuGia.DonGia2.ToString("N0", CultureInfo.GetCultureInfo("en-US")),
                        DonGiaCot3 = bieuGia.DonGia3.ToString("N0", CultureInfo.GetCultureInfo("en-US"))
                    }); ;
                    i++;
                }
                response.Add(data);
            }
            return response;

        }

        public async Task<List<GetBaoCaoResponse>> GetBaoCao(ChiTietPDFRequest request)
        {
            var query = await _unitOfWork.BieuGiaTongHopRepository.GetQuery(x => x.Nam == request.Nam && x.Quy == request.Quy)
                .Include(x => x.DM_BieuGia).ThenInclude(x => x.DM_LoaiBieuGia).ThenInclude(x => x.DM_KhuVuc)
                .Select(x => new
                {
                    IdBieuGia = x.IdBieuGia,
                    TenBieuGia = x.DM_BieuGia.TenBieuGia,
                    IdLoaiBieuGia = x.DM_BieuGia.idLoaiBieuGia,
                    PhanLoaiBieuGia = x.DM_BieuGia.DM_LoaiBieuGia.MaLoaiBieuGia,
                    IdKhuVuc = x.DM_BieuGia.DM_LoaiBieuGia.IdKhuVuc,
                    TenKhuVuc = x.DM_BieuGia.DM_LoaiBieuGia.DM_KhuVuc.TenKhuVuc,
                    DonGia = x.DonGia,
                    DonGia2 = x.DonGia2,
                    DonGia3 = x.DonGia3,
                    ThuTuHienThi = x.DM_BieuGia.ThuTuHienThi,
                    TinhTrang = x.TinhTrang
                }).AsNoTracking()
                .ToListAsync();

            var phanLoai = query.Where(x => x.PhanLoaiBieuGia == request.PhanLoaiBieuGia.ToString()).ToList();

            var groupBy = phanLoai.GroupBy(x => x.IdKhuVuc).Select(x => new { KhuVuc = x.Key, ListBieuGia = x.ToList() }).OrderBy(x => x.KhuVuc).ToList();

            var response = new List<CSKHResponse>();
            foreach (var item in groupBy)
            {
                var data = new CSKHResponse();
                data.TenKhuVuc = item.ListBieuGia.First().TenKhuVuc;
                data.ListBieuGiaChiTiet = new List<BGTHChiTiet>();
                int i = 1;
                foreach (var bieuGia in item.ListBieuGia.OrderBy(x => x.ThuTuHienThi))
                {
                    data.ListBieuGiaChiTiet.Add(new BGTHChiTiet
                    {
                        Stt = i,
                        TenBieuGia = bieuGia.TenBieuGia,
                        DonVi = "m",
                        DonGiaCot1 = bieuGia.DonGia.ToString("N0", CultureInfo.GetCultureInfo("en-US")),
                        DonGiaCot2 = bieuGia.DonGia2.ToString("N0", CultureInfo.GetCultureInfo("en-US")),
                        DonGiaCot3 = bieuGia.DonGia3.ToString("N0", CultureInfo.GetCultureInfo("en-US"))
                    });
                    i++;
                }
                response.Add(data);
            }

            var templatePath = RootPathConfig.TemplatePath.GetTemplate + "BaoCaoCapTrenKhong.xlsx";
            var excelPackage = new ExcelPackage(new FileInfo(templatePath), true);
            var workbook = excelPackage.Workbook;

            var sheet1 = workbook.Worksheets["Sheet1"];
            var currentRow = 4;

            var listResponse = new List<GetBaoCaoResponse>();

            if (response.Any())
            {
                foreach (var model in response)
                {
                    listResponse.Add(new GetBaoCaoResponse
                    {
                        ChiPhi = model.TenKhuVuc,
                    });

                    foreach (var item in model.ListBieuGiaChiTiet)
                    {
                        listResponse.Add(new GetBaoCaoResponse
                        {
                            Stt = item.Stt,
                            ChiPhi = item.TenBieuGia,
                            DonVi = item.DonVi,
                            Ctdl1 = item.DonGiaCot1,
                            Ctdl2 = item.DonGiaCot2,
                            Ctdl3 = item.DonGiaCot3
                        });
                    }
                }
            }
            return listResponse;
        }

        public async Task<object> GetDuLieuDonGia(int Vung)
        {
            //Doan nay if do ben KD bi nguoc mã vung: 
            int VungDonGia = 1;
            if (Vung == 2)
            {
                VungDonGia = 3;
            }
            else if (Vung == 3)
            {
                VungDonGia = 2;
            }
            else
            {
                VungDonGia = Vung;
            }

            var data = await _unitOfWork.BieuGiaTongHopRepository.GetQuery(x => x.TinhTrang == 4 && x.DM_BieuGia.DM_LoaiBieuGia.DM_KhuVuc.GhiChu == VungDonGia.ToString())
                .Include(x => x.DM_BieuGia).ThenInclude(x => x.BieuGiaCongViec).ThenInclude(z => z.DM_CongViec)
                //.Where(x => x.DM_BieuGia.BieuGiaCongViec.Any(z => z.CongViecChinh && z.DM_CongViec.MaCongViec == LoaiCap))
                .Include(x => x.DM_BieuGia).ThenInclude(x => x.DM_LoaiBieuGia)
                .GroupBy(x => new { x.IdBieuGia, x.Nam, x.Quy }).
                 Select(x => x.OrderByDescending(x => x.Nam).ThenByDescending(y => y.Quy).First())
                .ToListAsync();

            var listApiResult = new List<ApiDonGiaVatLieuResponse>();

            foreach (var item in data)
            {
                var idCongViecLapDat = item.DM_BieuGia.BieuGiaCongViec.Where(x => x.DM_CongViec.TenCongViec.ToLower().Contains("lắp đặt cáp")).FirstOrDefault()?.IdCongViec;

                var listDonGiaChietTinh = await _unitOfWork.DonGiaChietTinhRepository.GetQuery(x => x.IdCongViec == idCongViecLapDat && x.VungKhuVuc == VungDonGia)
                    .OrderByDescending(x => x.CreatedDate)
                    .AsNoTracking().FirstOrDefaultAsync();

                var maVatTu = item.DM_BieuGia.BieuGiaCongViec.FirstOrDefault(x => x.CongViecChinh)?.DM_CongViec?.MaCongViec;

                var donGia = data.Where(x => x.DM_BieuGia.BieuGiaCongViec.Any(y => y.DM_CongViec.MaCongViec == maVatTu)
                                && x.DM_BieuGia.DM_LoaiBieuGia.MaLoaiBieuGia != "2").OrderBy(z => int.Parse(z.DM_BieuGia.DM_LoaiBieuGia.MaLoaiBieuGia)).ToArray();

                var apiResult = new ApiDonGiaVatLieuResponse();
                apiResult.MaVatTu = item.DM_BieuGia.BieuGiaCongViec.FirstOrDefault(x => x.CongViecChinh)?.DM_CongViec?.MaCongViec;
                apiResult.NgayHieuLuc = item.NgayHieuLuc?.ToString("yyyy-MM-dd");
                apiResult.DonGiaTronGoi = new ApiDonGia
                {
                    CapDien = donGia[0]?.DonGia.ToString(),
                    NangCongSuat = donGia[1]?.DonGia.ToString(),
                    DiDoi = donGia[2]?.DonGia.ToString(),
                };
                apiResult.DonGiaTuTucCapSau = new ApiDonGia
                {
                    CapDien = donGia[0]?.DonGia2.ToString(),
                    NangCongSuat = donGia[1]?.DonGia2.ToString(),
                    DiDoi = donGia[2]?.DonGia2.ToString(),
                };
                apiResult.DonGiaTuTucCapVaVatTu = new ApiDonGia
                {
                    CapDien = donGia[0]?.DonGia3.ToString(),
                    NangCongSuat = donGia[1]?.DonGia3.ToString(),
                    DiDoi = donGia[2]?.DonGia3.ToString(),
                };
                apiResult.HinhThucThiCong = "";
                apiResult.DonGiaNhanCong = (listDonGiaChietTinh.DonGiaNhanCong / 100).ToString();
                listApiResult.Add(apiResult);
            }
            return listApiResult;
        }

        public async Task<List<BieuGiaTongHopResponse>> GetList(BieuGiaTongHopRequest request)
        {
            var position = TokenExtensions.GetPosition();
            if (string.IsNullOrEmpty(position)) throw new EvnException("Người dùng có chức vụ không đúng");

            var loaiBieuGia = await _unitOfWork.DM_LoaiBieuGiaRepository.GetQuery().AsNoTracking().ToListAsync();
            var query = await _unitOfWork.BieuGiaTongHopRepository.GetQuery(x => x.Nam == request.Nam && x.Quy == request.Quy)
                .Include(x => x.DM_BieuGia).ThenInclude(x => x.DM_LoaiBieuGia)
                .Select(x => new
                {
                    IdBieuGia = x.IdBieuGia,
                    TenBieuGia = x.DM_BieuGia.TenBieuGia,
                    IdLoaiBieuGia = x.DM_BieuGia.idLoaiBieuGia,
                    IdKhuVuc = x.DM_BieuGia.DM_LoaiBieuGia.IdKhuVuc,
                    DonGia = request.PhanLoai == 1 ? x.DonGia : (request.PhanLoai == 2 ? x.DonGia2 : x.DonGia3),
                    TinhTrang = x.TinhTrang
                }).AsNoTracking()
                .ToListAsync();

            var groupBy = query.GroupBy(x => x.TenBieuGia).Select(x => new { name = x.Key, listBG = x.ToList() }).ToList();

            var listResponse = new List<BieuGiaTongHopResponse>();
            foreach (var r in groupBy)
            {
                var item = new BieuGiaTongHopResponse();
                item.TenBieuGia = r.name;
                item.DonVi = "m";
                var listData = new List<string>();
                foreach (var list in loaiBieuGia)
                {
                    var value = r.listBG.Where(x => x.IdKhuVuc == list.IdKhuVuc && x.IdLoaiBieuGia == list.Id).FirstOrDefault()?.DonGia.ToString() ?? "";
                    listData.Add(value);
                }
                item.ListData = listData;
                item.TinhTrang = r.listBG.FirstOrDefault()?.TinhTrang ?? null;
                if (int.Parse(position) == (int)PositionEnum.ChuyenVienB08 && item.TinhTrang >= 0)
                {
                    listResponse.Add(item);
                }

                if (int.Parse(position) == (int)PositionEnum.LanhDaoB08)
                {
                    if (item.TinhTrang == 0)
                    {
                        throw new EvnException($"Biểu giá của quý {request.Quy} năm {request.Nam} chưa được chuyên viên B08 gửi lên");
                    }
                    listResponse.Add(item);
                }

                else if (int.Parse(position) == (int)PositionEnum.ChuyenVienB09)
                {
                    if (item.TinhTrang <= 1)
                    {
                        throw new EvnException($"Biểu giá của quý {request.Quy} năm {request.Nam} chưa được lãnh đạo B08 gửi lên");
                    }

                    listResponse.Add(item);
                }
                else if (int.Parse(position) == (int)PositionEnum.LanhDaoB09)
                {
                    if (item.TinhTrang <= 2)
                    {
                        throw new EvnException($"Biểu giá của quý {request.Quy} năm {request.Nam} chưa được chuyên viên B09 gửi lên");
                    }
                    listResponse.Add(item);
                }
            }

            return listResponse;
        }

        public async Task<object> GetVanBan(GetVanBanRequest request)
        {
            var data = await _unitOfWork.BieuGiaTongHopChiTietRepository.GetQuery(x => x.Quy == request.Quy && x.Nam == request.Nam).AsNoTracking()
                .Select(x => new
                {
                    VanBan = x.VanBan,
                    GhiChu = x.GhiChu,
                    TrangThai = x.TrangThai,
                    NgayTao = x.CreatedDate.ToString("dd/MM/yyyy")
                }).ToListAsync();
            return data;
        }

        public async Task<byte[]> XuatExcel(BieuGiaTongHopRequest request)
        {
            var position = TokenExtensions.GetPosition();
            if (string.IsNullOrEmpty(position)) throw new EvnException("Người dùng có chức vụ không đúng");

            var loaiBieuGia = await _unitOfWork.DM_LoaiBieuGiaRepository.GetQuery().AsNoTracking().ToListAsync();
            var query = await _unitOfWork.BieuGiaTongHopRepository.GetQuery(x => x.Nam == request.Nam && x.Quy == request.Quy)
                .Include(x => x.DM_BieuGia).ThenInclude(x => x.DM_LoaiBieuGia)
                .Select(x => new
                {
                    IdBieuGia = x.IdBieuGia,
                    TenBieuGia = x.DM_BieuGia.TenBieuGia,
                    IdLoaiBieuGia = x.DM_BieuGia.idLoaiBieuGia,
                    IdKhuVuc = x.DM_BieuGia.DM_LoaiBieuGia.IdKhuVuc,
                    DonGia = request.PhanLoai == 1 ? x.DonGia : (request.PhanLoai == 2 ? x.DonGia2 : x.DonGia3),
                    TinhTrang = x.TinhTrang
                }).AsNoTracking()
                .ToListAsync();

            var groupBy = query.GroupBy(x => x.TenBieuGia).Select(x => new { name = x.Key, listBG = x.ToList() }).ToList();

            var listResponse = new List<BieuGiaTongHopResponse>();
            foreach (var r in groupBy)
            {
                var item = new BieuGiaTongHopResponse();
                item.TenBieuGia = r.name;
                item.DonVi = "m";
                var listData = new List<string>();
                foreach (var list in loaiBieuGia)
                {
                    var value = r.listBG.Where(x => x.IdKhuVuc == list.IdKhuVuc && x.IdLoaiBieuGia == list.Id).FirstOrDefault()?.DonGia.ToString("N0", CultureInfo.GetCultureInfo("en-US")) ?? "0";
                    listData.Add(value);
                }
                item.ListData = listData;
                item.TinhTrang = r.listBG.FirstOrDefault()?.TinhTrang ?? null;
                if (int.Parse(position) == (int)PositionEnum.ChuyenVienB08 && item.TinhTrang >= 0)
                {
                    listResponse.Add(item);
                }

                if (int.Parse(position) == (int)PositionEnum.LanhDaoB08)
                {
                    if (item.TinhTrang == 0)
                    {
                        throw new EvnException($"Biểu giá của quý {request.Quy} năm {request.Nam} chưa được chuyên viên B08 gửi lên");
                    }
                    listResponse.Add(item);
                }

                else if (int.Parse(position) == (int)PositionEnum.ChuyenVienB09)
                {
                    if (item.TinhTrang <= 1)
                    {
                        throw new EvnException($"Biểu giá của quý {request.Quy} năm {request.Nam} chưa được lãnh đạo B08 gửi lên");
                    }

                    listResponse.Add(item);
                }
                else if (int.Parse(position) == (int)PositionEnum.LanhDaoB09)
                {
                    if (item.TinhTrang <= 2)
                    {
                        throw new EvnException($"Biểu giá của quý {request.Quy} năm {request.Nam} chưa được chuyên viên B09 gửi lên");
                    }
                    listResponse.Add(item);
                }
            }

            var templatePath = RootPathConfig.TemplatePath.GetTemplate + "ChiTietBieuGia.xlsx";
            var excelPackage = new ExcelPackage(new FileInfo(templatePath), true);
            var workbook = excelPackage.Workbook;

            var sheet1 = workbook.Worksheets["Sheet1"];
            var currentRow = 4;
            if (listResponse.Any())
            {
                int stt = 1;
                foreach (var model in listResponse)
                {
                    sheet1.Cells[$"A{currentRow}"].Value = stt.ToString();
                    sheet1.Cells[$"B{currentRow}"].Value = model.TenBieuGia;
                    sheet1.Cells[$"C{currentRow}"].Value = model.DonVi;
                    sheet1.Cells[$"D{currentRow}"].Value = model.ListData[0] ?? "";
                    sheet1.Cells[$"E{currentRow}"].Value = model.ListData[1] ?? "";
                    sheet1.Cells[$"F{currentRow}"].Value = model.ListData[2] ?? "";
                    sheet1.Cells[$"G{currentRow}"].Value = model.ListData[3] ?? "";
                    sheet1.Cells[$"H{currentRow}"].Value = model.ListData[4] ?? "";
                    sheet1.Cells[$"I{currentRow}"].Value = model.ListData[5] ?? "";
                    sheet1.Cells[$"J{currentRow}"].Value = model.ListData[6] ?? "";
                    sheet1.Cells[$"K{currentRow}"].Value = model.ListData[7] ?? "";
                    sheet1.Cells[$"L{currentRow}"].Value = model.ListData[8] ?? "";
                    sheet1.Cells[$"M{currentRow}"].Value = model.ListData[9] ?? "";
                    sheet1.Cells[$"N{currentRow}"].Value = model.ListData[10] ?? "";
                    sheet1.Cells[$"O{currentRow}"].Value = model.ListData[11] ?? "";
                    currentRow++;
                    stt++;

                }

                var endRow = currentRow - 1;
                sheet1.Cells[$"A3:O{endRow}"].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                sheet1.Cells[$"A3:O{endRow}"].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                sheet1.Cells[$"A3:O{endRow}"].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                sheet1.Cells[$"A3:O{endRow}"].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            }
            return excelPackage.GetAsByteArray();

        }
    }
}
