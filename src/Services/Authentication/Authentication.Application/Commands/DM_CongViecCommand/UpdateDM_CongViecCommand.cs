using Authentication.Infrastructure.Properties;
using Authentication.Infrastructure.Repositories;
using EVN.Core.Exceptions;
using MediatR;

namespace Authentication.Application.Commands.DM_CongViecCommand
{
    public class UpdateDM_CongViecCommand : IRequest<bool> // kế thừa IRequest<bool>
    {
        public Guid Id { get; set; } // thêm ID
        public string TenCongViec { get; set; }
        public string MaCongViec { get; set; }
        public string DonViTinh { get; set; }

    }

    //Tạo thêm 1 class Handler kế thừa IRequestHandler<UpdateDM_CongViecCommand, bool> rồi implement
    public class UpdateDM_CongViecCommandHandler : IRequestHandler<UpdateDM_CongViecCommand, bool> //
    {
        private readonly IUnitOfWork _unitOfWork; // khai báo 
        public UpdateDM_CongViecCommandHandler(IUnitOfWork unitOfWork) //cấu hình dependence
        {
            _unitOfWork = unitOfWork; // khai báo
        }
        public async Task<bool> Handle(UpdateDM_CongViecCommand request, CancellationToken cancellationToken)
        {
            // tìm kiếm xem có ID trong bảng DM_CongViec không
            var entity = await _unitOfWork.DM_CongViecRepository.FindOneAsync(x => x.Id == request.Id);
            // nếu không có dữ liệu  
            if (entity == null)
            {
                throw new EvnException(string.Format(Resources.MSG_NOT_FOUND, "Công việc"));
            }
            if (entity.TenCongViec == request.TenCongViec && entity.MaCongViec == request.MaCongViec)
            {
                entity.DonViTinh = request.DonViTinh;
            }
            else
            {
                var checkEntity = await _unitOfWork.DM_CongViecRepository.FindOneAsync(x => x.TenCongViec == request.TenCongViec && x.MaCongViec == request.MaCongViec);
                if (checkEntity != null)
                {
                    throw new EvnException(string.Format(Resources.MSG_IS_EXIST, "Công việc"));
                }
                entity.TenCongViec = request.TenCongViec;
                entity.MaCongViec = request.MaCongViec;
                entity.DonViTinh = request.DonViTinh;
            }


            //thêm vào DB
            _unitOfWork.DM_CongViecRepository.Update(entity);
            //lưu lại trong DB
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}
