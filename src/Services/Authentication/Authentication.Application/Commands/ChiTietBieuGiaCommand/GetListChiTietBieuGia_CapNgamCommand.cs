using Authentication.Application.Model.ChiTietBieuGia;
using Authentication.Infrastructure.Repositories;
using EVN.Core.Extensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using System.ComponentModel;
using System.Reflection;
using static EVN.Core.Common.AppEnum;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Authentication.Application.Commands.ChiTietBieuGiaCommand
{
    public class GetListChiTietBieuGia_CapNgamCommand : IRequest<ChiTietBieuGiaResult>
    {
        public int Quy { get; set; }
        public int Nam { get; set; }
        public Guid IdBieuGia { get; set; }
    }

    public class GetListChiTietBieuGia_CapNgamCommandHandler : IRequestHandler<GetListChiTietBieuGia_CapNgamCommand, ChiTietBieuGiaResult>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetListChiTietBieuGia_CapNgamCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ChiTietBieuGiaResult> Handle(GetListChiTietBieuGia_CapNgamCommand request, CancellationToken cancellationToken)
        {
            var chuaCoDuLieu = false;

            var quyTruoc = request.Quy == 1 ? 4 : request.Quy - 1;
            var namTruoc = request.Quy == 1 ? request.Nam - 1 : request.Nam;

            var result = new ChiTietBieuGiaResult();
            decimal soLuongCVC = 0;

            //Tạo câu query
            var query = await _unitOfWork.BieuGiaCongViec_CapNgamRepository.GetQuery(x => x.IdBieuGia == request.IdBieuGia)
                .Include(x => x.DM_BieuGia_CapNgam.ChiTietBieuGia_CapNgam.Where(x => x.Quy == request.Quy && x.Nam == request.Nam)).Include(x => x.DM_CongViec_CapNgam)
                .AsNoTracking()
                .Select(x => new ChiTietBieuGiaResponse
                {
                    //
                    PhanLoai = x.PhanLoai,
                    ThuTuHienThi = x.ThuTuHienThi,
                    Id = x.DM_BieuGia_CapNgam.ChiTietBieuGia_CapNgam.FirstOrDefault(y => y.IDCongViec == x.IdCongViec && y.Nam == request.Nam && y.Quy == request.Quy).Id,
                    IdBieuGia = x.DM_BieuGia_CapNgam.Id,
                    MaNoiDungCongViec = x.DM_CongViec_CapNgam.MaCongViec,
                    NoiDungCongViec = x.DM_CongViec_CapNgam.TenCongViec,
                    DonViTinh = x.DM_CongViec_CapNgam.DonViTinh,
                    IdCongViec = x.DM_CongViec_CapNgam.Id,
                    Nam = request.Nam,
                    Quy = request.Quy,
                    SoLuong = Math.Round(x.DM_BieuGia_CapNgam.ChiTietBieuGia_CapNgam.FirstOrDefault(y => y.IDCongViec == x.IdCongViec && y.Nam == request.Nam && y.Quy == request.Quy) != null
                    ? x.DM_BieuGia_CapNgam.ChiTietBieuGia_CapNgam.FirstOrDefault(y => y.IDCongViec == x.IdCongViec && y.Nam == request.Nam && y.Quy == request.Quy).SoLuong :
                     x.DM_BieuGia_CapNgam.ChiTietBieuGia_CapNgam.FirstOrDefault(y => y.IDCongViec == x.IdCongViec && y.Nam == namTruoc && y.Quy == quyTruoc).SoLuong, 2), //0

                    HeSoDieuChinh_K1nc = Math.Round(x.DM_BieuGia_CapNgam.ChiTietBieuGia_CapNgam.FirstOrDefault(y => y.IDCongViec == x.IdCongViec && y.Nam == request.Nam && y.Quy == request.Quy) != null
                    ? x.DM_BieuGia_CapNgam.ChiTietBieuGia_CapNgam.FirstOrDefault(y => y.IDCongViec == x.IdCongViec && y.Nam == request.Nam && y.Quy == request.Quy).HeSoDieuChinh_K1nc :
                    x.DM_BieuGia_CapNgam.ChiTietBieuGia_CapNgam.FirstOrDefault(y => y.IDCongViec == x.IdCongViec && y.Nam == namTruoc && y.Quy == quyTruoc).HeSoDieuChinh_K1nc, 2), //1

                    HeSoDieuChinh_K2nc = Math.Round(x.DM_BieuGia_CapNgam.ChiTietBieuGia_CapNgam.FirstOrDefault(y => y.IDCongViec == x.IdCongViec && y.Nam == request.Nam && y.Quy == request.Quy) != null
                    ? x.DM_BieuGia_CapNgam.ChiTietBieuGia_CapNgam.FirstOrDefault(y => y.IDCongViec == x.IdCongViec && y.Nam == request.Nam && y.Quy == request.Quy).HeSoDieuChinh_K2nc :
                    x.DM_BieuGia_CapNgam.ChiTietBieuGia_CapNgam.FirstOrDefault(y => y.IDCongViec == x.IdCongViec && y.Nam == namTruoc && y.Quy == quyTruoc).HeSoDieuChinh_K2nc, 2), // 1

                    HeSoDieuChinh_Kmtc = Math.Round(x.DM_BieuGia_CapNgam.ChiTietBieuGia_CapNgam.FirstOrDefault(y => y.IDCongViec == x.IdCongViec && y.Nam == request.Nam && y.Quy == request.Quy) != null
                    ? x.DM_BieuGia_CapNgam.ChiTietBieuGia_CapNgam.FirstOrDefault(y => y.IDCongViec == x.IdCongViec && y.Nam == request.Nam && y.Quy == request.Quy).HeSoDieuChinh_Kmtc :
                    x.DM_BieuGia_CapNgam.ChiTietBieuGia_CapNgam.FirstOrDefault(y => y.IDCongViec == x.IdCongViec && y.Nam == namTruoc && y.Quy == quyTruoc).HeSoDieuChinh_Kmtc, 2), // 1

                    DonGia_VL = Math.Round(x.DM_BieuGia_CapNgam.ChiTietBieuGia_CapNgam.FirstOrDefault(y => y.IDCongViec == x.IdCongViec && y.Nam == request.Nam && y.Quy == request.Quy) != null
                    ? x.DM_BieuGia_CapNgam.ChiTietBieuGia_CapNgam.FirstOrDefault(y => y.IDCongViec == x.IdCongViec && y.Nam == request.Nam && y.Quy == request.Quy).DonGia_VL :
                    x.DM_BieuGia_CapNgam.ChiTietBieuGia_CapNgam.FirstOrDefault(y => y.IDCongViec == x.IdCongViec && y.Nam == namTruoc && y.Quy == quyTruoc).DonGia_VL, 0), // 0

                    DonGia_NC = Math.Round(x.DM_BieuGia_CapNgam.ChiTietBieuGia_CapNgam.FirstOrDefault(y => y.IDCongViec == x.IdCongViec && y.Nam == request.Nam && y.Quy == request.Quy) != null
                    ? x.DM_BieuGia_CapNgam.ChiTietBieuGia_CapNgam.FirstOrDefault(y => y.IDCongViec == x.IdCongViec && y.Nam == request.Nam && y.Quy == request.Quy).DonGia_NC :
                    x.DM_BieuGia_CapNgam.ChiTietBieuGia_CapNgam.FirstOrDefault(y => y.IDCongViec == x.IdCongViec && y.Nam == namTruoc && y.Quy == quyTruoc).DonGia_NC, 0), // 0

                    DonGia_MTC = Math.Round(x.DM_BieuGia_CapNgam.ChiTietBieuGia_CapNgam.FirstOrDefault(y => y.IDCongViec == x.IdCongViec && y.Nam == request.Nam && y.Quy == request.Quy) != null
                    ? x.DM_BieuGia_CapNgam.ChiTietBieuGia_CapNgam.FirstOrDefault(y => y.IDCongViec == x.IdCongViec && y.Nam == request.Nam && y.Quy == request.Quy).DonGia_MTC :
                    x.DM_BieuGia_CapNgam.ChiTietBieuGia_CapNgam.FirstOrDefault(y => y.IDCongViec == x.IdCongViec && y.Nam == namTruoc && y.Quy == quyTruoc).DonGia_MTC, 0), // 0

                    CongViecChinh = x.CongViecChinh,
                }).AsSplitQuery()
                .ToListAsync();

            decimal donGiaVatLieu = 0;
            decimal donGiaNhanCong = 0;


            var stt = 1;
            foreach (var item in query)
            {
                item.SoLuong = item.SoLuong ?? 0;
                item.HeSoDieuChinh_K1nc = item.HeSoDieuChinh_K1nc ?? 1;
                item.HeSoDieuChinh_K2nc = item.HeSoDieuChinh_K2nc ?? 1;
                item.HeSoDieuChinh_Kmtc = item.HeSoDieuChinh_Kmtc ?? 1;
                item.DonGia_VL = item.DonGia_VL ?? 0;
                item.DonGia_NC = item.DonGia_NC ?? 0;
                item.DonGia_MTC = item.DonGia_MTC ?? 0;

                if (stt == 1 && item.Id == null) chuaCoDuLieu = true;
                if (string.IsNullOrEmpty(item.MaNoiDungCongViec))
                {
                    donGiaVatLieu += (item.DonGia_VL.Value * item.SoLuong.Value);
                }
                else
                {
                    donGiaNhanCong += (item.DonGia_VL.Value * item.SoLuong.Value);
                }

                item.Stt = stt.ToString();
                item.CPChung = item.PhanLoai == 2 ? (string.IsNullOrEmpty(item.MaNoiDungCongViec) ? 0 : Math.Round((decimal)65 / 100 * (item.DonGia_NC.Value), 0)) : 0;                                                  //12            
                item.CPChung2 = item.PhanLoai == 3 ? (string.IsNullOrEmpty(item.MaNoiDungCongViec) ? 0 : Math.Round((decimal)6.2 / 100 * (item.DonGia_VL.Value + item.DonGia_NC.Value + item.DonGia_MTC.Value), 0)) : 0;                                                  //12            
                item.CPChung3 = item.PhanLoai == 4 ? (string.IsNullOrEmpty(item.MaNoiDungCongViec) ? 0 : Math.Round((decimal)66 / 100 * (item.DonGia_NC.Value), 0)) : 0;                                                  //12            
                item.CPNhaTam = 0;             //13             tạm thời không cho công thức        
                //item.CPNhaTam = Math.Round((item.DonGia_VL.Value + item.DonGia_NC.Value + item.DonGia_MTC.Value) * (decimal)1.2 / 100, 0);             //13                    
                item.CPCongViecKhongXDDuocTuTK = string.IsNullOrEmpty(item.MaNoiDungCongViec) ? 0 : Math.Round((item.DonGia_VL.Value + item.DonGia_NC.Value + item.DonGia_MTC.Value) * (decimal)2 / 100, 0); ; //14     
                item.ThuNhapChiuThue = string.IsNullOrEmpty(item.MaNoiDungCongViec) ? 0 : Math.Round((item.DonGia_VL.Value + item.DonGia_NC.Value + item.DonGia_MTC.Value + item.CPChung.Value + item.CPChung2.Value + item.CPChung3.Value + item.CPNhaTam.Value + item.CPCongViecKhongXDDuocTuTK.Value) * (decimal)6 / 100, 0);
                item.DonGiaTruocThue = Math.Round(item.DonGia_VL.Value + item.DonGia_NC.Value + item.DonGia_MTC.Value + item.CPChung.Value + item.CPChung2.Value + item.CPChung3.Value + item.CPNhaTam.Value + item.CPCongViecKhongXDDuocTuTK.Value + item.ThuNhapChiuThue.Value, 0); ;
                item.GiaTriTruocThue = Math.Round(item.SoLuong.Value * item.DonGiaTruocThue.Value, 0);
                result.Tong += Math.Round(item.GiaTriTruocThue.Value, 0);
                if (item.CongViecChinh && item.SoLuong > 0)
                {
                    soLuongCVC = item.SoLuong.Value;
                }
                stt++;
            }
            var congViecChinh = query.Where(x => x.CongViecChinh).FirstOrDefault();

            var listBieuGia = new List<ChiTietBieuGiaResponse>();
            int t = 1;
            int d = 1;
            int ttt = 1;
            foreach (var item in query.OrderBy(x => x.PhanLoai).ThenBy(x => x.ThuTuHienThi).ToList())
            {
                if (item.PhanLoai == t)
                {
                    listBieuGia.Add(new ChiTietBieuGiaResponse
                    {
                        Stt = ChuLaMa(t),
                        NoiDungCongViec = GetEnumDescription((PhanLoaiEnum)item.PhanLoai)
                    });
                    t++;
                    d = 1;
                    item.tt = ttt;
                    ttt++;
                }
                item.tt = ttt;
                item.Stt = d.ToString();
                listBieuGia.Add(item);
                d++;
                ttt++;
            }

            result.ListBieuGia = listBieuGia;
            result.KhaoSat = 0;
            result.CongTruocThue = result.KhaoSat + result.Tong;
            result.DonGiaTongHopTruocThue = soLuongCVC == 0 ? 0 : Math.Round(result.Tong / soLuongCVC, 0);
            result.DonGiaThu5 = result.DonGiaTongHopTruocThue;
            result.DonGiaThu6 = (congViecChinh == null || congViecChinh.SoLuong.Value == (decimal)0.00) ? 0 : Math.Round((result.CongTruocThue - (congViecChinh.DonGia_VL.Value * congViecChinh.SoLuong.Value)) / congViecChinh.SoLuong.Value, 0);

            var itemLast = new ChiTietBieuGiaResponse();
            itemLast.NoiDungCongViec = "TỔNG CỘNG";
            itemLast.GiaTriTruocThue = result.Tong;


            result.ListBieuGia.Add(itemLast);

            var bieuGiaTongHop = await _unitOfWork.BieuGiaTongHop_CapNgamRepository
                .FindOneAsync(x => x.IdBieuGia == request.IdBieuGia && x.Quy == request.Quy && x.Nam == request.Nam && x.TinhTrang != (int)TinhTrangEnum.DaDuyet);



            result.ChuaCoDuLieu = chuaCoDuLieu;
            result.DonGiaThu7 = congViecChinh.SoLuong == 0 ? 0 : Math.Round((result.CongTruocThue - (donGiaVatLieu + (donGiaNhanCong * (decimal)1.06))) / congViecChinh.SoLuong.Value, 0);

            if (bieuGiaTongHop != null)
            {
                bieuGiaTongHop.DonGia = result.DonGiaThu5;
                bieuGiaTongHop.DonGia2 = result.DonGiaThu6;
                bieuGiaTongHop.DonGia3 = result.DonGiaThu7;
                _unitOfWork.BieuGiaTongHop_CapNgamRepository.Update(bieuGiaTongHop);
                await _unitOfWork.SaveChangesAsync();
            }

            return result;

        }

        private string ChuLaMa(int num)
        {
            if (num == 1)
            {
                return "I";
            }
            else if (num == 2)
            {
                return "II";
            }
            else if (num == 3)
            {
                return "III";
            }
            else
            {
                return "IV";
            }
        }
        private string GetEnumDescription(Enum value)
        {
            Type type = value.GetType();
            string name = Enum.GetName(type, value);

            if (name != null)
            {
                FieldInfo field = type.GetField(name);
                if (field != null)
                {
                    if (Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) is DescriptionAttribute attribute)
                    {
                        return attribute.Description;
                    }
                }
            }

            return value.ToString();
        }
    }
}
