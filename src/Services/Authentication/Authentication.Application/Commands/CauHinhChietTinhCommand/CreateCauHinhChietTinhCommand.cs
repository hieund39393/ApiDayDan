using Authentication.Infrastructure.AggregatesModel.DonGiaChietTinhAggregate;
using Authentication.Infrastructure.Repositories;
using EVN.Core.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static EVN.Core.Common.AppEnum;

namespace Authentication.Application.Commands.CauHinhChietTinhCommand
{
    public class CreateCauHinhChietTinhCommand : IRequest<bool>
    {
        public Guid IdCongViec { get; set; }
        public List<Guid> IdVatLieu { get; set; }
        public List<Guid> IdNhanCong { get; set; }
        public List<Guid> IdMTC { get; set; }
    }

    //Tạo thêm 1 class Handler kế thừa IRequestHandler<CreateCauHinhChietTinhCommand, bool> rồi implement
    public class CreateCauHinhChietTinhCommandHandler : IRequestHandler<CreateCauHinhChietTinhCommand, bool> //
    {
        private readonly IUnitOfWork _unitOfWork; // khai báo 
        public CreateCauHinhChietTinhCommandHandler(IUnitOfWork unitOfWork) //cấu hình dependence
        {
            _unitOfWork = unitOfWork; // khai báo
        }
        public async Task<bool> Handle(CreateCauHinhChietTinhCommand request, CancellationToken cancellationToken)
        {
            // tìm kiếm xem có trùng trong db không

            var checkExist = await _unitOfWork.CauHinhChietTinhRepository.GetQuery(x => x.IdCongViec == request.IdCongViec).FirstOrDefaultAsync();
            if (checkExist != null)
            {
                throw new EvnException("Công việc đã tồn tại");
            }
            var listCauHinh = new List<CauHinhChietTinh>();
            if (request.IdVatLieu.Any())
            {
                foreach (var item in request.IdVatLieu)
                {
                    listCauHinh.Add(new CauHinhChietTinh { IdCongViec = request.IdCongViec, IdChiTiet = item, PhanLoai = PhanLoaiChietTinhEnum.VatLieu.GetHashCode() });
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
