using Authentication.Infrastructure.Properties;
using Authentication.Infrastructure.Repositories;
using EVN.Core.Exceptions;
using MediatR;

namespace Authentication.Application.Commands.DM_NhanCongCommand
{
    public class UpdateDM_NhanCongCommand : IRequest<bool> // kế thừa IRequest<bool>
    {
        public Guid Id { get; set; } // thêm ID
        public string HeSo { get; set; }
    }

    //Tạo thêm 1 class Handler kế thừa IRequestHandler<UpdateDM_NhanCongCommand, bool> rồi implement
    public class UpdateDM_NhanCongCommandHandler : IRequestHandler<UpdateDM_NhanCongCommand, bool> //
    {
        private readonly IUnitOfWork _unitOfWork; // khai báo 
        public UpdateDM_NhanCongCommandHandler(IUnitOfWork unitOfWork) //cấu hình dependence
        {
            _unitOfWork = unitOfWork; // khai báo
        }
        public async Task<bool> Handle(UpdateDM_NhanCongCommand request, CancellationToken cancellationToken)
        {
            // tìm kiếm xem có ID trong bảng DM_NhanCong không
            var entity = await _unitOfWork.DM_NhanCongRepository.FindOneAsync(x => x.Id == request.Id);
            // nếu không có dữ liệu
            if (entity == null)
            {
                throw new EvnException(string.Format(Resources.MSG_NOT_FOUND, "Nhân công"));
            }
            entity.HeSo = request.HeSo;


            //thêm vào DB
            _unitOfWork.DM_NhanCongRepository.Update(entity);
            //lưu lại trong DB
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}
