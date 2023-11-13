﻿using Authentication.Application.Model.ChiTietBieuGia;
using Authentication.Infrastructure.AggregatesModel.BieuGiaTongHopAggregate;
using Authentication.Infrastructure.AggregatesModel.ChiTietBieuGiaAggregate;
using Authentication.Infrastructure.Properties;
using Authentication.Infrastructure.Repositories;
using AutoMapper;
using EVN.Core.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml.FormulaParsing.ExpressionGraph.FunctionCompilers;
using System.Net.WebSockets;
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
        private readonly IMediator _mediator;
        public SyncChiTietBieuGia_CapNgamCommandHandler(IUnitOfWork unitOfWork, IMediator mediator)
        {
            _unitOfWork = unitOfWork;
            _mediator = mediator;
        }
        public async Task<bool> Handle(SyncChiTietBieuGia_CapNgamCommand request, CancellationToken cancellationToken)
        {


            if (request.Quy == null && request.Nam == null)
            {
                var bieuGiaCu = await _unitOfWork.BieuGiaTongHop_CapNgamRepository.GetQuery(x => x.TinhTrang == 4).OrderByDescending(x => x.Nam).ThenByDescending(x => x.Quy).AsNoTracking().FirstOrDefaultAsync();

                var namCu = bieuGiaCu.Nam;
                var quyCu = bieuGiaCu.Quy;

                var chiTietBieuGiaCu = await _unitOfWork.ChiTietBieuGia_CapNgamRepository.GetQuery(x => x.Nam == namCu && x.Quy == quyCu).AsNoTracking().ToListAsync();
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


                var listBieuGiaCu = await _unitOfWork.BieuGiaTongHop_CapNgamRepository.GetQuery(x => x.Quy == quyCu && x.Nam == namCu).ToListAsync();

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

                    _unitOfWork.BieuGiaTongHop_CapNgamRepository.Add(bg);
                }
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
                    .ThenInclude(x => x.DM_LoaiBieuGia_CapNgam).ThenInclude(x => x.DM_KhuVuc).AsNoTracking().ToListAsync();

                var listDonGiaCap = await _unitOfWork.GiaCap_CapNgamRepository.GetQuery().Include(z => z.DM_LoaiCap_CapNgam)
                    .GroupBy(x => new { x.IdLoaiCap }).Select(x => x.OrderByDescending(y => y.CreatedDate).First())
                    .AsNoTracking().ToListAsync();

                var listDonGiaChietTinh = await _unitOfWork.DonGiaChietTinh_CapNgamRepository.GetQuery().Include(z => z.DM_CongViec_CapNgam)
                    .GroupBy(x => new { x.IdCongViec, x.VungKhuVuc }).Select(x => x.OrderByDescending(y => y.CreatedDate).First()).AsNoTracking().ToListAsync();

                var listDonGiaVatLieu = await _unitOfWork.DonGiaVatLieu_CapNgamRepository.GetQuery().Include(z => z.DM_VatLieu_CapNgam)
                    .GroupBy(x => new { x.IdVatLieu, x.VungKhuVuc }).Select(x => x.OrderByDescending(y => y.CreatedDate).First()).AsNoTracking().ToListAsync();

                var listChiTietBieuGia = new List<ChiTietBieuGia_CapNgam>();

                int index = 1;
                foreach (var item in chiTietBieuGiaCu)
                {
                    index++;

                    Console.WriteLine(index);

                    if (index == 23)
                    {

                    }
                    var congViec = listBieuGiaCongViec.Where(x => x.IdCongViec == item.IDCongViec && x.IdBieuGia == item.IDBieuGia).FirstOrDefault();
                    if (congViec == null) { continue; } //
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
                await _unitOfWork.SaveChangesAsync();

                foreach (var bg in listIdBieuGiaCu)
                {
                    var updateBieuGiaTongHop = await _mediator.Send(new GetListChiTietBieuGia_CapNgamCommand { IdBieuGia = bg.Value, Quy = bieuGiaCu.Quy, Nam = bieuGiaCu.Nam, UpdateTongHop = true });
                }
            }

            await _unitOfWork.SaveChangesAsync();

            return true;


        }
    }
}
