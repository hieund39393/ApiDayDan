using Authentication.Infrastructure.Properties;
using Authentication.Infrastructure.Repositories;
using EVN.Core.Exceptions;
using MediatR;

namespace Authentication.Application.Commands.DM_VatLieu_CapNgamCommand
{
    public class UpdateDM_VatLieu_CapNgamCommand : IRequest<bool> // kế thừa IRequest<bool>
    {
        public Guid Id { get; set; } // thêm ID
        public string TenVatLieu { get; set; }
        public string MaVatLieu { get; set; }
        public string DonViTinh { get; set; }
    }

    //Tạo thêm 1 class Handler kế thừa IRequestHandler<UpdateDM_VatLieu_CapNgamCommand, bool> rồi implement
    public class UpdateDM_VatLieu_CapNgamCommandHandler : IRequestHandler<UpdateDM_VatLieu_CapNgamCommand, bool> //
    {
        private readonly IUnitOfWork _unitOfWork; // khai báo 
        public UpdateDM_VatLieu_CapNgamCommandHandler(IUnitOfWork unitOfWork) //cấu hình dependence
        {
            _unitOfWork = unitOfWork; // khai báo
        }
        public async Task<bool> Handle(UpdateDM_VatLieu_CapNgamCommand request, CancellationToken cancellationToken)
        {
            // tìm kiếm xem có ID trong bảng DM_VatLieu_CapNgam không
            var entity = await _unitOfWork.DM_VatLieu_CapNgamRepository.FindOneAsync(x => x.Id == request.Id);
            // nếu không có dữ liệu
            if (entity == null)
            {
                throw new EvnException(string.Format(Resources.MSG_NOT_FOUND, "Vật liệu cáp ngầm"));
            }
            if (entity.TenVatLieu == request.TenVatLieu && entity.MaVatLieu == request.MaVatLieu)
            {
                entity.DonViTinh = request.DonViTinh;
            }
            else
            {
                var checkEntity = await _unitOfWork.DM_VatLieu_CapNgamRepository.FindOneAsync(x => x.TenVatLieu == request.TenVatLieu && x.MaVatLieu == request.MaVatLieu);
                if (checkEntity != null)
                {
                    throw new EvnException(string.Format(Resources.MSG_IS_EXIST, "Vật liệu cáp ngầm"));
                }

                entity.TenVatLieu = request.TenVatLieu;
                entity.MaVatLieu = request.MaVatLieu;
                entity.DonViTinh = request.DonViTinh;
            }


            //thêm vào DB
            _unitOfWork.DM_VatLieu_CapNgamRepository.Update(entity);
            //lưu lại trong DB
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}
