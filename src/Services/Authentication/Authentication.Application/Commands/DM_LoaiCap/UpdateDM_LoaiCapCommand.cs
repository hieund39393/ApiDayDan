using Authentication.Infrastructure.Properties;
using Authentication.Infrastructure.Repositories;
using EVN.Core.Exceptions;
using MediatR;

namespace Authentication.Application.Commands.DM_LoaiCapCommand
{
    public class UpdateDM_LoaiCapCommand : IRequest<bool> // kế thừa IRequest<bool>
    {
        public Guid Id { get; set; } // thêm ID
        public string TenLoaiCap { get; set; }
        public string MaLoaiCap { get; set; }
        public string DonViTinh { get; set; }
    }

    //Tạo thêm 1 class Handler kế thừa IRequestHandler<UpdateDM_LoaiCapCommand, bool> rồi implement
    public class UpdateDM_LoaiCapCommandHandler : IRequestHandler<UpdateDM_LoaiCapCommand, bool> //
    {
        private readonly IUnitOfWork _unitOfWork; // khai báo 
        public UpdateDM_LoaiCapCommandHandler(IUnitOfWork unitOfWork) //cấu hình dependence
        {
            _unitOfWork = unitOfWork; // khai báo
        }
        public async Task<bool> Handle(UpdateDM_LoaiCapCommand request, CancellationToken cancellationToken)
        {
            // tìm kiếm xem có ID trong bảng DM_LoaiCap không
            var entity = await _unitOfWork.DM_LoaiCapRepository.FindOneAsync(x => x.Id == request.Id);
            // nếu không có dữ liệu
            if (entity == null)
            {
                throw new EvnException(string.Format(Resources.MSG_NOT_FOUND, "Loại cáp"));
            }

            entity.TenLoaiCap = request.TenLoaiCap ;
            entity.MaLoaiCap = request.MaLoaiCap ;
            entity.DonViTinh = request.DonViTinh;
            //thêm vào DB
            _unitOfWork.DM_LoaiCapRepository.Update(entity);
            //lưu lại trong DB
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}
