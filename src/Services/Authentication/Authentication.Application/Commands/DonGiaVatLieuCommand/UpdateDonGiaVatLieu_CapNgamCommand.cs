using Authentication.Infrastructure.Properties;
using Authentication.Infrastructure.Repositories;
using EVN.Core.Exceptions;
using MediatR;

namespace Authentication.Application.Commands.DonGiaVatLieu_CapNgamCommand
{
    public class UpdateDonGiaVatLieu_CapNgamCommand : IRequest<bool> // kế thừa IRequest<bool>
    {
        public Guid Id { get; set; } // thêm ID
        public Guid IdVatLieu { get; set; }
        public string VanBan { get; set; }
        public decimal DonGia { get; set; }
    }

    //Tạo thêm 1 class Handler kế thừa IRequestHandler<UpdateDonGiaVatLieu_CapNgamCommand, bool> rồi implement
    public class UpdateDonGiaVatLieu_CapNgamCommandHandler : IRequestHandler<UpdateDonGiaVatLieu_CapNgamCommand, bool> //
    {
        private readonly IUnitOfWork _unitOfWork; // khai báo 
        public UpdateDonGiaVatLieu_CapNgamCommandHandler(IUnitOfWork unitOfWork) //cấu hình dependence
        {
            _unitOfWork = unitOfWork; // khai báo
        }
        public async Task<bool> Handle(UpdateDonGiaVatLieu_CapNgamCommand request, CancellationToken cancellationToken)
        {
            // tìm kiếm xem có ID trong bảng DonGiaVatLieu_CapNgam không
            var entity = await _unitOfWork.DonGiaVatLieu_CapNgamRepository.FindOneAsync(x => x.Id == request.Id);
            // nếu không có dữ liệu
            if (entity == null)
            {
                throw new EvnException(string.Format(Resources.MSG_NOT_FOUND, "Đơn giá vật liệu cáp ngầm"));
            }
            if (entity.IdVatLieu == request.IdVatLieu && entity.VanBan == request.VanBan)
            {
                entity.DonGia = request.DonGia;
            }
            else
            {
                var checkEntity = await _unitOfWork.DonGiaVatLieu_CapNgamRepository.FindOneAsync(x => x.IdVatLieu == request.IdVatLieu && x.VanBan == request.VanBan);
                if (checkEntity != null)
                {
                    throw new EvnException(string.Format(Resources.MSG_IS_EXIST, "Đơn giá vật liệu cáp ngầm"));
                }
                entity.IdVatLieu = request.IdVatLieu;
                entity.VanBan = request.VanBan;
                entity.DonGia = request.DonGia;
            }

            //thêm vào DB
            _unitOfWork.DonGiaVatLieu_CapNgamRepository.Update(entity);
            //lưu lại trong DB
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}
