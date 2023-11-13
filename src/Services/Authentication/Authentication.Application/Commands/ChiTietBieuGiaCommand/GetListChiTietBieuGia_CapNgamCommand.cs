using Authentication.Application.Model.ChiTietBieuGia;
using Authentication.Infrastructure.Repositories;
using EVN.Core.Exceptions;
using EVN.Core.Extensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.WebEncoders.Testing;
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

            var bieuGiaTongHop = await _unitOfWork.BieuGiaTongHop_CapNgamRepository
                .FindOneAsync(x => x.IdBieuGia == request.IdBieuGia && x.Quy == request.Quy && x.Nam == request.Nam && x.TinhTrang != (int)TinhTrangEnum.DaDuyet);

            var cauHinh = await _unitOfWork.CauHinhBieuGiaRepository.GetQuery(x => x.PhanLoaiCap == 2).ToListAsync();
            var cpChung1 = cauHinh.Where(x => x.TenCauHinh == TenCauHinhEnum.CH5.GetHashCode().ToString()
                 && x.Quy == request.Quy && x.Nam == request.Nam)
                .OrderByDescending(x => x.CreatedDate)
            .FirstOrDefault()?.GiaTri;
            var cpChung2 = cauHinh.Where(x => x.TenCauHinh == TenCauHinhEnum.CH6.GetHashCode().ToString()
             && x.Quy == request.Quy && x.Nam == request.Nam)
                .OrderByDescending(x => x.CreatedDate).FirstOrDefault()?.GiaTri;
            var cpChung3 = cauHinh.Where(x => x.TenCauHinh == TenCauHinhEnum.CH7.GetHashCode().ToString()
             && x.Quy == request.Quy && x.Nam == request.Nam)
                .OrderByDescending(x => x.CreatedDate)
            .FirstOrDefault()?.GiaTri;
            //var cpNhaTam = cauHinh.Where(x => x.TenCauHinh == TenCauHinhEnum.CH2.GetHashCode().ToString()).OrderBy(x => x.Nam).ThenBy(x => x.Quy).FirstOrDefault()?.GiaTri;
            var cpCVKXD = cauHinh.Where(x => x.TenCauHinh == TenCauHinhEnum.CH8.GetHashCode().ToString()
             && x.Quy == request.Quy && x.Nam == request.Nam)
                .OrderByDescending(x => x.CreatedDate).FirstOrDefault()?.GiaTri;
            var tnct = cauHinh.Where(x => x.TenCauHinh == TenCauHinhEnum.CH9.GetHashCode().ToString()
             && x.Quy == request.Quy && x.Nam == request.Nam)
                .OrderByDescending(x => x.CreatedDate).FirstOrDefault()?.GiaTri;

            if (cpChung1 == null || cpChung2 == null || cpChung3 == null /* || cpNhaTam == null*/ || cpCVKXD == null || tnct == null)
            {
                throw new EvnException("Chưa có cấu hình biểu giá");
            }

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
                    VungKhuVuc = x.DM_BieuGia_CapNgam.DM_LoaiBieuGia_CapNgam.DM_KhuVuc.GhiChu,
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
                    SoLuong = x.DM_BieuGia_CapNgam.ChiTietBieuGia_CapNgam.FirstOrDefault(y => y.IDCongViec == x.IdCongViec && y.Nam == request.Nam && y.Quy == request.Quy) != null
                    ? x.DM_BieuGia_CapNgam.ChiTietBieuGia_CapNgam.Where(y => y.IDCongViec == x.IdCongViec && y.Nam == request.Nam && y.Quy == request.Quy)
                    .OrderByDescending(z => z.CreatedDate).FirstOrDefault().SoLuong :
                     x.DM_BieuGia_CapNgam.ChiTietBieuGia_CapNgam.Where(y => y.IDCongViec == x.IdCongViec && y.Nam == namTruoc && y.Quy == quyTruoc)
                     .OrderByDescending(z => z.CreatedDate).FirstOrDefault().SoLuong, //0

                    HeSoDieuChinh_K1nc = x.DM_BieuGia_CapNgam.ChiTietBieuGia_CapNgam.FirstOrDefault(y => y.IDCongViec == x.IdCongViec && y.Nam == request.Nam && y.Quy == request.Quy) != null
                    ? x.DM_BieuGia_CapNgam.ChiTietBieuGia_CapNgam.Where(y => y.IDCongViec == x.IdCongViec && y.Nam == request.Nam && y.Quy == request.Quy)
                    .OrderByDescending(z => z.CreatedDate).FirstOrDefault().HeSoDieuChinh_K1nc :
                    x.DM_BieuGia_CapNgam.ChiTietBieuGia_CapNgam.Where(y => y.IDCongViec == x.IdCongViec && y.Nam == namTruoc && y.Quy == quyTruoc)
                    .OrderByDescending(z => z.CreatedDate).FirstOrDefault().HeSoDieuChinh_K1nc, //1

                    HeSoDieuChinh_K2nc = x.DM_BieuGia_CapNgam.ChiTietBieuGia_CapNgam.FirstOrDefault(y => y.IDCongViec == x.IdCongViec && y.Nam == request.Nam && y.Quy == request.Quy) != null
                    ? x.DM_BieuGia_CapNgam.ChiTietBieuGia_CapNgam.Where(y => y.IDCongViec == x.IdCongViec && y.Nam == request.Nam && y.Quy == request.Quy)
                    .OrderByDescending(z => z.CreatedDate).FirstOrDefault().HeSoDieuChinh_K2nc :
                    x.DM_BieuGia_CapNgam.ChiTietBieuGia_CapNgam.Where(y => y.IDCongViec == x.IdCongViec && y.Nam == namTruoc && y.Quy == quyTruoc)
                    .OrderByDescending(z => z.CreatedDate).FirstOrDefault().HeSoDieuChinh_K2nc, // 1

                    HeSoDieuChinh_Kmtc = x.DM_BieuGia_CapNgam.ChiTietBieuGia_CapNgam.FirstOrDefault(y => y.IDCongViec == x.IdCongViec && y.Nam == request.Nam && y.Quy == request.Quy) != null
                    ? x.DM_BieuGia_CapNgam.ChiTietBieuGia_CapNgam.Where(y => y.IDCongViec == x.IdCongViec && y.Nam == request.Nam && y.Quy == request.Quy)
                    .OrderByDescending(z => z.CreatedDate).FirstOrDefault().HeSoDieuChinh_Kmtc :
                    x.DM_BieuGia_CapNgam.ChiTietBieuGia_CapNgam.Where(y => y.IDCongViec == x.IdCongViec && y.Nam == namTruoc && y.Quy == quyTruoc)
                    .OrderByDescending(z => z.CreatedDate).FirstOrDefault().HeSoDieuChinh_Kmtc, // 1

                    //DonGia_VL = Math.Round(x.DM_BieuGia_CapNgam.ChiTietBieuGia_CapNgam.FirstOrDefault(y => y.IDCongViec == x.IdCongViec && y.Nam == request.Nam && y.Quy == request.Quy) != null
                    //? x.DM_BieuGia_CapNgam.ChiTietBieuGia_CapNgam.FirstOrDefault(y => y.IDCongViec == x.IdCongViec && y.Nam == request.Nam && y.Quy == request.Quy).DonGia_VL :
                    //x.DM_BieuGia_CapNgam.ChiTietBieuGia_CapNgam.FirstOrDefault(y => y.IDCongViec == x.IdCongViec && y.Nam == namTruoc && y.Quy == quyTruoc).DonGia_VL, 0), // 0

                    //DonGia_NC = Math.Round(x.DM_BieuGia_CapNgam.ChiTietBieuGia_CapNgam.FirstOrDefault(y => y.IDCongViec == x.IdCongViec && y.Nam == request.Nam && y.Quy == request.Quy) != null
                    //? x.DM_BieuGia_CapNgam.ChiTietBieuGia_CapNgam.FirstOrDefault(y => y.IDCongViec == x.IdCongViec && y.Nam == request.Nam && y.Quy == request.Quy).DonGia_NC :
                    //x.DM_BieuGia_CapNgam.ChiTietBieuGia_CapNgam.FirstOrDefault(y => y.IDCongViec == x.IdCongViec && y.Nam == namTruoc && y.Quy == quyTruoc).DonGia_NC, 0), // 0

                    //DonGia_MTC = Math.Round(x.DM_BieuGia_CapNgam.ChiTietBieuGia_CapNgam.FirstOrDefault(y => y.IDCongViec == x.IdCongViec && y.Nam == request.Nam && y.Quy == request.Quy) != null
                    //? x.DM_BieuGia_CapNgam.ChiTietBieuGia_CapNgam.FirstOrDefault(y => y.IDCongViec == x.IdCongViec && y.Nam == request.Nam && y.Quy == request.Quy).DonGia_MTC :
                    //x.DM_BieuGia_CapNgam.ChiTietBieuGia_CapNgam.FirstOrDefault(y => y.IDCongViec == x.IdCongViec && y.Nam == namTruoc && y.Quy == quyTruoc).DonGia_MTC, 0), // 0


                    //DonGia_VL = 0,
                    //DonGia_NC = 0,
                    //DonGia_MTC = 0,

                    DonGia_VL = x.DM_BieuGia_CapNgam.ChiTietBieuGia_CapNgam.Where(y => y.IDCongViec == x.IdCongViec && y.Nam == request.Nam && y.Quy == request.Quy)
                    .OrderByDescending(z => z.CreatedDate).FirstOrDefault().DonGia_VL,
                    DonGia_NC = x.DM_BieuGia_CapNgam.ChiTietBieuGia_CapNgam.Where(y => y.IDCongViec == x.IdCongViec && y.Nam == request.Nam && y.Quy == request.Quy)
                    .OrderByDescending(z => z.CreatedDate).FirstOrDefault().DonGia_NC,
                    DonGia_MTC = x.DM_BieuGia_CapNgam.ChiTietBieuGia_CapNgam.Where(y => y.IDCongViec == x.IdCongViec && y.Nam == request.Nam && y.Quy == request.Quy)
                    .OrderByDescending(z => z.CreatedDate).FirstOrDefault().DonGia_MTC,


                    CongViecChinh = x.CongViecChinh,

                    //ChuaCoDuLieu = x.DM_BieuGia_CapNgam.ChiTietBieuGia_CapNgam.FirstOrDefault(y => y.IDCongViec == x.IdCongViec && y.Nam == request.Nam && y.Quy == request.Quy)
                    //== null ? true : false
                    ChuaCoDuLieu = true
                }).AsSplitQuery()
                .ToListAsync();

            decimal donGiaVatLieu = 0;
            decimal donGiaNhanCong = 0;

            var vungKhuvuc = query.FirstOrDefault()?.VungKhuVuc;

            var listDonGiaCap = await _unitOfWork.GiaCap_CapNgamRepository.GetQuery().Include(z => z.DM_LoaiCap_CapNgam)
                .GroupBy(x => new { x.IdLoaiCap }).Select(x => x.OrderByDescending(y => y.CreatedDate).First()).AsNoTracking().ToListAsync();

            var listDonGiaChietTinh = await _unitOfWork.DonGiaChietTinh_CapNgamRepository.GetQuery(x => x.VungKhuVuc.ToString() == vungKhuvuc).Include(z => z.DM_CongViec_CapNgam)
                .GroupBy(x => new { x.IdCongViec }).Select(x => x.OrderByDescending(y => y.CreatedDate).First()).AsNoTracking().ToListAsync();

            var listDonGiaVatLieu = await _unitOfWork.DonGiaVatLieu_CapNgamRepository.GetQuery(x => x.VungKhuVuc.ToString() == vungKhuvuc).Include(z => z.DM_VatLieu_CapNgam)
                .GroupBy(x => new { x.IdVatLieu }).Select(x => x.OrderByDescending(y => y.CreatedDate).First()).AsNoTracking().ToListAsync();

            var stt = 1;
            foreach (var item in query)
            {
                item.SoLuong = item.SoLuong ?? 0;
                item.HeSoDieuChinh_K1nc = item.HeSoDieuChinh_K1nc ?? 1;
                item.HeSoDieuChinh_K2nc = item.HeSoDieuChinh_K2nc ?? 1;
                item.HeSoDieuChinh_Kmtc = item.HeSoDieuChinh_Kmtc ?? 1;


                var listMaChietTinh = new List<string>() { "D", "03", "05", "11", "SE", "SA", "AB", "BB", "AD", "GT", "HT", "SB", "AD" }; // Các công việc chiết tính mã này thiếu này a



                if (item.CongViecChinh)
                {
                    var giaCap = listDonGiaCap.Where(x => x.DM_LoaiCap_CapNgam.MaLoaiCap.Trim() == item.MaNoiDungCongViec).FirstOrDefault()?.DonGia;
                    if (giaCap == null)
                    {
                        Console.WriteLine(item.MaNoiDungCongViec);
                    }
                    item.DonGia_VL = item.DonGia_VL ?? ((giaCap ?? 0) * item.HeSoDieuChinh_K1nc);
                    item.DonGia_NC = item.DonGia_NC ?? ((item?.DonGia_NC ?? 0) * item.HeSoDieuChinh_K2nc);
                    item.DonGia_MTC = item.DonGia_MTC ?? ((item?.DonGia_MTC ?? 0) * item.HeSoDieuChinh_Kmtc);
                }
                else if (!string.IsNullOrEmpty(item.MaNoiDungCongViec) && listMaChietTinh.Any(prefix => item.MaNoiDungCongViec.ToUpper().StartsWith(prefix)))
                {
                    var donGiaCT = listDonGiaChietTinh.Where(x => x.IdCongViec == item.IdCongViec).FirstOrDefault();
                    item.DonGia_VL = item.DonGia_VL ?? ((donGiaCT?.DonGiaVatLieu ?? 0) * item.HeSoDieuChinh_K1nc);
                    item.DonGia_NC = item.DonGia_NC ?? ((donGiaCT?.DonGiaNhanCong ?? 0) * item.HeSoDieuChinh_K2nc);
                    item.DonGia_MTC = item.DonGia_MTC ?? ((donGiaCT?.DonGiaMTC ?? 0) * item.HeSoDieuChinh_Kmtc);
                }
                else
                {
                    var vatLieu = listDonGiaVatLieu.Where(x => x.DM_VatLieu_CapNgam.MaVatLieu != null && (x.DM_VatLieu_CapNgam.MaVatLieu.Trim() == item.MaNoiDungCongViec.Trim())).FirstOrDefault();
                    item.DonGia_VL = item.DonGia_VL ?? ((vatLieu?.DonGia ?? 0) * item.HeSoDieuChinh_K1nc);
                    item.DonGia_NC = item.DonGia_NC ?? ((item?.DonGia_NC ?? 0) * item.HeSoDieuChinh_K2nc);
                    item.DonGia_MTC = item.DonGia_MTC ?? ((item?.DonGia_MTC ?? 0) * item.HeSoDieuChinh_Kmtc);
                }



                if (stt == 1 && item.Id == null) chuaCoDuLieu = true;

                if (!listMaChietTinh.Any(prefix => item.MaNoiDungCongViec.ToUpper().StartsWith(prefix)))
                {
                    donGiaVatLieu += (item.DonGia_VL.Value * item.SoLuong.Value);
                }
                else
                {
                    donGiaNhanCong += (item.DonGia_VL.Value * item.SoLuong.Value);
                }

                item.Stt = stt.ToString();
                item.CPChung = item.PhanLoai == 2 ? (string.IsNullOrEmpty(item.MaNoiDungCongViec) ? 0 : Math.Round(decimal.Parse(cpChung1) / 100 * (item.DonGia_NC.Value), 0)) : 0;                                                  //12            
                item.CPChung2 = item.PhanLoai == 3 ? (string.IsNullOrEmpty(item.MaNoiDungCongViec) ? 0 : Math.Round(decimal.Parse(cpChung2) / 100 * (item.DonGia_VL.Value + item.DonGia_NC.Value + item.DonGia_MTC.Value), 0)) : 0;                                                  //12            
                item.CPChung3 = item.PhanLoai == 4 ? (string.IsNullOrEmpty(item.MaNoiDungCongViec) ? 0 : Math.Round(decimal.Parse(cpChung3) / 100 * (item.DonGia_NC.Value), 0)) : 0;                                                  //12            
                item.CPNhaTam = 0;             //13             tạm thời không cho công thức        
                                               //item.CPNhaTam = Math.Round((item.DonGia_VL.Value + item.DonGia_NC.Value + item.DonGia_MTC.Value) * (decimal)1.2 / 100, 0);             //13                    
                                               //item.CPCongViecKhongXDDuocTuTK = string.IsNullOrEmpty(item.MaNoiDungCongViec) ? 0 : Math.Round((item.DonGia_VL.Value + item.DonGia_NC.Value + item.DonGia_MTC.Value) * decimal.Parse(cpCVKXD) / 100, 0); ; //14     

                item.CPCongViecKhongXDDuocTuTK = 0; //14     
                if (listMaChietTinh.Any(prefix => item.MaNoiDungCongViec.ToUpper().StartsWith(prefix)))
                {
                    item.CPCongViecKhongXDDuocTuTK = string.IsNullOrEmpty(item.MaNoiDungCongViec) ? 0 : Math.Round((item.DonGia_VL.Value + item.DonGia_NC.Value + item.DonGia_MTC.Value) * decimal.Parse(cpCVKXD) / 100, 0);//14     
                }

                if (!string.IsNullOrEmpty(item.MaNoiDungCongViec) && listMaChietTinh.Any(prefix => item.MaNoiDungCongViec.ToUpper().StartsWith(prefix)))
                {
                    item.ThuNhapChiuThue = string.IsNullOrEmpty(item.MaNoiDungCongViec) ? 0 : Math.Round((
                        item.DonGia_VL.Value + item.DonGia_NC.Value + item.DonGia_MTC.Value +
                        item.CPChung.Value + item.CPChung2.Value
                        + item.CPChung3.Value + item.CPNhaTam.Value + item.CPCongViecKhongXDDuocTuTK.Value) * decimal.Parse(tnct) / 100, 0);

                }
                else
                {
                    item.ThuNhapChiuThue = 0;
                }

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



            result.ChuaCoDuLieu = chuaCoDuLieu;
            result.DonGiaThu7 = congViecChinh.SoLuong == 0 ? 0 : Math.Round((result.CongTruocThue - (donGiaVatLieu + (donGiaNhanCong * (decimal)1.06))) / congViecChinh.SoLuong.Value, 0);

            if (bieuGiaTongHop != null && bieuGiaTongHop.TinhTrang < 2)
            {
                bieuGiaTongHop.DonGia = result.DonGiaThu5;
                bieuGiaTongHop.DonGia2 = result.DonGiaThu6;
                bieuGiaTongHop.DonGia3 = result.DonGiaThu7;
                _unitOfWork.BieuGiaTongHop_CapNgamRepository.Update(bieuGiaTongHop);
                await _unitOfWork.SaveChangesAsync();
            }

            // cấu hình
            result.CPChung1 = cpChung1;
            result.CPChung2 = cpChung2;
            result.CPChung3 = cpChung3;

            result.CPCVKXD = cpCVKXD;
            result.TNCT = tnct;

            if (chuaCoDuLieu)
            {
                var checkDataExist = await _unitOfWork.ChiTietBieuGia_CapNgamRepository.GetQuery(x => x.IDBieuGia == request.IdBieuGia)
                    .AsNoTracking().FirstOrDefaultAsync();
                if (checkDataExist == null)
                {
                    result.ChuaCoDuLieuBieuGia = true;

                }
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
