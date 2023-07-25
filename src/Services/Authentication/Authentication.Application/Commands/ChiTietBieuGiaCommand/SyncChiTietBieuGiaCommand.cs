using Authentication.Application.Model.ChiTietBieuGia;
using Authentication.Infrastructure.AggregatesModel.BieuGiaTongHopAggregate;
using Authentication.Infrastructure.AggregatesModel.ChiTietBieuGiaAggregate;
using Authentication.Infrastructure.AggregatesModel.DonGiaVatLieuAggregate;
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
        public int? Nam { get; set; }
        public int? Quy { get; set; }
    }
    public class SyncChiTietBieuGiaCommandHandler : IRequestHandler<SyncChiTietBieuGiaCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMediator _mediator;
        public SyncChiTietBieuGiaCommandHandler(IUnitOfWork unitOfWork, IMediator mediator)
        {
            _unitOfWork = unitOfWork;
            _mediator = mediator;
        }
        public async Task<bool> Handle(SyncChiTietBieuGiaCommand request, CancellationToken cancellationToken)
        {


            if (request.Quy == null && request.Nam == null)
            {
                var bieuGiaCu = await _unitOfWork.BieuGiaTongHopRepository.GetQuery(x => x.TinhTrang == 4).OrderByDescending(x => x.Nam).ThenByDescending(x => x.Quy).AsNoTracking().FirstOrDefaultAsync();

                var namCu = bieuGiaCu.Nam;
                var quyCu = bieuGiaCu.Quy;

                var chiTietBieuGiaCu = await _unitOfWork.ChiTietBieuGiaRepository.GetQuery(x => x.Nam == namCu && x.Quy == quyCu).AsNoTracking().ToListAsync();
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

                var listBieuGiaCu = await _unitOfWork.BieuGiaTongHopRepository.GetQuery(x => x.Quy == quyCu && x.Nam == namCu).ToListAsync();

                foreach (var bg in listBieuGiaCu)
                {
                    bg.Id = Guid.NewGuid();
                    bg.TinhTrang = TinhTrangEnum.TaoMoi.GetHashCode();
                    if (bg.Quy == 4)
                    {
                        bg.Quy = 1;
                        bg.Nam += 1;
                    }
                    else
                    {
                        bg.Quy += 1;
                    }

                    _unitOfWork.BieuGiaTongHopRepository.Add(bg);
                }

            }
            else
            {
                var bieuGiaCu = await _unitOfWork.BieuGiaTongHopRepository.GetQuery(x => x.TinhTrang == 0).OrderByDescending(x => x.Nam).ThenByDescending(x => x.Quy).AsNoTracking().FirstOrDefaultAsync();

                if (bieuGiaCu.Nam != request.Nam || bieuGiaCu.Quy != request.Quy)
                {
                    throw new EvnException($"Quý {request.Quy} năm {request.Nam} chưa có dữ liệu vui lòng đồng bộ dữ liệu của quý trước khi thực hiện thay đổi đơn giá");
                }

                var chiTietBieuGiaCu = await _unitOfWork.ChiTietBieuGiaRepository.GetQuery(x => x.Nam == bieuGiaCu.Nam && x.Quy == bieuGiaCu.Quy)
                    .Include(x => x.DM_BieuGia).ThenInclude(x => x.DM_LoaiBieuGia).ToListAsync();
                var listIdBieuGiaCu = chiTietBieuGiaCu.Select(x => x.IDBieuGia).Distinct().ToList();
                var listBieuGiaCongViec = await _unitOfWork.BieuGiaCongViecRepository.GetQuery(x => listIdBieuGiaCu.Contains(x.IdBieuGia))
                    .Include(x => x.DM_CongViec).AsNoTracking().ToListAsync();

                var listDonGiaCap = await _unitOfWork.GiaCapRepository.GetQuery().Include(z => z.DM_LoaiCap).GroupBy(x => x.IdLoaiCap).Select(x => x.OrderByDescending(y => y.CreatedDate).First())
                    .AsNoTracking().ToListAsync();

                var listDonGiaChietTinh = await _unitOfWork.DonGiaChietTinhRepository.GetQuery().Include(z => z.DM_CongViec)
                    .GroupBy(x => new { x.IdCongViec, x.VungKhuVuc }).Select(x => x.OrderByDescending(y => y.CreatedDate).First()).AsNoTracking().ToListAsync();

                var listDonGiaVatLieu = await _unitOfWork.DonGiaVatLieuRepository.GetQuery().Include(z => z.DM_VatLieu)
                    .GroupBy(x => x.IdVatLieu).Select(x => x.OrderByDescending(y => y.CreatedDate).First()).AsNoTracking().ToListAsync();

                var listChiTietBieuGia = new List<ChiTietBieuGia>();

                foreach (var item in chiTietBieuGiaCu)
                {
                    var listMaChietTinh = new List<string>() { "D4", "D3", "03", "05", "11" }; // Các công việc chiết tính

                    var congViec = listBieuGiaCongViec.Where(x => x.IdCongViec == item.IDCongViec && x.IdBieuGia == item.IDBieuGia).FirstOrDefault();
                    if(congViec == null)
                    {
                        Console.WriteLine(item.IDCongViec);
                        Console.WriteLine(item.IDBieuGia);
                    }
                    if (congViec.CongViecChinh)
                    {
                        var giaCap = listDonGiaCap.Where(x => x.DM_LoaiCap.MaLoaiCap.Trim() == congViec.DM_CongViec.MaCongViec.Trim()).FirstOrDefault()?.DonGia;
                        if (giaCap == null)
                        {
                            Console.WriteLine(congViec.DM_CongViec.MaCongViec);
                        }
                        item.DonGia_VL = giaCap.Value;
                    }
                    else if (!string.IsNullOrEmpty(congViec.DM_CongViec.MaCongViec) && listMaChietTinh.Any(x => congViec.DM_CongViec.MaCongViec.ToUpper().StartsWith(x)))
                    {
                        var donGiaCT = listDonGiaChietTinh.Where(x => x.IdCongViec == item.IDCongViec && x.VungKhuVuc.ToString() == item.DM_BieuGia.DM_LoaiBieuGia.MaLoaiBieuGia).FirstOrDefault();
                        
                        if(donGiaCT == null)
                        {
                            Console.WriteLine(item.IDCongViec);
                            Console.WriteLine(item.DM_BieuGia.DM_LoaiBieuGia.DM_KhuVuc.GhiChu);

                        }
                        item.DonGia_VL = donGiaCT.DonGiaVatLieu ?? item.DonGia_VL;
                        item.DonGia_NC = donGiaCT.DonGiaNhanCong ?? item.DonGia_NC;
                        item.DonGia_MTC = donGiaCT.DonGiaMTC ?? item.DonGia_MTC;
                    }
                    else
                    {
                        var vatLieu = listDonGiaVatLieu.Where(x => x.DM_VatLieu.MaVatLieu != null && (x.DM_VatLieu.MaVatLieu.Trim() == congViec.DM_CongViec.MaCongViec.Trim())).FirstOrDefault();
                        if (vatLieu == null)
                        {
                            Console.WriteLine(congViec.DM_CongViec.MaCongViec);

                        }
                        item.DonGia_VL = vatLieu?.DonGia ?? item.DonGia_VL;
                    }

                    _unitOfWork.ChiTietBieuGiaRepository.Update(item);
                    await _unitOfWork.SaveChangesAsync();
                }

                foreach (var bg in listIdBieuGiaCu)
                {
                    var updateBieuGiaTongHop = await _mediator.Send(new GetListChiTietBieuGiaCommand { IdBieuGia = bg.Value, Quy = bieuGiaCu.Quy, Nam = bieuGiaCu.Nam });
                }


            }

            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}
