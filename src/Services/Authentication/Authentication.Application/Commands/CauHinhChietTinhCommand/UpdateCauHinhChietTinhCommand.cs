using Authentication.Infrastructure.AggregatesModel.DonGiaChietTinhAggregate;
using Authentication.Infrastructure.Properties;
using Authentication.Infrastructure.Repositories;
using EVN.Core.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static EVN.Core.Common.AppEnum;

namespace Authentication.Application.Commands.CauHinhChietTinhCommand
{
    public class UpdateCauHinhChietTinhCommand : IRequest<bool>
    {
        public Guid IdCongViec { get; set; }
        public List<Guid> IdVatLieu { get; set; }
        public List<Guid> IdNhanCong { get; set; }
        public List<Guid> IdMTC { get; set; }
    }

    //Tạo thêm 1 class Handler kế thừa IRequestHandler<UpdateCauHinhChietTinhCommand, bool> rồi implement
    public class UpdateCauHinhChietTinhCommandHandler : IRequestHandler<UpdateCauHinhChietTinhCommand, bool> //
    {
        private readonly IUnitOfWork _unitOfWork; // khai báo 
        public UpdateCauHinhChietTinhCommandHandler(IUnitOfWork unitOfWork) //cấu hình dependence
        {
            _unitOfWork = unitOfWork; // khai báo
        }
        public async Task<bool> Handle(UpdateCauHinhChietTinhCommand request, CancellationToken cancellationToken)
        {


            // tìm kiếm xem có ID trong bảng CauHinhChietTinh không
            var listData = await _unitOfWork.CauHinhChietTinhRepository.GetQuery(x => x.IdCongViec == request.IdCongViec).ToListAsync();
            foreach (var item in listData)
            {
                item.IsDeleted = true;
                _unitOfWork.CauHinhChietTinhRepository.Update(item);
            }

            var listCauHinh = new List<CauHinhChietTinh>();
            var listVatLieu = await _unitOfWork.DM_VatLieuRepository.GetQuery().ToListAsync();

            if (request.IdVatLieu.Any())
            {
                foreach (var item in request.IdVatLieu)
                {
                    var thuTu = listVatLieu.Where(x => x.Id == item).FirstOrDefault();

                    listCauHinh.Add(new CauHinhChietTinh { ThuTuHienThi = thuTu.ThuTuHienThi, IdCongViec = request.IdCongViec, IdChiTiet = item, PhanLoai = PhanLoaiChietTinhEnum.VatLieu.GetHashCode() });
                }
            }
            if (request.IdNhanCong.Any())
            {
                foreach (var item in request.IdNhanCong)
                {
                    listCauHinh.Add(new CauHinhChietTinh { IdCongViec = request.IdCongViec, IdChiTiet = item, PhanLoai = PhanLoaiChietTinhEnum.NhanCong.GetHashCode() });
                }
            }
            if (request.IdMTC.Any())
            {
                foreach (var item in request.IdMTC)
                {
                    listCauHinh.Add(new CauHinhChietTinh { IdCongViec = request.IdCongViec, IdChiTiet = item, PhanLoai = PhanLoaiChietTinhEnum.MTC.GetHashCode() });
                }
            }
            if (listCauHinh.Any())
            {
                _unitOfWork.CauHinhChietTinhRepository.AddRange(listCauHinh);
            }

            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}
