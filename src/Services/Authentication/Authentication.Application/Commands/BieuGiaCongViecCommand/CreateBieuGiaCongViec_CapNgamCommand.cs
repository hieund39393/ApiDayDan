using Authentication.Infrastructure.AggregatesModel.BieuGiaCongViecAggregate;
using Authentication.Infrastructure.Properties;
using Authentication.Infrastructure.Repositories;
using EVN.Core.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Authentication.Application.Commands.BieuGiaCongViec_CapNgamCommand
{
    public class CreateBieuGiaCongViec_CapNgamCommand : IRequest<bool>
    {
        public Guid IdBieuGia { get; set; }
        public List<Guid> IdCongViec { get; set; }
        public bool CongViecChinh { get; set; }
        public int PhanLoai { get; set; }
    }

    //Tạo thêm 1 class Handler kế thừa IRequestHandler<CreateBieuGiaCongViec_CapNgamCommand, bool> rồi implement
    public class CreateBieuGiaCongViec_CapNgamCommandHandler : IRequestHandler<CreateBieuGiaCongViec_CapNgamCommand, bool> //
    {
        private readonly IUnitOfWork _unitOfWork; // khai báo 
        public CreateBieuGiaCongViec_CapNgamCommandHandler(IUnitOfWork unitOfWork) //cấu hình dependence
        {
            _unitOfWork = unitOfWork; // khai báo
        }
        public async Task<bool> Handle(CreateBieuGiaCongViec_CapNgamCommand request, CancellationToken cancellationToken)
        {
            // tìm kiếm xem có trùng trong db không

            var listBieuGia = await _unitOfWork.BieuGiaCongViec_CapNgamRepository.GetQuery(x => x.IdBieuGia == request.IdBieuGia).ToListAsync();
            if (request.CongViecChinh == true && listBieuGia.Where(x => x.CongViecChinh).Count() > 0)
            {
                throw new EvnException("Biểu giá đã có công việc chính");
            }

            foreach (var item in request.IdCongViec)
            {
                var entity = listBieuGia.FirstOrDefault(x => x.IdCongViec == item && x.IdBieuGia == request.IdBieuGia);
                // nếu không có dữ liệu thì thêm mới
                if (entity == null)
                {
                    // Tạo model BieuGiaCongViec_CapNgam
                    var model = new BieuGiaCongViec_CapNgam
                    {
                        IdBieuGia = request.IdBieuGia,
                        IdCongViec = item,
                        CongViecChinh = request.CongViecChinh,
                        PhanLoai = request.PhanLoai,
                    };
                    //thêm vào DB
                    _unitOfWork.BieuGiaCongViec_CapNgamRepository.Add(model);
                    //lưu lại trong DB
                }
            }

            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}
