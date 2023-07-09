using Authentication.Infrastructure.Properties;
using Authentication.Infrastructure.Repositories;
using EVN.Core.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Authentication.Application.Commands.BieuGiaCongViec_CapNgamCommand
{
    public class UpdateBieuGiaCongViec_CapNgamCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
        public Guid IdBieuGia { get; set; }
        public Guid IdCongViec { get; set; }
        public bool CongViecChinh { get; set; }
        public int PhanLoai { get; set; }
    }

    //Tạo thêm 1 class Handler kế thừa IRequestHandler<UpdateBieuGiaCongViec_CapNgamCommand, bool> rồi implement
    public class UpdateBieuGiaCongViec_CapNgamCommandHandler : IRequestHandler<UpdateBieuGiaCongViec_CapNgamCommand, bool> //
    {
        private readonly IUnitOfWork _unitOfWork; // khai báo 
        public UpdateBieuGiaCongViec_CapNgamCommandHandler(IUnitOfWork unitOfWork) //cấu hình dependence
        {
            _unitOfWork = unitOfWork; // khai báo
        }
        public async Task<bool> Handle(UpdateBieuGiaCongViec_CapNgamCommand request, CancellationToken cancellationToken)
        {


            // tìm kiếm xem có ID trong bảng BieuGiaCongViec_CapNgam không
            var entity = await _unitOfWork.BieuGiaCongViec_CapNgamRepository.FindOneAsync(x => x.Id == request.Id);

            // nếu không có dữ liệu
            if (entity == null)
            {
                throw new EvnException(string.Format(Resources.MSG_NOT_FOUND, "biểu giá công việc"));
            }
            if (request.CongViecChinh && !entity.CongViecChinh && await _unitOfWork.BieuGiaCongViec_CapNgamRepository.GetQuery().AnyAsync(x => x.IdBieuGia == request.IdBieuGia && x.CongViecChinh))
            {
                throw new EvnException("Biểu giá đã có công việc chính");
            }

            entity.IdBieuGia = request.IdBieuGia;
            entity.IdCongViec = request.IdCongViec;
            entity.CongViecChinh = request.CongViecChinh;
            entity.PhanLoai = request.PhanLoai;
            //thêm vào DB
            _unitOfWork.BieuGiaCongViec_CapNgamRepository.Update(entity);
            //lưu lại trong DB
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}
