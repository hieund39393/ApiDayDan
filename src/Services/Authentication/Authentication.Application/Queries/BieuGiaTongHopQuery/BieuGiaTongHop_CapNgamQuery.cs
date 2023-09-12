using Authentication.Application.Model.BieuGiaTongHop;
using Authentication.Infrastructure.AggregatesModel.PositionAggregate;
using Authentication.Infrastructure.Repositories;
using EVN.Core.Common;
using EVN.Core.Exceptions;
using EVN.Core.Extensions;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml.Style;
using OfficeOpenXml;
using System.Text;
using static EVN.Core.Common.AppEnum;
using EVN.Core.Helpers;
using System.Collections.Generic;
using System.Globalization;

namespace Authentication.Application.Queries.BieuGiaTongHop_CapNgamQuery
{
    public interface IBieuGiaTongHop_CapNgamQuery
    {
        Task<List<BieuGiaTongHopResponse>> GetList(BieuGiaTongHopRequest request);
        Task<byte[]> XuatExcel(BieuGiaTongHopRequest request);
        Task<object> ChiTietPDF(ChiTietPDFRequest request);
        Task<byte[]> BaoCaoExcel(ChiTietPDFRequest request);
        Task<object> GetDuLieuDonGia(int Vung);
        Task<object> GetVanBan(GetVanBanRequest request);
        Task<List<GetBaoCaoResponse>> GetBaoCao(ChiTietPDFRequest request);
    }
    public class BieuGiaTongHop_CapNgamQuery : IBieuGiaTongHop_CapNgamQuery
    {
        private readonly IUnitOfWork _unitOfWork;
        public BieuGiaTongHop_CapNgamQuery(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<GetBaoCaoResponse>> GetBaoCao(ChiTietPDFRequest request)
        {
            var query = await _unitOfWork.BieuGiaTongHop_CapNgamRepository.GetQuery(x => x.Nam == request.Nam && x.Quy == request.Quy)
                .Include(x => x.DM_BieuGia_CapNgam).ThenInclude(x => x.DM_LoaiBieuGia_CapNgam).ThenInclude(x => x.DM_KhuVuc)
                .Select(x => new BGTHReponse
                {
                    IdBieuGia = x.IdBieuGia,
                    TenBieuGia = x.DM_BieuGia_CapNgam.TenBieuGia,
                    IdLoaiBieuGia = x.DM_BieuGia_CapNgam.idLoaiBieuGia,
                    PhanLoaiBieuGia = x.DM_BieuGia_CapNgam.DM_LoaiBieuGia_CapNgam.Code,
                    IdKhuVuc = x.DM_BieuGia_CapNgam.DM_LoaiBieuGia_CapNgam.IdKhuVuc,
                    TenKhuVuc = x.DM_BieuGia_CapNgam.DM_LoaiBieuGia_CapNgam.DM_KhuVuc.GhiChu,
                    ThuTuHienThi = x.DM_BieuGia_CapNgam.ThuTuHienThi,
                    DonGia = x.DonGia,
                    DonGia2 = x.DonGia2,
                    DonGia3 = x.DonGia3,
                    TinhTrang = x.TinhTrang
                }).AsNoTracking()
                .ToListAsync();


            foreach (var kv in query)
            {
                if (int.Parse(kv.TenKhuVuc) <= 3)
                {
                    kv.TenKhuVuc = "Vùng I - Khu Vực 1";
                }
                else if (int.Parse(kv.TenKhuVuc) > 3 && int.Parse(kv.TenKhuVuc) <= 6)
                {
                    kv.TenKhuVuc = "Vùng I - Khu Vực 2";
                }
                else
                {
                    kv.TenKhuVuc = "Vùng II";
                }
            }

            var groupBy = query.GroupBy(x => x.TenKhuVuc).Select(x => new { KhuVuc = x.Key, ListBieuGia = x.ToList() }).OrderBy(x => x.KhuVuc).ToList();

            var response = new List<CSKHResponse>();
            foreach (var item in groupBy)
            {
                var data = new CSKHResponse();
                data.TenKhuVuc = item.ListBieuGia.First().TenKhuVuc;
                data.ListBieuGiaChiTiet = new List<BGTHChiTiet>();
                int i = 1;

                var nhom = 1;
                foreach (var bieuGia in item.ListBieuGia.OrderBy(x => int.Parse(x.PhanLoaiBieuGia)).ThenBy(x => x.ThuTuHienThi))
                {
                    if (bieuGia.PhanLoaiBieuGia == nhom.ToString())
                    {

                        var tenNhom = new BGTHChiTiet();
                        if (nhom == 1) tenNhom.TenBieuGia = "1.1 Nhóm 1";
                        else if (nhom == 4) tenNhom.TenBieuGia = "1.2 Nhóm 2";
                        else if (nhom == 7) tenNhom.TenBieuGia = "1.3 Nhóm 3";
                        else if (nhom == 10) tenNhom.TenBieuGia = "2.1 Nhóm 1";
                        else if (nhom == 13) tenNhom.TenBieuGia = "2.2 Nhóm 2";
                        else if (nhom == 16) tenNhom.TenBieuGia = "2.3 Nhóm 3";
                        else if (nhom == 18) tenNhom.TenBieuGia = "3.1 Nhóm 1";
                        else if (nhom == 21) tenNhom.TenBieuGia = "3.2 Nhóm 2";
                        else if (nhom == 24) tenNhom.TenBieuGia = "3.3 Nhóm 3";

                        data.ListBieuGiaChiTiet.Add(tenNhom);
                        nhom = nhom + 3;
                        i = 1;
                    }

                    data.ListBieuGiaChiTiet.Add(new BGTHChiTiet
                    {
                        Stt = i,
                        TenBieuGia = bieuGia.TenBieuGia,
                        DonVi = "m",
                        DonGiaCot1 = bieuGia.DonGia?.ToString("N0", CultureInfo.GetCultureInfo("en-US")),
                        DonGiaCot2 = bieuGia.DonGia2?.ToString("N0", CultureInfo.GetCultureInfo("en-US")),
                        DonGiaCot3 = bieuGia.DonGia3?.ToString("N0", CultureInfo.GetCultureInfo("en-US"))
                    });
                    i++;
                }
                response.Add(data);
            }

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


        public async Task<object> GetVanBan(GetVanBanRequest request)
        {
            var data = await _unitOfWork.BieuGiaTongHopChiTiet_CapNgamRepository.GetQuery(x => x.Quy == request.Quy && x.Nam == request.Nam).AsNoTracking()
                .Select(x => new
                {
                    VanBan = x.VanBan,
                    GhiChu = x.GhiChu,
                    TrangThai = x.TrangThai,
                    NgayTao = x.CreatedDate.ToString("dd/MM/yyyy")
                }).ToListAsync();
            return data;
        }

        public async Task<byte[]> BaoCaoExcel(ChiTietPDFRequest request)
        {
            var query = await _unitOfWork.BieuGiaTongHop_CapNgamRepository.GetQuery(x => x.Nam == request.Nam && x.Quy == request.Quy)
                .Include(x => x.DM_BieuGia_CapNgam).ThenInclude(x => x.DM_LoaiBieuGia_CapNgam).ThenInclude(x => x.DM_KhuVuc)
                .Select(x => new BGTHReponse
                {
                    IdBieuGia = x.IdBieuGia,
                    TenBieuGia = x.DM_BieuGia_CapNgam.TenBieuGia,
                    IdLoaiBieuGia = x.DM_BieuGia_CapNgam.idLoaiBieuGia,
                    PhanLoaiBieuGia = x.DM_BieuGia_CapNgam.DM_LoaiBieuGia_CapNgam.Code,
                    IdKhuVuc = x.DM_BieuGia_CapNgam.DM_LoaiBieuGia_CapNgam.IdKhuVuc,
                    TenKhuVuc = x.DM_BieuGia_CapNgam.DM_LoaiBieuGia_CapNgam.DM_KhuVuc.GhiChu,
                    DonGia = x.DonGia,
                    DonGia2 = x.DonGia2,
                    DonGia3 = x.DonGia3,
                    TinhTrang = x.TinhTrang
                }).AsNoTracking()
                .ToListAsync();


            foreach (var kv in query)
            {
                if (int.Parse(kv.TenKhuVuc) <= 3)
                {
                    kv.TenKhuVuc = "Vùng I - Khu Vực 1";
                }
                else if (int.Parse(kv.TenKhuVuc) > 3 && int.Parse(kv.TenKhuVuc) <= 6)
                {
                    kv.TenKhuVuc = "Vùng I - Khu Vực 2";
                }
                else
                {
                    kv.TenKhuVuc = "Vùng II";
                }
            }

            var groupBy = query.GroupBy(x => x.TenKhuVuc).Select(x => new { KhuVuc = x.Key, ListBieuGia = x.ToList() }).OrderBy(x => x.KhuVuc).ToList();

            var response = new List<CSKHResponse>();
            foreach (var item in groupBy)
            {
                var data = new CSKHResponse();
                data.TenKhuVuc = item.ListBieuGia.First().TenKhuVuc;
                data.ListBieuGiaChiTiet = new List<BGTHChiTiet>();
                int i = 1;

                var nhom = 1;
                //foreach (var bieuGia in item.ListBieuGia.OrderBy(x => int.Parse(x.PhanLoaiBieuGia)))
                foreach (var bieuGia in item.ListBieuGia.OrderBy(x => int.Parse(x.PhanLoaiBieuGia)).ThenBy(x => x.ThuTuHienThi))
                {
                    if (bieuGia.PhanLoaiBieuGia == nhom.ToString())
                    {

                        var tenNhom = new BGTHChiTiet();
                        if (nhom == 1) tenNhom.TenBieuGia = "1.1 Nhóm 1";
                        else if (nhom == 4) tenNhom.TenBieuGia = "1.2 Nhóm 2";
                        else if (nhom == 7) tenNhom.TenBieuGia = "1.3 Nhóm 3";
                        else if (nhom == 10) tenNhom.TenBieuGia = "2.1 Nhóm 1";
                        else if (nhom == 13) tenNhom.TenBieuGia = "2.2 Nhóm 2";
                        else if (nhom == 16) tenNhom.TenBieuGia = "2.3 Nhóm 3";
                        else if (nhom == 18) tenNhom.TenBieuGia = "3.1 Nhóm 1";
                        else if (nhom == 21) tenNhom.TenBieuGia = "3.2 Nhóm 2";
                        else if (nhom == 24) tenNhom.TenBieuGia = "3.3 Nhóm 3";

                        data.ListBieuGiaChiTiet.Add(tenNhom);
                        nhom = nhom + 3;
                        i = 1;
                    }

                    data.ListBieuGiaChiTiet.Add(new BGTHChiTiet
                    {
                        Stt = i,
                        TenBieuGia = bieuGia.TenBieuGia,
                        DonVi = "m",
                        DonGiaCot1 = bieuGia.DonGia?.ToString("N0", CultureInfo.GetCultureInfo("en-US")),
                        DonGiaCot2 = bieuGia.DonGia2?.ToString("N0", CultureInfo.GetCultureInfo("en-US")),
                        DonGiaCot3 = bieuGia.DonGia3?.ToString("N0", CultureInfo.GetCultureInfo("en-US"))
                    });
                    i++;
                }
                response.Add(data);
            }

            var templatePath = RootPathConfig.TemplatePath.GetTemplate + "Book1.xlsx";
            var excelPackage = new ExcelPackage(new FileInfo(templatePath), true);
            var workbook = excelPackage.Workbook;

            var sheet1 = workbook.Worksheets["Sheet1"];
            var currentRow = 3;
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
                        if (item.Stt == 0)
                        {
                            sheet1.Cells[$"A{currentRow}"].Value = item.TenBieuGia;
                            sheet1.Cells[$"A{currentRow}:F{currentRow}"].Merge = true;
                            sheet1.Cells[$"A{currentRow}:F{currentRow}"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                            sheet1.Cells[$"A{currentRow}:F{currentRow}"].Style.Font.Bold = true;
                            currentRow++;
                        }
                        else
                        {
                            sheet1.Cells[$"A{currentRow}"].Value = item.Stt;
                            sheet1.Cells[$"B{currentRow}"].Value = item.TenBieuGia;
                            sheet1.Cells[$"C{currentRow}"].Value = item.DonVi;
                            sheet1.Cells[$"D{currentRow}"].Value = item.DonGiaCot1;
                            sheet1.Cells[$"E{currentRow}"].Value = item.DonGiaCot2;
                            sheet1.Cells[$"F{currentRow}"].Value = item.DonGiaCot3;
                            currentRow++;
                        }
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
            var query = await _unitOfWork.BieuGiaTongHop_CapNgamRepository.GetQuery(x => x.Nam == request.Nam && x.Quy == request.Quy)
               .Include(x => x.DM_BieuGia_CapNgam).ThenInclude(x => x.DM_LoaiBieuGia_CapNgam).ThenInclude(x => x.DM_KhuVuc)
               .Select(x => new
               {
                   IdBieuGia = x.IdBieuGia,
                   TenBieuGia = x.DM_BieuGia_CapNgam.TenBieuGia,
                   IdLoaiBieuGia = x.DM_BieuGia_CapNgam.idLoaiBieuGia,
                   PhanLoaiBieuGia = x.DM_BieuGia_CapNgam.DM_LoaiBieuGia_CapNgam.Code,
                   IdKhuVuc = x.DM_BieuGia_CapNgam.DM_LoaiBieuGia_CapNgam.IdKhuVuc,
                   TenKhuVuc = x.DM_BieuGia_CapNgam.DM_LoaiBieuGia_CapNgam.DM_KhuVuc.TenKhuVuc,
                   DonGia = x.DonGia,
                   DonGia2 = x.DonGia2,
                   DonGia3 = x.DonGia3,
                   TinhTrang = x.TinhTrang
               }).AsNoTracking()
               .ToListAsync();


            #region Nhóm dữ liệu
            int i1 = 1;
            var nhom1 = query.Where(x => int.Parse(x.PhanLoaiBieuGia) <= 3).Select(x => new BGTHChiTiet
            {
                Stt = i1++,
                TenBieuGia = x.TenBieuGia,
                DonVi = "m",
                DonGiaCot1 = x.DonGia.ToString(),
                DonGiaCot2 = x.DonGia2.ToString(),
                DonGiaCot3 = x.DonGia3.ToString(),
            }).ToList();
            int i2 = 1;
            var nhom2 = query.Where(x => int.Parse(x.PhanLoaiBieuGia) > 3 && int.Parse(x.PhanLoaiBieuGia) <= 6).Select(x => new BGTHChiTiet
            {
                Stt = i2++,
                TenBieuGia = x.TenBieuGia,
                DonVi = "m",
                DonGiaCot1 = x.DonGia.ToString(),
                DonGiaCot2 = x.DonGia2.ToString(),
                DonGiaCot3 = x.DonGia3.ToString(),
            }).ToList();
            int i3 = 1;
            var nhom3 = query.Where(x => int.Parse(x.PhanLoaiBieuGia) > 6 && int.Parse(x.PhanLoaiBieuGia) <= 9).Select(x => new BGTHChiTiet
            {
                Stt = i3++,
                TenBieuGia = x.TenBieuGia,
                DonVi = "m",
                DonGiaCot1 = x.DonGia.ToString(),
                DonGiaCot2 = x.DonGia2.ToString(),
                DonGiaCot3 = x.DonGia3.ToString(),
            }).ToList();
            int i4 = 1;
            var nhom4 = query.Where(x => int.Parse(x.PhanLoaiBieuGia) > 9 && int.Parse(x.PhanLoaiBieuGia) <= 12).Select(x => new BGTHChiTiet
            {
                Stt = i4++,
                TenBieuGia = x.TenBieuGia,
                DonVi = "m",
                DonGiaCot1 = x.DonGia.ToString(),
                DonGiaCot2 = x.DonGia2.ToString(),
                DonGiaCot3 = x.DonGia3.ToString(),
            }).ToList();
            int i5 = 1;
            var nhom5 = query.Where(x => int.Parse(x.PhanLoaiBieuGia) > 12 && int.Parse(x.PhanLoaiBieuGia) <= 15).Select(x => new BGTHChiTiet
            {
                Stt = i5++,
                TenBieuGia = x.TenBieuGia,
                DonVi = "m",
                DonGiaCot1 = x.DonGia.ToString(),
                DonGiaCot2 = x.DonGia2.ToString(),
                DonGiaCot3 = x.DonGia3.ToString(),
            }).ToList();
            int i6 = 1;
            var nhom6 = query.Where(x => int.Parse(x.PhanLoaiBieuGia) > 15 && int.Parse(x.PhanLoaiBieuGia) <= 18).Select(x => new BGTHChiTiet
            {
                Stt = i6++,
                TenBieuGia = x.TenBieuGia,
                DonVi = "m",
                DonGiaCot1 = x.DonGia.ToString(),
                DonGiaCot2 = x.DonGia2.ToString(),
                DonGiaCot3 = x.DonGia3.ToString(),
            }).ToList();
            int i7 = 1;
            var nhom7 = query.Where(x => int.Parse(x.PhanLoaiBieuGia) > 18 && int.Parse(x.PhanLoaiBieuGia) <= 21).Select(x => new BGTHChiTiet
            {
                Stt = i7++,
                TenBieuGia = x.TenBieuGia,
                DonVi = "m",
                DonGiaCot1 = x.DonGia.ToString(),
                DonGiaCot2 = x.DonGia2.ToString(),
                DonGiaCot3 = x.DonGia3.ToString(),
            }).ToList();
            int i8 = 1;
            var nhom8 = query.Where(x => int.Parse(x.PhanLoaiBieuGia) > 21 && int.Parse(x.PhanLoaiBieuGia) <= 24).Select(x => new BGTHChiTiet
            {
                Stt = i8++,
                TenBieuGia = x.TenBieuGia,
                DonVi = "m",
                DonGiaCot1 = x.DonGia.ToString(),
                DonGiaCot2 = x.DonGia2.ToString(),
                DonGiaCot3 = x.DonGia3.ToString(),
            }).ToList();
            int i9 = 1;
            var nhom9 = query.Where(x => int.Parse(x.PhanLoaiBieuGia) > 24 && int.Parse(x.PhanLoaiBieuGia) <= 27).Select(x => new BGTHChiTiet
            {
                Stt = i9++,
                TenBieuGia = x.TenBieuGia,
                DonVi = "m",
                DonGiaCot1 = x.DonGia.ToString(),
                DonGiaCot2 = x.DonGia2.ToString(),
                DonGiaCot3 = x.DonGia3.ToString(),
            }).ToList();
            #endregion

            var vung1 = new CSKHCapNgamResponse();
            vung1.TenKhuVuc = "1. VÙNG 1 - KHU VỰC 1";
            vung1.NhomVung = new List<NhomVung>();
            vung1.NhomVung.Add(new NhomVung()
            {
                Nhom = "1.1 Nhóm 1",
                ListBieuGiaChiTiet = new List<BGTHChiTiet>(nhom1)
            });
            vung1.NhomVung.Add(new NhomVung()
            {
                Nhom = "1.2 Nhóm 2",
                ListBieuGiaChiTiet = new List<BGTHChiTiet>(nhom2)
            });
            vung1.NhomVung.Add(new NhomVung()
            {
                Nhom = "1.3 Nhóm 3",
                ListBieuGiaChiTiet = new List<BGTHChiTiet>(nhom3)
            });

            var vung2 = new CSKHCapNgamResponse();
            vung2.TenKhuVuc = "2. VÙNG 1 - KHU VỰC 2";
            vung2.NhomVung = new List<NhomVung>();
            vung2.NhomVung.Add(new NhomVung()
            {
                Nhom = "2.1 Nhóm 1",
                ListBieuGiaChiTiet = new List<BGTHChiTiet>(nhom4)
            });
            vung2.NhomVung.Add(new NhomVung()
            {
                Nhom = "2.2 Nhóm 2",
                ListBieuGiaChiTiet = new List<BGTHChiTiet>(nhom5)
            });
            vung2.NhomVung.Add(new NhomVung()
            {
                Nhom = "2.3 Nhóm 3",
                ListBieuGiaChiTiet = new List<BGTHChiTiet>(nhom6)
            });

            var vung3 = new CSKHCapNgamResponse();
            vung3.TenKhuVuc = "3. VÙNG 2";
            vung3.NhomVung = new List<NhomVung>();
            vung3.NhomVung.Add(new NhomVung()
            {
                Nhom = "3.1 Nhóm 1",
                ListBieuGiaChiTiet = new List<BGTHChiTiet>(nhom7)
            });
            vung3.NhomVung.Add(new NhomVung()
            {
                Nhom = "3.2 Nhóm 2",
                ListBieuGiaChiTiet = new List<BGTHChiTiet>(nhom8)
            });
            vung3.NhomVung.Add(new NhomVung()
            {
                Nhom = "3.3 Nhóm 3",
                ListBieuGiaChiTiet = new List<BGTHChiTiet>(nhom9)
            });

            var response = new List<CSKHCapNgamResponse>();
            response.Add(vung1);
            response.Add(vung2);
            response.Add(vung3);
            return response;
        }

        public async Task<List<BieuGiaTongHopResponse>> GetList(BieuGiaTongHopRequest request)
        {
            var position = TokenExtensions.GetPosition();
            if (string.IsNullOrEmpty(position)) throw new EvnException("Người dùng có chức vụ không đúng");

            var loaiBieuGia = await _unitOfWork.DM_LoaiBieuGia_CapNgamRepository.GetQuery().AsNoTracking().ToListAsync();
            var query = await _unitOfWork.BieuGiaTongHop_CapNgamRepository.GetQuery(x => x.Nam == request.Nam && x.Quy == request.Quy)
                .Include(x => x.DM_BieuGia_CapNgam).ThenInclude(x => x.DM_LoaiBieuGia_CapNgam)
                .Select(x => new
                {
                    IdBieuGia = x.IdBieuGia,
                    TenBieuGia = x.DM_BieuGia_CapNgam.TenBieuGia,
                    IdLoaiBieuGia = x.DM_BieuGia_CapNgam.idLoaiBieuGia,
                    IdKhuVuc = x.DM_BieuGia_CapNgam.DM_LoaiBieuGia_CapNgam.IdKhuVuc,
                    MaKhuVuc = x.DM_BieuGia_CapNgam.DM_LoaiBieuGia_CapNgam.DM_KhuVuc.GhiChu,
                    DonGia = request.PhanLoai == 1 ? x.DonGia : (request.PhanLoai == 2 ? x.DonGia2 : x.DonGia3),
                    MaLoaiBieuGia = int.Parse(x.DM_BieuGia_CapNgam.DM_LoaiBieuGia_CapNgam.MaLoaiBieuGia),
                    TinhTrang = x.TinhTrang
                }).AsNoTracking()
                .ToListAsync();

            var groupBy = query.GroupBy(x => new { x.TenBieuGia, x.MaLoaiBieuGia }).Select(x => new { name = x.Key.TenBieuGia, ma = x.Key.MaLoaiBieuGia, listBG = x.ToList() }).ToList();

            var listResponse = new List<BieuGiaTongHopResponse>();
            foreach (var r in groupBy.OrderBy(x => x.ma))
            {
                var item = new BieuGiaTongHopResponse();
                item.TenBieuGia = r.name;
                var listData = new List<string>();

                for (int i = 1; i <= 9; i++)
                {
                    var khuvuc = query.GroupBy(x => x.IdKhuVuc).ToList();
                    //var values = r.listBG.Skip((i - 1) * i).Take(3).ToList();
                    var value = r.listBG.Where(x => x.MaKhuVuc == i.ToString()).FirstOrDefault()?.DonGia.ToString() ?? "";
                    listData.Add(value);
                }

                //foreach (var list in loaiBieuGia)
                //{
                //    var value = r.listBG.Where(x => x.IdKhuVuc == list.IdKhuVuc && x.IdLoaiBieuGia == list.Id).FirstOrDefault()?.DonGia.ToString() ?? "";
                //    listData.Add(value);
                //}
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

        public async Task<byte[]> XuatExcel(BieuGiaTongHopRequest request)
        {
            var position = TokenExtensions.GetPosition();
            if (string.IsNullOrEmpty(position)) throw new EvnException("Người dùng có chức vụ không đúng");

            var loaiBieuGia = await _unitOfWork.DM_LoaiBieuGia_CapNgamRepository.GetQuery().AsNoTracking().ToListAsync();
            var query = await _unitOfWork.BieuGiaTongHop_CapNgamRepository.GetQuery(x => x.Nam == request.Nam && x.Quy == request.Quy)
                 .Include(x => x.DM_BieuGia_CapNgam).ThenInclude(x => x.DM_LoaiBieuGia_CapNgam)
                 .Select(x => new
                 {
                     IdBieuGia = x.IdBieuGia,
                     TenBieuGia = x.DM_BieuGia_CapNgam.TenBieuGia,
                     IdLoaiBieuGia = x.DM_BieuGia_CapNgam.idLoaiBieuGia,
                     IdKhuVuc = x.DM_BieuGia_CapNgam.DM_LoaiBieuGia_CapNgam.IdKhuVuc,
                     MaKhuVuc = x.DM_BieuGia_CapNgam.DM_LoaiBieuGia_CapNgam.DM_KhuVuc.GhiChu,
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
                var listData = new List<string>();

                for (int i = 1; i <= 9; i++)
                {
                    var khuvuc = query.GroupBy(x => x.IdKhuVuc).ToList();
                    //var values = r.listBG.Skip((i - 1) * i).Take(3).ToList();
                    var value = r.listBG.Where(x => x.MaKhuVuc == i.ToString()).Skip((i - 1) * i).Take(3).FirstOrDefault()?.DonGia.ToString("N0", CultureInfo.GetCultureInfo("en-US")) ?? "0";
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

            var templatePath = RootPathConfig.TemplatePath.GetTemplate + "ChiTietBieuGiaCapNgam.xlsx";
            var excelPackage = new ExcelPackage(new FileInfo(templatePath), true);
            var workbook = excelPackage.Workbook;

            var sheet1 = workbook.Worksheets["Sheet1"];
            var currentRow = 4;
            if (listResponse.Any())
            {
                int stt = 1;
                foreach (var model in listResponse)
                {
                    sheet1.Cells[$"A{currentRow}"].Value = stt;
                    sheet1.Cells[$"B{currentRow}"].Value = model.TenBieuGia;
                    sheet1.Cells[$"C{currentRow}"].Value = model.ListData[0] ?? "";
                    sheet1.Cells[$"D{currentRow}"].Value = model.ListData[1] ?? "";
                    sheet1.Cells[$"E{currentRow}"].Value = model.ListData[2] ?? "";
                    sheet1.Cells[$"F{currentRow}"].Value = model.ListData[3] ?? "";
                    sheet1.Cells[$"G{currentRow}"].Value = model.ListData[4] ?? "";
                    sheet1.Cells[$"H{currentRow}"].Value = model.ListData[5] ?? "";
                    sheet1.Cells[$"I{currentRow}"].Value = model.ListData[6] ?? "";
                    sheet1.Cells[$"J{currentRow}"].Value = model.ListData[7] ?? "";
                    sheet1.Cells[$"K{currentRow}"].Value = model.ListData[8] ?? "";
                    currentRow++;
                    stt++;
                }

                var endRow = currentRow - 1;
                sheet1.Cells[$"A3:K{endRow}"].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                sheet1.Cells[$"A3:K{endRow}"].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                sheet1.Cells[$"A3:K{endRow}"].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                sheet1.Cells[$"A3:K{endRow}"].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            }
            return excelPackage.GetAsByteArray();
        }

        public async Task<object> GetDuLieuDonGia(int Vung)
        {
            var data = await _unitOfWork.BieuGiaTongHop_CapNgamRepository.GetQuery(x => x.TinhTrang == 4 && x.DM_BieuGia_CapNgam.DM_LoaiBieuGia_CapNgam.DM_KhuVuc.GhiChu == Vung.ToString())
                .Include(x => x.DM_BieuGia_CapNgam).ThenInclude(x => x.BieuGiaCongViec_CapNgam).ThenInclude(z => z.DM_CongViec_CapNgam)
                //.Where(x => x.DM_BieuGia.BieuGiaCongViec.Any(z => z.CongViecChinh && z.DM_CongViec.MaCongViec == LoaiCap))
                .Include(x => x.DM_BieuGia_CapNgam).ThenInclude(x => x.DM_LoaiBieuGia_CapNgam)
                .GroupBy(x => new { x.IdBieuGia, x.Nam, x.Quy }).
                 Select(x => x.OrderByDescending(x => x.Nam).ThenByDescending(y => y.Quy).First())
                .ToListAsync();

            var listApiResult = new List<ApiDonGiaVatLieuResponse>();


            foreach (var item in data)
            {
                //var maVatTu = item.DM_BieuGia_CapNgam.BieuGiaCongViec_CapNgam.FirstOrDefault(x => x.CongViecChinh)?.DM_CongViec_CapNgam?.MaCongViec;
                //Console.WriteLine(maVatTu);
                //var donGia = data.Where(x => x.DM_BieuGia_CapNgam.BieuGiaCongViec_CapNgam.Any(y => y.DM_CongViec_CapNgam.MaCongViec == maVatTu)
                //                )
                //    .OrderBy(z => int.Parse(z.DM_BieuGia_CapNgam.DM_LoaiBieuGia_CapNgam.MaLoaiBieuGia))
                //    .ToArray();

                var apiResult = new ApiDonGiaVatLieuResponse();
                apiResult.MaVatTu = item.DM_BieuGia_CapNgam.BieuGiaCongViec_CapNgam.FirstOrDefault(x => x.CongViecChinh)?.DM_CongViec_CapNgam?.MaCongViec;
                apiResult.NgayHieuLuc = item.UpdatedDate?.ToString("yyyy-MM-dd");
                apiResult.DonGiaTronGoi = new ApiDonGia
                {
                    CapDien = item?.DonGia.ToString(),
                    NangCongSuat = "0",
                    DiDoi = "0",
                };
                apiResult.DonGiaTuTucCapSau = new ApiDonGia
                {
                    CapDien = item?.DonGia2.ToString(),
                    NangCongSuat = "0",
                    DiDoi = "0",
                };
                apiResult.DonGiaTuTucCapVaVatTu = new ApiDonGia
                {
                    CapDien = item?.DonGia3.ToString(),
                    NangCongSuat = "0",
                    DiDoi = "0",
                };
                apiResult.HinhThucThiCong = item.DM_BieuGia_CapNgam.MaBieuGia.Trim();
                listApiResult.Add(apiResult);
            }

            return listApiResult;
        }
    }
}
