using Authentication.Infrastructure.Properties;
using Authentication.Infrastructure.Repositories;
using EVN.Core.Exceptions;
using MediatR;

namespace Authentication.Application.Commands.GiaCapCommand
{
    public class UpdateGiaCapCommand : IRequest<bool> // kế thừa IRequest<bool>
    {
        public Guid Id { get; set; } // thêm ID
        public Guid IdLoaiCap { get; set; }
        public string VanBan { get; set; }
        public decimal DonGia { get; set; }
    }

    //Tạo thêm 1 class Handler kế thừa IRequestHandler<UpdateGiaCapCommand, bool> rồi implement
    public class UpdateGiaCapCommandHandler : IRequestHandler<UpdateGiaCapCommand, bool> //
    {
        private readonly IUnitOfWork _unitOfWork; // khai báo 
        public UpdateGiaCapCommandHandler(IUnitOfWork unitOfWork) //cấu hình dependence
        {
            _unitOfWork = unitOfWork; // khai báo
        }
        public async Task<bool> Handle(UpdateGiaCapCommand request, CancellationToken cancellationToken)
        {
            // tìm kiếm xem có ID trong bảng GiaCap không
            var entity = await _unitOfWork.GiaCapRepository.FindOneAsync(x => x.Id == request.Id);
            // nếu không có dữ liệu
            if (entity == null)
            {
                throw new EvnException(string.Format(Resources.MSG_NOT_FOUND, "Giá cáp"));
            }
            if (entity.IdLoaiCap == request.IdLoaiCap && entity.VanBan == request.VanBan)
            {
                entity.DonGia = request.DonGia;
            }
            else
            {
                var checkEntity = await _unitOfWork.GiaCapRepository.FindOneAsync(x => x.IdLoaiCap == request.IdLoaiCap && x.VanBan == request.VanBan);
                if (checkEntity != null)
                {
                    throw new EvnException(string.Format(Resources.MSG_IS_EXIST, "Giá cáp"));
                }
                entity.IdLoaiCap = request.IdLoaiCap;
                entity.VanBan = request.VanBan;
                entity.DonGia = request.DonGia;
            }

            //thêm vào DB
            _unitOfWork.GiaCapRepository.Update(entity);
            //lưu lại trong DB
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}
