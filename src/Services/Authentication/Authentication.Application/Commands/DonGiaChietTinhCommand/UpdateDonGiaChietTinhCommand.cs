using Authentication.Application.Model.DonGiaChietTinh;
using Authentication.Infrastructure.AggregatesModel.DonGiaChietTinhAggregate;
using Authentication.Infrastructure.Repositories;
using MediatR;

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
            //var listDMDGC = request.DonGia.Where(x => x.IsDonGiaCu || x.IsDinhMucCu).ToList();
            //foreach (var item in listDMDGC)
            //{
            //    if (item.IsDonGiaCu || item.IsDinhMucCu)
            //    {
            //        switch (item.PhanLoai)
            //        {
            //            case 1:
            //                var vlEntity = await _unitOfWork.DonGiaVatLieuRepository.GetQuery(x => x.Id == item.IdVatLieu).OrderByDescending(x => x.CreatedDate).FirstOrDefaultAsync();
            //                if (item.IsDinhMucCu)
            //                {
            //                    vlEntity.DinhMucCu = null;
            //                }
            //                if (item.IsDonGiaCu)
            //                {
            //                    vlEntity.DonGiaCu = null;
            //                }
            //                _unitOfWork.DonGiaVatLieuRepository.Update(vlEntity);
            //                break;
            //            case 2:
            //                var ncEntity = await _unitOfWork.DonGiaNhanCongRepository.GetQuery(x => x.Id == item.IdVatLieu).OrderByDescending(x => x.CreatedDate).FirstOrDefaultAsync();
            //                if (item.IsDinhMucCu)
            //                {
            //                    ncEntity.DinhMucCu = null;
            //                }
            //                if (item.IsDonGiaCu)
            //                {
            //                    ncEntity.DonGiaCu = null;
            //                }
            //                _unitOfWork.DonGiaNhanCongRepository.Update(ncEntity);
            //                break;
            //            case 3:
            //                var mtcEntity = await _unitOfWork.DonGiaMTCRepository.GetQuery(x => x.Id == item.IdVatLieu).OrderByDescending(x => x.CreatedDate).FirstOrDefaultAsync();
            //                if (item.IsDinhMucCu)
            //                {
            //                    mtcEntity.DinhMucCu = null;
            //                }
            //                if (item.IsDonGiaCu)
            //                {
            //                    mtcEntity.DonGiaCu = null;
            //                }
            //                _unitOfWork.DonGiaMTCRepository.Update(mtcEntity);
            //                break;
            //            default:
            //                break;
            //        }
            //    }
            //}

            var data = request.DonGia.GroupBy(x => x.IdCongViec).Select(x => new { IdCongViec = x.Key, DonGia = x.Where(y => y.Level == 3).ToList() });

            foreach (var item in data)
            {
                var entity = new DonGiaChietTinh();
                entity.IdCongViec = item.IdCongViec;
                entity.DonGiaVatLieu = 0;
                entity.DonGiaNhanCong = 0;
                entity.DonGiaMTC = 0;

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
                    _unitOfWork.DonGiaChietTinhRepository.Add(entity);
                //}

            }

            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}
