using Authentication.Application.Model.DonGiaChietTinh;
using Authentication.Infrastructure.AggregatesModel.DonGiaChietTinhAggregate;
using Authentication.Infrastructure.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Authentication.Application.Commands.DonGiaChietTinhCommand
{
    public class UpdateDonGiaChietTinhCommand : IRequest<bool> // kế thừa IRequest<bool>
    {
        public List<DonGiaChietTinhResponse> DonGia { get; set; }
    }

    //Tạo thêm 1 class Handler kế thừa IRequestHandler<UpdateDonGiaChietTinhCommand, bool> rồi implement
    public class UpdateDonGiaChietTinhCommandHandler : IRequestHandler<UpdateDonGiaChietTinhCommand, bool> //
    {
        private readonly IUnitOfWork _unitOfWork; // khai báo 
        public UpdateDonGiaChietTinhCommandHandler(IUnitOfWork unitOfWork) //cấu hình dependence
        {
            _unitOfWork = unitOfWork; // khai báo
        }
        public async Task<bool> Handle(UpdateDonGiaChietTinhCommand request, CancellationToken cancellationToken)
        {

            var data = request.DonGia.GroupBy(x => x.IdCongViec).Select(x => new { IdCongViec = x.Key, DonGia = x.Where(y => y.Level == 3).ToList() });

            foreach (var item in data)
            {
                var chiTiet = new List<ChietTinhChiTiet>();
                var donGiaCu = await _unitOfWork.DonGiaChietTinhRepository.GetQuery(z => z.IdCongViec == item.IdCongViec)
                    .OrderByDescending(x => x.CreatedDate).FirstOrDefaultAsync();
                var entity = new DonGiaChietTinh();

                entity.IdCongViec = item.IdCongViec;
                entity.DonGiaVatLieu = 0;
                entity.DonGiaMTC = 0;

                entity.DonGiaNhanCong = 0;

                foreach (var dg in item.DonGia)
                {
                    if (dg.PhanLoai == 1)
                    {
                        entity.DonGiaVatLieu += (dg.DinhMuc * dg.DGVL);
                    }
                    else if (dg.PhanLoai == 2)
                    {
                        entity.DonGiaNhanCong += (dg.DinhMuc * dg.DGNC);
                    }
                    else if (dg.PhanLoai == 3)
                    {
                        entity.DonGiaMTC += (dg.DinhMuc * dg.DGMTC);
                    }
                    chiTiet.Add(new ChietTinhChiTiet
                    {
                        DinhMuc = dg.DinhMuc,
                        IdChiTiet = dg.IdVatLieu,
                        IdCongViec = entity.IdCongViec,
                        PhanLoai = dg.PhanLoai,
                    });
                }

                entity.VungKhuVuc = item.DonGia.First().VungKhuVuc;

                _unitOfWork.DonGiaChietTinhRepository.Add(entity);

                if (chiTiet.Any())
                {
                    foreach (var ct in chiTiet)
                    {
                        ct.IdDonGiaChietTinh = entity.Id;
                        _unitOfWork.ChietTinhChiTietRepository.Add(ct);
                    }
                }


            }

            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}
