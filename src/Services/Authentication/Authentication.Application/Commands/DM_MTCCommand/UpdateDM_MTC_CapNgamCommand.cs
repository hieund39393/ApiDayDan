using Authentication.Infrastructure.Properties;
using Authentication.Infrastructure.Repositories;
using EVN.Core.Exceptions;
using MediatR;

namespace Authentication.Application.Commands.DM_MTC_CapNgamCommand
{
    public class UpdateDM_MTC_CapNgamCommand : IRequest<bool> // kế thừa IRequest<bool>
    {
        public Guid Id { get; set; } // thêm ID
        public string TenMTC { get; set; }
        public string MaMTC { get; set; }
        public string DonViTinh { get; set; }
    }

    //Tạo thêm 1 class Handler kế thừa IRequestHandler<UpdateDM_MTC_CapNgamCommand, bool> rồi implement
    public class UpdateDM_MTC_CapNgamCommandHandler : IRequestHandler<UpdateDM_MTC_CapNgamCommand, bool> //
    {
        private readonly IUnitOfWork _unitOfWork; // khai báo 
        public UpdateDM_MTC_CapNgamCommandHandler(IUnitOfWork unitOfWork) //cấu hình dependence
        {
            _unitOfWork = unitOfWork; // khai báo
        }
        public async Task<bool> Handle(UpdateDM_MTC_CapNgamCommand request, CancellationToken cancellationToken)
        {
            // tìm kiếm xem có ID trong bảng DM_MTC_CapNgam không
            var entity = await _unitOfWork.DM_MTC_CapNgamRepository.FindOneAsync(x => x.Id == request.Id);
            // nếu không có dữ liệu
            if (entity == null)
            {
                throw new EvnException(string.Format(Resources.MSG_NOT_FOUND, "Máy thi công cáp ngầm"));
            }
            if (entity.TenMTC == request.TenMTC && entity.MaMTC == request.MaMTC)
            {
                entity.DonViTinh = request.DonViTinh;
            }
            else
            {
                var checkEntity = await _unitOfWork.DM_MTC_CapNgamRepository.FindOneAsync(x => x.TenMTC == request.TenMTC && x.MaMTC == request.MaMTC);
                if (checkEntity != null)
                {
                    throw new EvnException(string.Format(Resources.MSG_IS_EXIST, "Máy thi công cáp ngầm"));
                }

                entity.TenMTC = request.TenMTC;
                entity.MaMTC = request.MaMTC;
                entity.DonViTinh = request.DonViTinh;
            }


            //thêm vào DB
            _unitOfWork.DM_MTC_CapNgamRepository.Update(entity);
            //lưu lại trong DB
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}
