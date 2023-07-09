using Authentication.Infrastructure.AggregatesModel.ChiTietBieuGiaAggregate;
using Authentication.Infrastructure.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static EVN.Core.Common.AppEnum;

namespace Authentication.Application.Commands.ChiTietBieuGiaCommand
{
    public class SyncChiTietBieuGia_CapNgamCommand : IRequest<bool>
    {
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
            var bieuGiaCu = await _unitOfWork.BieuGiaTongHop_CapNgamRepository.GetQuery(x => x.TinhTrang == TinhTrangEnum.DaDuyet.GetHashCode()).OrderByDescending(x => x.Nam).ThenByDescending(x => x.Quy).AsNoTracking().FirstOrDefaultAsync();
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

            _unitOfWork.BieuGiaTongHop_CapNgamRepository.Add(bieuGiaCu);

            await _unitOfWork.SaveChangesAsync();
            return true;


        }
    }
}
