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
            var bieuGiaCu = await _unitOfWork.BieuGiaTongHopRepository.GetQuery(x=>x.TinhTrang == TinhTrangEnum.DaDuyet.GetHashCode()).OrderByDescending(x => x.Nam).ThenByDescending(x => x.Quy).AsNoTracking().FirstOrDefaultAsync();
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

            await _unitOfWork.SaveChangesAsync();
            return true;


        }
    }
}
