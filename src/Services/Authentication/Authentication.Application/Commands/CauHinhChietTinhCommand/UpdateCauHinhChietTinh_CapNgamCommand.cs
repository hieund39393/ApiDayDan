using Authentication.Infrastructure.Properties;
using Authentication.Infrastructure.Repositories;
using EVN.Core.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Authentication.Application.Commands.CauHinhChietTinh_CapNgamCommand
{
    public class UpdateCauHinhChietTinh_CapNgamCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
        public Guid IdBieuGia { get; set; }
        public Guid IdCongViec { get; set; }
        public bool CongViecChinh { get; set; }
        public int PhanLoai { get; set; }
    }

    //Tạo thêm 1 class Handler kế thừa IRequestHandler<UpdateCauHinhChietTinh_CapNgamCommand, bool> rồi implement
    public class UpdateCauHinhChietTinh_CapNgamCommandHandler : IRequestHandler<UpdateCauHinhChietTinh_CapNgamCommand, bool> //
    {
        private readonly IUnitOfWork _unitOfWork; // khai báo 
        public UpdateCauHinhChietTinh_CapNgamCommandHandler(IUnitOfWork unitOfWork) //cấu hình dependence
        {
            _unitOfWork = unitOfWork; // khai báo
        }
        public async Task<bool> Handle(UpdateCauHinhChietTinh_CapNgamCommand request, CancellationToken cancellationToken)
        {


            // tìm kiếm xem có ID trong bảng CauHinhChietTinh_CapNgam không
            var entity = await _unitOfWork.CauHinhChietTinh_CapNgamRepository.FindOneAsync(x => x.Id == request.Id);

            // nếu không có dữ liệu
         
            //thêm vào DB
            _unitOfWork.CauHinhChietTinh_CapNgamRepository.Update(entity);
            //lưu lại trong DB
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}
