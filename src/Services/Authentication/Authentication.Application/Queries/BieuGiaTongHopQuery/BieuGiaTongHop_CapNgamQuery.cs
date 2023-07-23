using Authentication.Application.Model.BieuGiaTongHop;
using Authentication.Infrastructure.AggregatesModel.PositionAggregate;
using Authentication.Infrastructure.Repositories;
using EVN.Core.Exceptions;
using EVN.Core.Extensions;
using Microsoft.EntityFrameworkCore;
using System.Text;
using static EVN.Core.Common.AppEnum;

namespace Authentication.Application.Queries.BieuGiaTongHop_CapNgamQuery
{
    public interface IBieuGiaTongHop_CapNgamQuery
    {
        Task<List<BieuGiaTongHopResponse>> GetList(BieuGiaTongHopRequest request);
        Task<object> ChiTietPDF(ChiTietPDFRequest request);
    }
    public class BieuGiaTongHop_CapNgamQuery : IBieuGiaTongHop_CapNgamQuery
    {
        private readonly IUnitOfWork _unitOfWork;
        public BieuGiaTongHop_CapNgamQuery(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
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
                   PhanLoaiBieuGia = x.DM_BieuGia_CapNgam.DM_LoaiBieuGia_CapNgam.MaLoaiBieuGia,
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
    }
}
