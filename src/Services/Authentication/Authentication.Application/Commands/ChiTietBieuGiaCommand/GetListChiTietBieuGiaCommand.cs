using Authentication.Application.Model.ChiTietBieuGia;
using Authentication.Infrastructure.Repositories;
using EVN.Core.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static EVN.Core.Common.AppEnum;

namespace Authentication.Application.Commands.ChiTietBieuGiaCommand
{
    public class GetListChiTietBieuGiaCommand : IRequest<ChiTietBieuGiaResult>
    {
        public int Quy { get; set; }
        public int Nam { get; set; }
        public Guid IdBieuGia { get; set; }
    }

    public class GetListChiTietBieuGiaCommandHandler : IRequestHandler<GetListChiTietBieuGiaCommand, ChiTietBieuGiaResult>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetListChiTietBieuGiaCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ChiTietBieuGiaResult> Handle(GetListChiTietBieuGiaCommand request, CancellationToken cancellationToken)
        {
            var cauHinh = await _unitOfWork.CauHinhBieuGiaRepository.GetQuery(x => x.PhanLoaiCap == 1).ToListAsync();
            var cpChung = cauHinh.Where(x => x.TenCauHinh == TenCauHinhEnum.CH1.GetHashCode().ToString() && x.Quy == request.Quy && x.Nam == request.Nam)
                .OrderByDescending(x => x.CreatedDate).FirstOrDefault()?.GiaTri;
            var cpNhaTam = cauHinh.Where(x => x.TenCauHinh == TenCauHinhEnum.CH2.GetHashCode().ToString()
            && x.Quy == request.Quy && x.Nam == request.Nam)
                .OrderByDescending(x => x.CreatedDate).FirstOrDefault()?.GiaTri;
            var cpCVKXD = cauHinh.Where(x => x.TenCauHinh == TenCauHinhEnum.CH3.GetHashCode().ToString()
                && x.Quy == request.Quy && x.Nam == request.Nam)
                .OrderByDescending(x => x.CreatedDate)
            .FirstOrDefault()?.GiaTri;
            var tnct = cauHinh.Where(x => x.TenCauHinh == TenCauHinhEnum.CH4.GetHashCode().ToString()
            && x.Quy == request.Quy && x.Nam == request.Nam)
                .OrderByDescending(x => x.CreatedDate).FirstOrDefault()?.GiaTri;

            if (cpChung == null/* || cpNhaTam == null*/ || cpCVKXD == null || tnct == null)
            {
                throw new EvnException("Chưa có cấu hình biểu giá");
            }

            var chuaCoDuLieu = false;

            var quyTruoc = request.Quy == 1 ? 4 : request.Quy - 1;
            var namTruoc = request.Quy == 1 ? request.Nam - 1 : request.Nam;

            var result = new ChiTietBieuGiaResult();
            decimal soLuongCVC = 0;

            //Tạo câu query
            var query = await _unitOfWork.BieuGiaCongViecRepository.GetQuery(x => x.IdBieuGia == request.IdBieuGia)
                .Include(x => x.DM_BieuGia.ChiTietBieuGia.Where(x => x.Quy == request.Quy && x.Nam == request.Nam)).Include(x => x.DM_CongViec)
                .Include(x => x.DM_BieuGia).ThenInclude(y => y.DM_LoaiBieuGia).ThenInclude(z => z.DM_KhuVuc)
                .AsNoTracking()
                .Select(x => new ChiTietBieuGiaResponse
                {
                    VungKhuVuc = x.DM_BieuGia.DM_LoaiBieuGia.DM_KhuVuc.GhiChu,
                    Id = x.DM_BieuGia.ChiTietBieuGia.FirstOrDefault(y => y.IDCongViec == x.IdCongViec && y.Nam == request.Nam && y.Quy == request.Quy).Id,
                    IdBieuGia = x.DM_BieuGia.Id,
                    MaNoiDungCongViec = x.DM_CongViec.MaCongViec,
                    NoiDungCongViec = x.DM_CongViec.TenCongViec,
                    DonViTinh = x.DM_CongViec.DonViTinh,
                    IdCongViec = x.DM_CongViec.Id,
                    Nam = request.Nam,
                    Quy = request.Quy,
                    ThuTuHienThi = x.ThuTuHienThi,
                    SoLuong = Math.Round(x.DM_BieuGia.ChiTietBieuGia.FirstOrDefault(y => y.IDCongViec == x.IdCongViec && y.Nam == request.Nam && y.Quy == request.Quy) != null
                    ? x.DM_BieuGia.ChiTietBieuGia.FirstOrDefault(y => y.IDCongViec == x.IdCongViec && y.Nam == request.Nam && y.Quy == request.Quy).SoLuong :
                     x.DM_BieuGia.ChiTietBieuGia.FirstOrDefault(y => y.IDCongViec == x.IdCongViec && y.Nam == namTruoc && y.Quy == quyTruoc).SoLuong, 2), //0

                    HeSoDieuChinh_K1nc = Math.Round(x.DM_BieuGia.ChiTietBieuGia.FirstOrDefault(y => y.IDCongViec == x.IdCongViec && y.Nam == request.Nam && y.Quy == request.Quy) != null
                    ? x.DM_BieuGia.ChiTietBieuGia.FirstOrDefault(y => y.IDCongViec == x.IdCongViec && y.Nam == request.Nam && y.Quy == request.Quy).HeSoDieuChinh_K1nc :
                    x.DM_BieuGia.ChiTietBieuGia.FirstOrDefault(y => y.IDCongViec == x.IdCongViec && y.Nam == namTruoc && y.Quy == quyTruoc).HeSoDieuChinh_K1nc, 2), //1

                    HeSoDieuChinh_K2nc = Math.Round(x.DM_BieuGia.ChiTietBieuGia.FirstOrDefault(y => y.IDCongViec == x.IdCongViec && y.Nam == request.Nam && y.Quy == request.Quy) != null
                    ? x.DM_BieuGia.ChiTietBieuGia.FirstOrDefault(y => y.IDCongViec == x.IdCongViec && y.Nam == request.Nam && y.Quy == request.Quy).HeSoDieuChinh_K2nc :
                    x.DM_BieuGia.ChiTietBieuGia.FirstOrDefault(y => y.IDCongViec == x.IdCongViec && y.Nam == namTruoc && y.Quy == quyTruoc).HeSoDieuChinh_K2nc, 2), // 1

                    HeSoDieuChinh_Kmtc = Math.Round(x.DM_BieuGia.ChiTietBieuGia.FirstOrDefault(y => y.IDCongViec == x.IdCongViec && y.Nam == request.Nam && y.Quy == request.Quy) != null
                    ? x.DM_BieuGia.ChiTietBieuGia.FirstOrDefault(y => y.IDCongViec == x.IdCongViec && y.Nam == request.Nam && y.Quy == request.Quy).HeSoDieuChinh_Kmtc :
                    x.DM_BieuGia.ChiTietBieuGia.FirstOrDefault(y => y.IDCongViec == x.IdCongViec && y.Nam == namTruoc && y.Quy == quyTruoc).HeSoDieuChinh_Kmtc, 2), // 1

                    //DonGia_VL = Math.Round(x.DM_BieuGia.ChiTietBieuGia.FirstOrDefault(y => y.IDCongViec == x.IdCongViec && y.Nam == request.Nam && y.Quy == request.Quy) != null
                    //? x.DM_BieuGia.ChiTietBieuGia.FirstOrDefault(y => y.IDCongViec == x.IdCongViec && y.Nam == request.Nam && y.Quy == request.Quy).DonGia_VL :
                    //x.DM_BieuGia.ChiTietBieuGia.FirstOrDefault(y => y.IDCongViec == x.IdCongViec && y.Nam == namTruoc && y.Quy == quyTruoc).DonGia_VL, 0), // 0

                    //DonGia_NC = Math.Round(x.DM_BieuGia.ChiTietBieuGia.FirstOrDefault(y => y.IDCongViec == x.IdCongViec && y.Nam == request.Nam && y.Quy == request.Quy) != null
                    //? x.DM_BieuGia.ChiTietBieuGia.FirstOrDefault(y => y.IDCongViec == x.IdCongViec && y.Nam == request.Nam && y.Quy == request.Quy).DonGia_NC :
                    //x.DM_BieuGia.ChiTietBieuGia.FirstOrDefault(y => y.IDCongViec == x.IdCongViec && y.Nam == namTruoc && y.Quy == quyTruoc).DonGia_NC, 0), // 0

                    //DonGia_MTC = Math.Round(x.DM_BieuGia.ChiTietBieuGia.FirstOrDefault(y => y.IDCongViec == x.IdCongViec && y.Nam == request.Nam && y.Quy == request.Quy) != null
                    //? x.DM_BieuGia.ChiTietBieuGia.FirstOrDefault(y => y.IDCongViec == x.IdCongViec && y.Nam == request.Nam && y.Quy == request.Quy).DonGia_MTC :
                    //x.DM_BieuGia.ChiTietBieuGia.FirstOrDefault(y => y.IDCongViec == x.IdCongViec && y.Nam == namTruoc && y.Quy == quyTruoc).DonGia_MTC, 0), // 0

                    DonGia_VL = 0,
                    DonGia_NC = 0,
                    DonGia_MTC = 0,

                    CongViecChinh = x.CongViecChinh,

                    //ChuaCoDuLieu = x.DM_BieuGia.ChiTietBieuGia.FirstOrDefault(y => y.IDCongViec == x.IdCongViec && y.Nam == request.Nam && y.Quy == request.Quy) == null ? true : false,
                    ChuaCoDuLieu = true,
                }).AsSplitQuery()
                .ToListAsync();

            decimal donGiaVatLieu = 0;
            decimal donGiaNhanCong = 0;

            var listDonGiaCap = await _unitOfWork.GiaCapRepository.GetQuery().Include(z => z.DM_LoaiCap).GroupBy(x => x.IdLoaiCap).Select(x => x.OrderByDescending(y => y.CreatedDate).First())
                .AsNoTracking().ToListAsync();

            var listDonGiaChietTinh = await _unitOfWork.DonGiaChietTinhRepository.GetQuery().Include(z => z.DM_CongViec)
                .GroupBy(x => new { x.IdCongViec, x.VungKhuVuc }).Select(x => x.OrderByDescending(y => y.CreatedDate).First()).AsNoTracking().ToListAsync();

            var listDonGiaVatLieu = await _unitOfWork.DonGiaVatLieuRepository.GetQuery().Include(z => z.DM_VatLieu)
                .GroupBy(x => x.IdVatLieu).Select(x => x.OrderByDescending(y => y.CreatedDate).First()).AsNoTracking().ToListAsync();

            var stt = 1;
            var listMaChietTinh = new List<string>() { "D", "03", "05", "11" }; // Các công việc chiết tính
            foreach (var item in query)
            {
                item.SoLuong = item.SoLuong ?? 0;
                item.HeSoDieuChinh_K1nc = item.HeSoDieuChinh_K1nc ?? 1;
                item.HeSoDieuChinh_K2nc = item.HeSoDieuChinh_K2nc ?? 1;
                item.HeSoDieuChinh_Kmtc = item.HeSoDieuChinh_Kmtc ?? 1;


                if (item.CongViecChinh)
                {
                    var giaCap = listDonGiaCap.Where(x => x.DM_LoaiCap.MaLoaiCap.Trim() == item.MaNoiDungCongViec).FirstOrDefault()?.DonGia;
                    item.DonGia_VL = (giaCap ?? 0) * item.HeSoDieuChinh_K1nc;
                    item.DonGia_NC = (item?.DonGia_NC ?? 0) * item.HeSoDieuChinh_K2nc;
                    item.DonGia_MTC = (item?.DonGia_MTC ?? 0) * item.HeSoDieuChinh_Kmtc;
                }
                else if (!string.IsNullOrEmpty(item.MaNoiDungCongViec) && listMaChietTinh.Any(prefix => item.MaNoiDungCongViec.ToUpper().StartsWith(prefix)))
                {
                    var donGiaCT = listDonGiaChietTinh.Where(x => x.IdCongViec == item.IdCongViec && x.VungKhuVuc.ToString() == item.VungKhuVuc).FirstOrDefault();
                    item.DonGia_VL = (donGiaCT?.DonGiaVatLieu ?? 0) * item.HeSoDieuChinh_K1nc;
                    item.DonGia_NC = (donGiaCT?.DonGiaNhanCong ?? 0) * item.HeSoDieuChinh_K2nc;
                    item.DonGia_MTC = (donGiaCT?.DonGiaMTC ?? 0) * item.HeSoDieuChinh_Kmtc;
                }
                else
                {
                    var vatLieu = listDonGiaVatLieu.Where(x => x.DM_VatLieu.MaVatLieu != null && (x.DM_VatLieu.MaVatLieu.Trim() == item.MaNoiDungCongViec.Trim())).FirstOrDefault();
                    item.DonGia_VL = (vatLieu?.DonGia ?? 0) * item.HeSoDieuChinh_K1nc;
                    item.DonGia_NC = (item?.DonGia_NC ?? 0) * item.HeSoDieuChinh_K2nc;
                    item.DonGia_MTC = (item?.DonGia_MTC ?? 0) * item.HeSoDieuChinh_Kmtc;
                }


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
                item.CPChung = string.IsNullOrEmpty(item.MaNoiDungCongViec) ? 0 : Math.Round(decimal.Parse(cpChung) / 100 * (item.DonGia_NC.Value), 0);                                                  //12            
                item.CPNhaTam = 0;             //13             tạm thời không cho công thức        
                                               //item.CPNhaTam = Math.Round((item.DonGia_VL.Value + item.DonGia_NC.Value + item.DonGia_MTC.Value) * (decimal)1.2 / 100, 0);             //13                    

                item.CPCongViecKhongXDDuocTuTK = 0; //14     
                if (listMaChietTinh.Any(prefix => item.MaNoiDungCongViec.ToUpper().StartsWith(prefix)))
                {
                    item.CPCongViecKhongXDDuocTuTK = string.IsNullOrEmpty(item.MaNoiDungCongViec) ? 0 : Math.Round((item.DonGia_VL.Value + item.DonGia_NC.Value + item.DonGia_MTC.Value) * decimal.Parse(cpCVKXD) / 100, 0);//14     
                }

                if (!string.IsNullOrEmpty(item.MaNoiDungCongViec) && listMaChietTinh.Any(prefix => item.MaNoiDungCongViec.ToUpper().StartsWith(prefix)))
                {
                    item.ThuNhapChiuThue = string.IsNullOrEmpty(item.MaNoiDungCongViec) ? 0 : Math.Round((item.DonGia_VL.Value + item.DonGia_NC.Value + item.DonGia_MTC.Value + item.CPChung.Value + item.CPNhaTam.Value + item.CPCongViecKhongXDDuocTuTK.Value) * decimal.Parse(tnct) / 100, 0);
                }
                else
                {
                    item.ThuNhapChiuThue = 0;
                }

                item.DonGiaTruocThue = Math.Round(item.DonGia_VL.Value + item.DonGia_NC.Value + item.DonGia_MTC.Value + item.CPChung.Value + item.CPNhaTam.Value + item.CPCongViecKhongXDDuocTuTK.Value + item.ThuNhapChiuThue.Value, 0); ;
                item.GiaTriTruocThue = Math.Round(item.SoLuong.Value * item.DonGiaTruocThue.Value, 0);
                result.Tong += Math.Round(item.GiaTriTruocThue.Value, 0);
                if (item.CongViecChinh && item.SoLuong > 0)
                {
                    soLuongCVC = item.SoLuong.Value;
                }
                stt++;
            }
            var congViecChinh = query.Where(x => x.CongViecChinh).FirstOrDefault();

            result.ListBieuGia = query.OrderBy(x => x.ThuTuHienThi).ToList();
            result.KhaoSat = 0;
            result.CongTruocThue = result.KhaoSat + result.Tong;
            result.DonGiaTongHopTruocThue = soLuongCVC == 0 ? 0 : Math.Round(result.Tong / soLuongCVC, 0);
            result.DonGiaThu5 = result.DonGiaTongHopTruocThue;
            result.DonGiaThu6 = (congViecChinh == null || congViecChinh.SoLuong.Value == (decimal)0.00) ? 0 : Math.Round((result.CongTruocThue - (congViecChinh.DonGia_VL.Value * congViecChinh.SoLuong.Value)) / congViecChinh.SoLuong.Value, 0);

            var itemLast = new ChiTietBieuGiaResponse();
            itemLast.NoiDungCongViec = "TỔNG CỘNG";
            itemLast.GiaTriTruocThue = result.Tong;

            result.ListBieuGia.Add(itemLast);

            var bieuGiaTongHop = await _unitOfWork.BieuGiaTongHopRepository
                .FindOneAsync(x => x.IdBieuGia == request.IdBieuGia && x.Quy == request.Quy && x.Nam == request.Nam && x.TinhTrang != (int)TinhTrangEnum.DaDuyet);



            result.ChuaCoDuLieu = chuaCoDuLieu;
            result.DonGiaThu7 = congViecChinh.SoLuong == 0 ? 0 : Math.Round((result.CongTruocThue - (donGiaVatLieu + (donGiaNhanCong * (decimal)1.06))) / congViecChinh.SoLuong.Value, 0);

            if (bieuGiaTongHop != null)
            {
                bieuGiaTongHop.DonGia = result.DonGiaThu5;
                bieuGiaTongHop.DonGia2 = result.DonGiaThu6;
                bieuGiaTongHop.DonGia3 = result.DonGiaThu7;
                _unitOfWork.BieuGiaTongHopRepository.Update(bieuGiaTongHop);
                await _unitOfWork.SaveChangesAsync();
            }

            // cấu hình
            result.CPChung = cpChung;
            result.CPNhaTam = cpNhaTam;
            result.CPCVKXD = cpCVKXD;
            result.TNCT = tnct;

            if (chuaCoDuLieu)
            {
                var checkDataExist = await _unitOfWork.ChiTietBieuGiaRepository.GetQuery(x => x.IDBieuGia == request.IdBieuGia)
                    .AsNoTracking().FirstOrDefaultAsync();
                if (checkDataExist == null)
                {
                    result.ChuaCoDuLieuBieuGia = true;

                }
            }
            return result;

        }
    }
}
