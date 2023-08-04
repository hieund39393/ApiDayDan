using Authentication.Application.Model.DonGiaChietTinh;
using Authentication.Infrastructure.AggregatesModel.DonGiaChietTinhAggregate;
using Authentication.Infrastructure.Repositories;
using EVN.Core.Common;
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

            var data = request.DonGia.GroupBy(x => new { x.IdCongViec, x.VungKhuVuc }).Select(x => new { IdCongViec = x.Key.IdCongViec, VungKhuVuc = x.Key.VungKhuVuc, DonGia = x.Where(y => y.Level == 3).ToList() }).ToList();

            foreach (var item in data)
            {
                var chiTiet = new List<ChietTinhChiTiet_CapNgam>();
                var entity = new DonGiaChietTinh_CapNgam();
                entity.IdCongViec = item.IdCongViec;
                entity.DonGiaVatLieu = 0;
                entity.DonGiaNhanCong = 0;
                entity.DonGiaMTC = 0;
                entity.VungKhuVuc = item.VungKhuVuc;

                decimal vlKhac = 0;
                decimal mtcKhac = 0;

                foreach (var dg in item.DonGia)
                {
                    if (dg.DinhMuc == null) dg.DinhMuc = 0;
                    if (dg.DGVL == null) dg.DGVL = 0;
                    if (dg.DGNC == null) dg.DGNC = 0;
                    if (dg.DGMTC == null) dg.DGMTC = 0;

                    if (dg.PhanLoai == 1 && dg.Ma != AppConstants.VatLieuKhac)
                    {
                        entity.DonGiaVatLieu += (dg.DinhMuc * dg.DGVL);
                    }
                    else if (dg.PhanLoai == 2)
                    {
                        entity.DonGiaNhanCong += (dg.DinhMuc * dg.DGNC);
                    }
                    else if (dg.PhanLoai == 3 && dg.Ma != AppConstants.MTCKhac)
                    {
                        entity.DonGiaMTC += (dg.DinhMuc * dg.DGMTC);
                    }
                    if (dg.Ma == AppConstants.VatLieuKhac || dg.TenVatLieu.ToLower().Contains("vật liệu khác"))
                    {
                        decimal dinhMuc = dg.DinhMuc.Value;
                        vlKhac = (entity.DonGiaVatLieu.Value * dinhMuc / 100);

                        entity.DonGiaVatLieu += vlKhac;
                    }
                    if (dg.Ma == AppConstants.MTCKhac || dg.TenVatLieu.ToLower().Contains("máy khác"))
                    {
                        mtcKhac = (entity.DonGiaMTC.Value * dg.DinhMuc.Value / 100);
                        entity.DonGiaMTC += mtcKhac;
                    }

                    chiTiet.Add(new ChietTinhChiTiet_CapNgam
                    {
                        DinhMuc = dg.DinhMuc,
                        IdChiTiet = dg.IdVatLieu,
                        IdCongViec = entity.IdCongViec,
                        PhanLoai = dg.PhanLoai,
                        DonGiaKhac = dg.Ma == AppConstants.VatLieuKhac ? vlKhac : (dg.Ma == AppConstants.MTCKhac ? mtcKhac : null)
                    });
                }

                _unitOfWork.DonGiaChietTinh_CapNgamRepository.Add(entity);
                //await _unitOfWork.SaveChangesAsync();
                if (chiTiet.Any())
                {
                    foreach (var ct in chiTiet)
                    {
                        ct.IdDonGiaChietTinh = entity.Id;
                        _unitOfWork.ChietTinhChiTiet_CapNgamRepository.Add(ct);
                    }
                }

            }

            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}
