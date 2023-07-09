using Authentication.Infrastructure.Properties;
using Authentication.Infrastructure.Repositories;
using EVN.Core.Exceptions;
using MediatR;

namespace Authentication.Application.Commands.DM_CongViec_CapNgamCommand
{
    public class UpdateDM_CongViec_CapNgamCommand : IRequest<bool> // kế thừa IRequest<bool>
    {
        public Guid Id { get; set; } // thêm ID
        public string TenCongViec { get; set; }
        public string MaCongViec { get; set; }
        public string DonViTinh { get; set; }

    }

    //Tạo thêm 1 class Handler kế thừa IRequestHandler<UpdateDM_CongViec_CapNgamCommand, bool> rồi implement
    public class UpdateDM_CongViec_CapNgamCommandHandler : IRequestHandler<UpdateDM_CongViec_CapNgamCommand, bool> //
    {
        private readonly IUnitOfWork _unitOfWork; // khai báo 
        public UpdateDM_CongViec_CapNgamCommandHandler(IUnitOfWork unitOfWork) //cấu hình dependence
        {
            _unitOfWork = unitOfWork; // khai báo
        }
        public async Task<bool> Handle(UpdateDM_CongViec_CapNgamCommand request, CancellationToken cancellationToken)
        {
            // tìm kiếm xem có ID trong bảng DM_CongViec_CapNgam không
            var entity = await _unitOfWork.DM_CongViec_CapNgamRepository.FindOneAsync(x => x.Id == request.Id);
            // nếu không có dữ liệu  
            if (entity == null)
            {
                throw new EvnException(string.Format(Resources.MSG_NOT_FOUND, "Công việc cáp ngầm"));
            }
            if (entity.TenCongViec == request.TenCongViec && entity.MaCongViec == request.MaCongViec)
            {
                entity.DonViTinh = request.DonViTinh;
            }
            else
            {
                var checkEntity = await _unitOfWork.DM_CongViec_CapNgamRepository.FindOneAsync(x => x.TenCongViec == request.TenCongViec && x.MaCongViec == request.MaCongViec);
                if (checkEntity != null)
                {
                    throw new EvnException(string.Format(Resources.MSG_IS_EXIST, "Công việc cáp ngầm"));
                }
                entity.TenCongViec = request.TenCongViec;
                entity.MaCongViec = request.MaCongViec;
                entity.DonViTinh = request.DonViTinh;
            }


            //thêm vào DB
            _unitOfWork.DM_CongViec_CapNgamRepository.Update(entity);
            //lưu lại trong DB
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}
