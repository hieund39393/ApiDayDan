using Authentication.Application.Model.DonGiaChietTinh;
using Authentication.Infrastructure.AggregatesModel.DonGiaChietTinhAggregate;
using Authentication.Infrastructure.Repositories;
using MediatR;

namespace Authentication.Application.Commands.DonGiaChietTinhCommand
{
    public class UpdateDonGiaChietTinh_CapNgamCommand : IRequest<bool> // kế thừa IRequest<bool>
    {
        public List<DonGiaChietTinhResponse> DonGia { get; set; }
    }

    //Tạo thêm 1 class Handler kế thừa IRequestHandler<UpdateDonGiaChietTinh_CapNgamCommand, bool> rồi implement
    public class UpdateDonGiaChietTinh_CapNgamCommandHandler : IRequestHandler<UpdateDonGiaChietTinh_CapNgamCommand, bool> //
    {
        private readonly IUnitOfWork _unitOfWork; // khai báo 
        public UpdateDonGiaChietTinh_CapNgamCommandHandler(IUnitOfWork unitOfWork) //cấu hình dependence
        {
            _unitOfWork = unitOfWork; // khai báo
        }
        public async Task<bool> Handle(UpdateDonGiaChietTinh_CapNgamCommand request, CancellationToken cancellationToken)
        {

            var data = request.DonGia.GroupBy(x => new { x.IdCongViec, x.VungKhuVuc }).Select(x => new { IdCongViec = x.Key.IdCongViec, VungKhuVuc = x.Key.VungKhuVuc, DonGia = x.Where(y => y.Level == 3).ToList() });

            foreach (var item in data)
            {
                var entity = new DonGiaChietTinh_CapNgam();
                entity.IdCongViec = item.IdCongViec;
                entity.DonGiaVatLieu = 0;
                entity.DonGiaNhanCong = 0;
                entity.DonGiaMTC = 0;
                entity.VungKhuVuc = item.VungKhuVuc;
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
                }

                //var checkExist = await _unitOfWork.DonGiaChietTinhRepository.FindOneAsync(x => x.IdCongViec == entity.IdCongViec);
                //if (checkExist != null)
                //{
                //    checkExist.DonGiaVatLieu = entity.DonGiaVatLieu;
                //    checkExist.DonGiaNhanCong = entity.DonGiaNhanCong;
                //    checkExist.DonGiaMTC = entity.DonGiaMTC;
                //    _unitOfWork.DonGiaChietTinhRepository.Update(checkExist);
                //}
                //else
                //{
                _unitOfWork.DonGiaChietTinh_CapNgamRepository.Add(entity);
                //}

            }

            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}
