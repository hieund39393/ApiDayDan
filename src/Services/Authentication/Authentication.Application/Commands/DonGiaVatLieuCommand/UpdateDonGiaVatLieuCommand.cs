using Authentication.Infrastructure.Properties;
using Authentication.Infrastructure.Repositories;
using EVN.Core.Exceptions;
using MediatR;

namespace Authentication.Application.Commands.DonGiaVatLieuCommand
{
    public class UpdateDonGiaVatLieuCommand : IRequest<bool> // kế thừa IRequest<bool>
    {
        public Guid Id { get; set; } // thêm ID
        public Guid IdVatLieu { get; set; }
        public string VanBan { get; set; }
        public decimal DonGia { get; set; }
    }

    //Tạo thêm 1 class Handler kế thừa IRequestHandler<UpdateDonGiaVatLieuCommand, bool> rồi implement
    public class UpdateDonGiaVatLieuCommandHandler : IRequestHandler<UpdateDonGiaVatLieuCommand, bool> //
    {
        private readonly IUnitOfWork _unitOfWork; // khai báo 
        public UpdateDonGiaVatLieuCommandHandler(IUnitOfWork unitOfWork) //cấu hình dependence
        {
            _unitOfWork = unitOfWork; // khai báo
        }
        public async Task<bool> Handle(UpdateDonGiaVatLieuCommand request, CancellationToken cancellationToken)
        {
            // tìm kiếm xem có ID trong bảng DonGiaVatLieu không
            var entity = await _unitOfWork.DonGiaVatLieuRepository.FindOneAsync(x => x.Id == request.Id);
            // nếu không có dữ liệu
            if (entity == null)
            {
                throw new EvnException(string.Format(Resources.MSG_NOT_FOUND, "Đơn giá vật liệu"));
            }
            if (entity.IdVatLieu == request.IdVatLieu && entity.VanBan == request.VanBan)
            {
                entity.DonGia = request.DonGia;
            }
            else
            {
                var checkEntity = await _unitOfWork.DonGiaVatLieuRepository.FindOneAsync(x => x.IdVatLieu == request.IdVatLieu && x.VanBan == request.VanBan);
                if (checkEntity != null)
                {
                    throw new EvnException(string.Format(Resources.MSG_IS_EXIST, "Đơn giá vật liệu"));
                }
                entity.IdVatLieu = request.IdVatLieu;
                entity.VanBan = request.VanBan;
                entity.DonGia = request.DonGia;
            }

            //thêm vào DB
            _unitOfWork.DonGiaVatLieuRepository.Update(entity);
            //lưu lại trong DB
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}
