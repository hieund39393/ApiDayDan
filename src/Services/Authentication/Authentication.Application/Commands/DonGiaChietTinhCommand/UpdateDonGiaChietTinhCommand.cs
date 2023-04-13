using Authentication.Infrastructure.Properties;
using Authentication.Infrastructure.Repositories;
using EVN.Core.Exceptions;
using MediatR;

namespace Authentication.Application.Commands.DonGiaChietTinhCommand
{
    public class UpdateDonGiaChietTinhCommand : IRequest<bool> // kế thừa IRequest<bool>
    {
        public Guid Id { get; set; } // thêm ID
        public Guid? IdVatLieu { get; set; }
        public decimal DonGia { get; set; }
        public int IdPhanLoai{ get; set; }
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
            // tìm kiếm xem có ID trong bảng DonGiaChietTinh không
            var entity = await _unitOfWork.DonGiaChietTinhRepository.FindOneAsync(x => x.Id == request.Id);
            // nếu không có dữ liệu
            if (entity == null)
            {
                throw new EvnException(string.Format(Resources.MSG_NOT_FOUND, "Đơn giá chiết tinh"));
            }
            if (entity.IdVatLieu == request.IdVatLieu)
            {
                entity.DonGia = request.DonGia;
            }
            else
            {
                var checkEntity = await _unitOfWork.DonGiaChietTinhRepository.FindOneAsync(x => entity.IdVatLieu == request.IdVatLieu);
                if (checkEntity != null)
                {
                    throw new EvnException(string.Format(Resources.MSG_IS_EXIST, "Đơn giá chiết tinh"));
                }
                entity.IdVatLieu = request.IdVatLieu;
                entity.DonGia = request.DonGia;
                entity.IdPhanLoai = request.IdPhanLoai;
            }

            //thêm vào DB
            _unitOfWork.DonGiaChietTinhRepository.Update(entity);
            //lưu lại trong DB
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}
