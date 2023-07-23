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
                var donGiaCu = await _unitOfWork.DonGiaChietTinhRepository.GetQuery(z => z.IdCongViec == item.IdCongViec)
                    .OrderByDescending(x => x.CreatedDate).FirstOrDefaultAsync();
                var entity = new DonGiaChietTinh();

                entity.IdCongViec = item.IdCongViec;
                entity.DonGiaVatLieu = 0;
                entity.DonGiaMTC = 0;

                if (item.DonGia.First().VungKhuVuc == 1)
                {
                    entity.DonGiaNhanCong = 0;
                    entity.DonGiaNhanCongHai = donGiaCu?.DonGiaNhanCongHai;
                    entity.DonGiaNhanCongBa = donGiaCu?.DonGiaNhanCongBa;
                    entity.DinhMucHai = donGiaCu?.DinhMucHai;
                    entity.DinhMucBa = donGiaCu?.DinhMucBa;

                    foreach (var dg in item.DonGia)
                    {
                        if (dg.PhanLoai == 1)
                        {
                            entity.DonGiaVatLieu += (dg.DinhMuc * dg.DGVL);
                        }
                        else if (dg.PhanLoai == 2)
                        {
                            entity.DonGiaNhanCong += (dg.DinhMuc * dg.DGNC);
                            entity.DinhMuc = dg.DinhMuc;

                        }
                        else if (dg.PhanLoai == 3)
                        {
                            entity.DonGiaMTC += (dg.DinhMuc * dg.DGMTC);
                        }
                    }
                }
                else if (item.DonGia.First().VungKhuVuc == 2)
                {
                    entity.DonGiaNhanCongHai = 0;
                    entity.DonGiaNhanCong = donGiaCu?.DonGiaNhanCong;
                    entity.DonGiaNhanCongBa = donGiaCu?.DonGiaNhanCongBa;
                    entity.DinhMuc = donGiaCu?.DinhMuc;
                    entity.DinhMucBa = donGiaCu?.DinhMucBa;
                    foreach (var dg in item.DonGia)
                    {
                        if (dg.PhanLoai == 1)
                        {
                            entity.DonGiaVatLieu += (dg.DinhMuc * dg.DGVL);
                        }
                        else if (dg.PhanLoai == 2)
                        {
                            entity.DonGiaNhanCongHai += (dg.DinhMuc * dg.DGNC);
                            entity.DinhMucHai = dg.DinhMuc;
                        }
                        else if (dg.PhanLoai == 3)
                        {
                            entity.DonGiaMTC += (dg.DinhMuc * dg.DGMTC);
                        }
                    }
                }
                else
                {
                    entity.DonGiaNhanCong = donGiaCu?.DonGiaNhanCong;
                    entity.DonGiaNhanCongHai = donGiaCu?.DonGiaNhanCongHai;
                    entity.DonGiaNhanCongBa = 0;
                    entity.DinhMuc = donGiaCu?.DinhMuc;
                    entity.DinhMucHai = donGiaCu?.DinhMucHai;
                    foreach (var dg in item.DonGia)
                    {
                        if (dg.PhanLoai == 1)
                        {
                            entity.DonGiaVatLieu += (dg.DinhMuc * dg.DGVL);
                        }
                        else if (dg.PhanLoai == 2)
                        {
                            entity.DonGiaNhanCongBa += (dg.DinhMuc * dg.DGNC);
                            entity.DinhMucBa = dg.DinhMuc;
                        }
                        else if (dg.PhanLoai == 3)
                        {
                            entity.DonGiaMTC += (dg.DinhMuc * dg.DGMTC);
                        }
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
                _unitOfWork.DonGiaChietTinhRepository.Add(entity);
                //}

            }

            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}
