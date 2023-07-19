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
    public class SyncChiTietBieuGiaCommand : IRequest<bool>
    {
        public int Nam { get; set; }
        public int Quy { get; set; }
        public int LoaiDongBo { get; set; }
    }
    public class SyncChiTietBieuGiaCommandHandler : IRequestHandler<SyncChiTietBieuGiaCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        public SyncChiTietBieuGiaCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<bool> Handle(SyncChiTietBieuGiaCommand request, CancellationToken cancellationToken)
        {
            var bieuGiaCu = await _unitOfWork.BieuGiaTongHopRepository.GetQuery(x => x.TinhTrang == TinhTrangEnum.DaDuyet.GetHashCode()).OrderByDescending(x => x.Nam).ThenByDescending(x => x.Quy).AsNoTracking().FirstOrDefaultAsync();

            if (request.LoaiDongBo == 1)
            {
                var chiTietBieuGiaCu = await _unitOfWork.ChiTietBieuGiaRepository.GetQuery(x => x.Nam == bieuGiaCu.Nam && x.Quy == bieuGiaCu.Quy).AsNoTracking().ToListAsync();
                var listChiTietBieuGia = new List<ChiTietBieuGia>();
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
                    var checkExist = await _unitOfWork.ChiTietBieuGiaRepository
                        .FindOneAsync(x => x.IDBieuGia == item.IDBieuGia && x.IDCongViec == item.IDCongViec && x.Nam == item.Nam && x.Quy == item.Quy);
                    if (checkExist == null)
                    {
                        listChiTietBieuGia.Add(item);

                    }

                }
                _unitOfWork.ChiTietBieuGiaRepository.AddRange(listChiTietBieuGia);

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

                _unitOfWork.BieuGiaTongHopRepository.Add(bieuGiaCu);
            }
            else if (request.LoaiDongBo == 2)
            {
                if (bieuGiaCu.Nam != request.Nam || bieuGiaCu.Quy != request.Quy)
                {
                    throw new EvnException($"Quý {request.Quy} năm {request.Nam} chưa có dữ liệu vui lòng đồng bộ dữ liệu của quý trước khi thực hiện thay đổi đơn giá");
                }

                var chiTietBieuGiaCu = await _unitOfWork.ChiTietBieuGiaRepository.GetQuery(x => x.Nam == bieuGiaCu.Nam && x.Quy == bieuGiaCu.Quy).AsNoTracking().ToListAsync();
                var listIdBieuGiaCu = chiTietBieuGiaCu.Select(x => x.IDBieuGia).Distinct().ToList();
                var listBieuGiaCongViec = await _unitOfWork.BieuGiaCongViecRepository.GetQuery(x => listIdBieuGiaCu.Contains(x.IdBieuGia))
                    .Include(x => x.DM_CongViec).AsNoTracking().ToListAsync();

                var listDonGiaCap = await _unitOfWork.GiaCapRepository.GetQuery().GroupBy(x => x.IdLoaiCap).Select(x => x.OrderByDescending(y => y.CreatedDate).First())
                    .Include(z => z.DM_LoaiCap).AsNoTracking().ToListAsync();

                var listDonGiaChietTinh = await _unitOfWork.DonGiaChietTinhRepository.GetQuery()
                    .GroupBy(x => x.IdCongViec).Select(x => x.OrderByDescending(y => y.CreatedDate).First()).Include(z => z.DM_CongViec).AsNoTracking().ToListAsync();

                var listDonGiaVatLieu = await _unitOfWork.DonGiaVatLieuRepository.GetQuery()
                    .GroupBy(x => x.IdVatLieu).Select(x => x.OrderByDescending(y => y.CreatedDate).First()).Include(z => z.DM_VatLieu).AsNoTracking().ToListAsync();

                var listChiTietBieuGia = new List<ChiTietBieuGia>();

                foreach (var item in chiTietBieuGiaCu)
                {
                    var cap = listBieuGiaCongViec.Where(x => x.IdCongViec == item.IDCongViec && x.IdBieuGia == item.IDBieuGia).FirstOrDefault();
                    if (cap.CongViecChinh)
                    {
                        var giaCap = listDonGiaCap.Where(x => x.DM_LoaiCap.MaLoaiCap.Trim() == cap.DM_CongViec.MaCongViec.Trim()).FirstOrDefault()?.DonGia;
                        item.DonGia_VL = giaCap.Value;
                    }
                    else if (!string.IsNullOrEmpty(item.DM_CongViec.MaCongViec) && item.DM_CongViec.MaCongViec.ToUpper().StartsWith("D"))
                    {
                        var donGiaCT = listDonGiaChietTinh.Where(x => x.IdCongViec == item.IDCongViec).FirstOrDefault();
                        item.DonGia_VL = donGiaCT.DonGiaVatLieu ?? item.DonGia_VL;
                        item.DonGia_NC = donGiaCT.DonGiaNhanCong ?? item.DonGia_NC;
                        item.DonGia_MTC = donGiaCT.DonGiaMTC ?? item.DonGia_MTC;
                    }
                    else
                    {
                        var vatLieu = listDonGiaVatLieu.Where(x => x.IdVatLieu == item.IDCongViec).FirstOrDefault();
                        item.DonGia_VL = vatLieu?.DonGia ?? item.DonGia_VL;
                    }

                    _unitOfWork.ChiTietBieuGiaRepository.Update(item);
                }
            }

            await _unitOfWork.SaveChangesAsync();
            return true;


        }
    }
}
