using Authentication.Infrastructure.Properties;
using Authentication.Infrastructure.Repositories;
using EVN.Core.Exceptions;
using MediatR;

namespace Authentication.Application.Commands.DM_NhanCong_CapNgamCommand
{
    public class UpdateDM_NhanCong_CapNgamCommand : IRequest<bool> // kế thừa IRequest<bool>
    {
        public Guid Id { get; set; } // thêm ID
        public string HeSo { get; set; }
    }

    //Tạo thêm 1 class Handler kế thừa IRequestHandler<UpdateDM_NhanCong_CapNgamCommand, bool> rồi implement
    public class UpdateDM_NhanCong_CapNgamCommandHandler : IRequestHandler<UpdateDM_NhanCong_CapNgamCommand, bool> //
    {
        private readonly IUnitOfWork _unitOfWork; // khai báo 
        public UpdateDM_NhanCong_CapNgamCommandHandler(IUnitOfWork unitOfWork) //cấu hình dependence
        {
            _unitOfWork = unitOfWork; // khai báo
        }
        public async Task<bool> Handle(UpdateDM_NhanCong_CapNgamCommand request, CancellationToken cancellationToken)
        {
            // tìm kiếm xem có ID trong bảng DM_NhanCong_CapNgam không
            var entity = await _unitOfWork.DM_NhanCong_CapNgamRepository.FindOneAsync(x => x.Id == request.Id);
            // nếu không có dữ liệu
            if (entity == null)
            {
                throw new EvnException(string.Format(Resources.MSG_NOT_FOUND, "Nhân công cáp ngầm"));
            }
           
            entity.HeSo = request.HeSo;
            //thêm vào DB
            _unitOfWork.DM_NhanCong_CapNgamRepository.Update(entity);
            //lưu lại trong DB
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}
