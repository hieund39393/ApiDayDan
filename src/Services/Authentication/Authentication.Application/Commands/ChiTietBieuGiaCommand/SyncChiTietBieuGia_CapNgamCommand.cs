using Authentication.Application.Model.ChiTietBieuGia;
using Authentication.Infrastructure.AggregatesModel.BieuGiaTongHopAggregate;
using Authentication.Infrastructure.AggregatesModel.ChiTietBieuGiaAggregate;
using Authentication.Infrastructure.Properties;
using Authentication.Infrastructure.Repositories;
using AutoMapper;
using EVN.Core.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static EVN.Core.Common.AppEnum;

namespace Authentication.Application.Commands.ChiTietBieuGiaCommand
{
    public class SyncChiTietBieuGia_CapNgamCommand : IRequest<bool>
    {
        public int? Nam { get; set; }
        public int? Quy { get; set; }
    }
    public class SyncChiTietBieuGia_CapNgamCommandHandler : IRequestHandler<SyncChiTietBieuGia_CapNgamCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        public SyncChiTietBieuGia_CapNgamCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<bool> Handle(SyncChiTietBieuGia_CapNgamCommand request, CancellationToken cancellationToken)
        {


            if (request.Quy == null && request.Nam == null)
            {
                var bieuGiaCu = await _unitOfWork.BieuGiaTongHop_CapNgamRepository.GetQuery(x => x.TinhTrang == 4).OrderByDescending(x => x.Nam).ThenByDescending(x => x.Quy).AsNoTracking().FirstOrDefaultAsync();

                var chiTietBieuGiaCu = await _unitOfWork.ChiTietBieuGia_CapNgamRepository.GetQuery(x => x.Nam == bieuGiaCu.Nam && x.Quy == bieuGiaCu.Quy).AsNoTracking().ToListAsync();
                var listChiTietBieuGia = new List<ChiTietBieuGia_CapNgam>();
                foreach (var item in chiTietBieuGiaCu)
                {
                    item.Id = Guid.NewGuid();
                    if (item.Quy == 4)
                    {
                        item.Quy = 1;
                        item.Nam += 1;
                    }
                    else
                    {
                        item.Quy += 1;
                    }
                    var checkExist = await _unitOfWork.ChiTietBieuGia_CapNgamRepository
                        .FindOneAsync(x => x.IDBieuGia == item.IDBieuGia && x.IDCongViec == item.IDCongViec && x.Nam == item.Nam && x.Quy == item.Quy);
                    if (checkExist == null)
                    {
                        listChiTietBieuGia.Add(item);

                    }

                }
                _unitOfWork.ChiTietBieuGia_CapNgamRepository.AddRange(listChiTietBieuGia);

                bieuGiaCu.Id = Guid.NewGuid();
                bieuGiaCu.TinhTrang = TinhTrangEnum.TaoMoi.GetHashCode();
                if (bieuGiaCu.Quy == 4)
                {
                    bieuGiaCu.Quy = 1;
                    bieuGiaCu.Nam += 1;
                }
                else
                {
                    bieuGiaCu.Quy += 1;
                }

                var checkExistBGTH = await _unitOfWork.BieuGiaTongHop_CapNgamRepository.GetQuery(x => x.Nam == bieuGiaCu.Nam && x.Quy == bieuGiaCu.Quy).ToListAsync();
                if (checkExistBGTH.Any())
                {
                    foreach (var item in checkExistBGTH)
                    {
                        item.IsDeleted = true;
                    }
                }

                _unitOfWork.BieuGiaTongHop_CapNgamRepository.Add(bieuGiaCu);
            }
            else
            {
                var bieuGiaCu = await _unitOfWork.BieuGiaTongHop_CapNgamRepository.GetQuery(x => x.TinhTrang == 0).OrderByDescending(x => x.Nam).ThenByDescending(x => x.Quy).AsNoTracking().FirstOrDefaultAsync();

                if (bieuGiaCu.Nam != request.Nam || bieuGiaCu.Quy != request.Quy)
                {
                    throw new EvnException($"Quý {request.Quy} năm {request.Nam} chưa có dữ liệu vui lòng đồng bộ dữ liệu của quý trước khi thực hiện thay đổi đơn giá");
                }

                var chiTietBieuGiaCu = await _unitOfWork.ChiTietBieuGia_CapNgamRepository.GetQuery(x => x.Nam == bieuGiaCu.Nam && x.Quy == bieuGiaCu.Quy).AsNoTracking().ToListAsync();
                var listIdBieuGiaCu = chiTietBieuGiaCu.Select(x => x.IDBieuGia).Distinct().ToList();
                var listBieuGiaCongViec = await _unitOfWork.BieuGiaCongViec_CapNgamRepository.GetQuery(x => listIdBieuGiaCu.Contains(x.IdBieuGia))
                    .Include(x => x.DM_CongViec_CapNgam).Include(x => x.DM_BieuGia_CapNgam)
                    .ThenInclude(x => x.DM_LoaiBieuGia_CapNgam).ThenInclude(x=>x.DM_KhuVuc).AsNoTracking().ToListAsync();

                var listDonGiaCap = await _unitOfWork.GiaCap_CapNgamRepository.GetQuery().Include(z => z.DM_LoaiCap_CapNgam)
                    .GroupBy(x => new { x.IdLoaiCap }).Select(x => x.OrderByDescending(y => y.CreatedDate).First())
                    .AsNoTracking().ToListAsync();

                var listDonGiaChietTinh = await _unitOfWork.DonGiaChietTinh_CapNgamRepository.GetQuery().Include(z => z.DM_CongViec_CapNgam)
                    .GroupBy(x => new { x.IdCongViec, x.VungKhuVuc }).Select(x => x.OrderByDescending(y => y.CreatedDate).First()).AsNoTracking().ToListAsync();

                var listDonGiaVatLieu = await _unitOfWork.DonGiaVatLieu_CapNgamRepository.GetQuery().Include(z => z.DM_VatLieu_CapNgam)
                    .GroupBy(x => new { x.IdVatLieu, x.VungKhuVuc }).Select(x => x.OrderByDescending(y => y.CreatedDate).First()).AsNoTracking().ToListAsync();

                var listChiTietBieuGia = new List<ChiTietBieuGia_CapNgam>();

                foreach (var item in chiTietBieuGiaCu)
                {
                    var congViec = listBieuGiaCongViec.Where(x => x.IdCongViec == item.IDCongViec && x.IdBieuGia == item.IDBieuGia).FirstOrDefault();

                    var vungKhuVuc = int.Parse(congViec.DM_BieuGia_CapNgam.DM_LoaiBieuGia_CapNgam.DM_KhuVuc.GhiChu);
                    if (congViec.CongViecChinh)
                    {
                        var giaCap = listDonGiaCap.Where(x => x.DM_LoaiCap_CapNgam.MaLoaiCap.Trim() == congViec.DM_CongViec_CapNgam.MaCongViec.Trim()
                        ).FirstOrDefault()?.DonGia ?? 0;
                        item.DonGia_VL = giaCap;
                    }
                    else if (!string.IsNullOrEmpty(congViec.DM_CongViec_CapNgam.MaCongViec) && congViec.DM_CongViec_CapNgam.MaCongViec.ToUpper().StartsWith("D"))
                    {
                        var donGiaCT = listDonGiaChietTinh.Where(x => x.IdCongViec == item.IDCongViec && x.VungKhuVuc == vungKhuVuc).FirstOrDefault();
                        item.DonGia_VL = donGiaCT?.DonGiaVatLieu ?? item.DonGia_VL;
                        item.DonGia_NC = donGiaCT?.DonGiaNhanCong ?? item.DonGia_NC;
                        item.DonGia_MTC = donGiaCT?.DonGiaMTC ?? item.DonGia_MTC;
                    }
                    else
                    {
                        var vatLieu = listDonGiaVatLieu.Where(x => x.DM_VatLieu_CapNgam.MaVatLieu.Trim() == congViec.DM_CongViec_CapNgam.MaCongViec.Trim()
                        && x.VungKhuVuc == vungKhuVuc
                        ).FirstOrDefault();

                        item.DonGia_VL = vatLieu?.DonGia ?? item.DonGia_VL;
                    }

                    _unitOfWork.ChiTietBieuGia_CapNgamRepository.Update(item);
                }
            }

            await _unitOfWork.SaveChangesAsync();
            return true;


        }
    }
}
