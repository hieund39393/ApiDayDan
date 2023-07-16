using Authentication.Infrastructure.Properties;
using Authentication.Infrastructure.Repositories;
using EVN.Core.Exceptions;
using MediatR;

namespace Authentication.Application.Commands.DM_MTCCommand
{
    public class UpdateDM_MTCCommand : IRequest<bool> // kế thừa IRequest<bool>
    {
        public Guid Id { get; set; } // thêm ID
        public string TenMTC { get; set; }
        public string MaMTC { get; set; }
        public string DonViTinh { get; set; }
    }

    //Tạo thêm 1 class Handler kế thừa IRequestHandler<UpdateDM_MTCCommand, bool> rồi implement
    public class UpdateDM_MTCCommandHandler : IRequestHandler<UpdateDM_MTCCommand, bool> //
    {
        private readonly IUnitOfWork _unitOfWork; // khai báo 
        public UpdateDM_MTCCommandHandler(IUnitOfWork unitOfWork) //cấu hình dependence
        {
            _unitOfWork = unitOfWork; // khai báo
        }
        public async Task<bool> Handle(UpdateDM_MTCCommand request, CancellationToken cancellationToken)
        {
            // tìm kiếm xem có ID trong bảng DM_MTC không
            var entity = await _unitOfWork.DM_MTCRepository.FindOneAsync(x => x.Id == request.Id);
            // nếu không có dữ liệu
            if (entity == null)
            {
                throw new EvnException(string.Format(Resources.MSG_NOT_FOUND, "Máy thi công"));
            }
            if (entity.TenMayThiCong == request.TenMTC && entity.MaMTC == request.MaMTC)
            {
                entity.DonViTinh = request.DonViTinh;
            }
            else
            {
                var checkEntity = await _unitOfWork.DM_MTCRepository.FindOneAsync(x => x.TenMayThiCong == request.TenMTC && x.MaMTC == request.MaMTC);
                if (checkEntity != null)
                {
                    throw new EvnException(string.Format(Resources.MSG_IS_EXIST, "Máy thi công"));
                }

                entity.TenMayThiCong = request.TenMTC;
                entity.MaMTC = request.MaMTC;
                entity.DonViTinh = request.DonViTinh;
            }


            //thêm vào DB
            _unitOfWork.DM_MTCRepository.Update(entity);
            //lưu lại trong DB
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}
