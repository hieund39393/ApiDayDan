using Authentication.Infrastructure.AggregatesModel.DonGiaChietTinhAggregate;
using Authentication.Infrastructure.Repositories;
using EVN.Core.Exceptions;
using EVN.Core.Helpers;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static EVN.Core.Common.AppEnum;

namespace Authentication.Application.Commands.CauHinhChietTinhCommand
{
    public class CreateCauHinhChietTinh_CapNgamCommand : IRequest<bool>
    {
        public Guid IdCongViec { get; set; }
        public List<Guid> IdVatLieu { get; set; }
        public List<Guid> IdNhanCong { get; set; }
        public List<Guid> IdMTC { get; set; }
        public int VungKhuVuc { get; set; }
    }

    //Tạo thêm 1 class Handler kế thừa IRequestHandler<CreateCauHinhChietTinh_CapNgamCommand, bool> rồi implement
    public class CreateCauHinhChietTinh_CapNgamCommandHandler : IRequestHandler<CreateCauHinhChietTinh_CapNgamCommand, bool> //
    {
        private readonly IUnitOfWork _unitOfWork; // khai báo 
        public CreateCauHinhChietTinh_CapNgamCommandHandler(IUnitOfWork unitOfWork) //cấu hình dependence
        {
            _unitOfWork = unitOfWork; // khai báo
        }
        public async Task<bool> Handle(CreateCauHinhChietTinh_CapNgamCommand request, CancellationToken cancellationToken)
        {
            // tìm kiếm xem có trùng trong db không

            var checkExist = await _unitOfWork.CauHinhChietTinh_CapNgamRepository.GetQuery(x => x.IdCongViec == request.IdCongViec).FirstOrDefaultAsync();
            if (checkExist != null)
            {
                throw new EvnException("Công việc đã tồn tại");
            }
            var listCauHinh = new List<CauHinhChietTinh_CapNgam>();
            if (request.IdVatLieu.Any())
            {
                foreach (var item in request.IdVatLieu)
                {
                    listCauHinh.Add(new CauHinhChietTinh_CapNgam { IdCongViec = request.IdCongViec, IdChiTiet = item, PhanLoai = PhanLoaiChietTinhEnum.VatLieu.GetHashCode(), VungKhuVuc = request.VungKhuVuc });
                }
            }
            if (request.IdNhanCong.Any())
            {
                foreach (var item in request.IdNhanCong)
                {
                    listCauHinh.Add(new CauHinhChietTinh_CapNgam { IdCongViec = request.IdCongViec, IdChiTiet = item, PhanLoai = PhanLoaiChietTinhEnum.NhanCong.GetHashCode(), VungKhuVuc = request.VungKhuVuc });
                }
            }
            if (request.IdMTC.Any())
            {
                foreach (var item in request.IdMTC)
                {
                    listCauHinh.Add(new CauHinhChietTinh_CapNgam { IdCongViec = request.IdCongViec, IdChiTiet = item, PhanLoai = PhanLoaiChietTinhEnum.MTC.GetHashCode(), VungKhuVuc = request.VungKhuVuc });
                }
            }
            if (listCauHinh.Any())
            {
                _unitOfWork.CauHinhChietTinh_CapNgamRepository.AddRange(listCauHinh);
            }

            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}
