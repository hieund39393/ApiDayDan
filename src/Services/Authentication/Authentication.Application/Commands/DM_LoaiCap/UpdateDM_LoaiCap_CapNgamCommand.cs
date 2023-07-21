using Authentication.Infrastructure.Properties;
using Authentication.Infrastructure.Repositories;
using EVN.Core.Exceptions;
using MediatR;

namespace Authentication.Application.Commands.DM_LoaiCap_CapNgamCommand
{
    public class UpdateDM_LoaiCap_CapNgamCommand : IRequest<bool> // kế thừa IRequest<bool>
    {
        public Guid Id { get; set; } // thêm ID
        public string TenLoaiCap { get; set; }
        public string MaLoaiCap { get; set; }
        public string DonViTinh { get; set; }
    }

    //Tạo thêm 1 class Handler kế thừa IRequestHandler<UpdateDM_LoaiCap_CapNgamCommand, bool> rồi implement
    public class UpdateDM_LoaiCap_CapNgamCommandHandler : IRequestHandler<UpdateDM_LoaiCap_CapNgamCommand, bool> //
    {
        private readonly IUnitOfWork _unitOfWork; // khai báo 
        public UpdateDM_LoaiCap_CapNgamCommandHandler(IUnitOfWork unitOfWork) //cấu hình dependence
        {
            _unitOfWork = unitOfWork; // khai báo
        }
        public async Task<bool> Handle(UpdateDM_LoaiCap_CapNgamCommand request, CancellationToken cancellationToken)
        {
            // tìm kiếm xem có ID trong bảng DM_LoaiCap_CapNgam không
            var entity = await _unitOfWork.DM_LoaiCap_CapNgamRepository.FindOneAsync(x => x.Id == request.Id);
            // nếu không có dữ liệu
            if (entity == null)
            {
                throw new EvnException(string.Format(Resources.MSG_NOT_FOUND, "Loại cáp"));
            }
            if (entity.TenLoaiCap == request.TenLoaiCap && entity.MaLoaiCap == request.MaLoaiCap)
            {
                entity.DonViTinh = request.DonViTinh;
            }
            else
            {
                var checkEntity = await _unitOfWork.DM_LoaiCap_CapNgamRepository.FindOneAsync(x => x.TenLoaiCap == request.TenLoaiCap && x.MaLoaiCap == request.MaLoaiCap);
                if (checkEntity != null)
                {
                    throw new EvnException(string.Format(Resources.MSG_IS_EXIST, "Loại cáp"));
                }
                entity.TenLoaiCap = request.TenLoaiCap;
                entity.MaLoaiCap = request.MaLoaiCap;
                entity.DonViTinh = request.DonViTinh;
            }


            //thêm vào DB
            _unitOfWork.DM_LoaiCap_CapNgamRepository.Update(entity);
            //lưu lại trong DB
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}
