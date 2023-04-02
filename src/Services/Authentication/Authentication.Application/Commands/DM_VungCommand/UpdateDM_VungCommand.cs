using Authentication.Infrastructure.Properties;
using Authentication.Infrastructure.Repositories;
using EVN.Core.Exceptions;
using MediatR;

namespace Authentication.Application.Commands.DM_VungCommand
{
    public class UpdateDM_VungCommand : IRequest<bool> // kế thừa IRequest<bool>
    {
        public Guid Id { get; set; } // thêm ID
        public string TenVung { get; set; }
        public string GhiChu { get; set; }
    }

    //Tạo thêm 1 class Handler kế thừa IRequestHandler<UpdateDM_VungCommand, bool> rồi implement
    public class UpdateDM_VungCommandHandler : IRequestHandler<UpdateDM_VungCommand, bool> //
    {
        private readonly IUnitOfWork _unitOfWork; // khai báo 
        public UpdateDM_VungCommandHandler(IUnitOfWork unitOfWork) //cấu hình dependence
        {
            _unitOfWork = unitOfWork; // khai báo
        }
        public async Task<bool> Handle(UpdateDM_VungCommand request, CancellationToken cancellationToken)
        {
            // tìm kiếm xem có ID trong bảng DM_Vung không
            var entity = await _unitOfWork.DM_VungRepository.FindOneAsync(x => x.Id == request.Id);
            // nếu không có dữ liệu
            if (entity == null)
            {
                throw new EvnException(string.Format(Resources.MSG_NOT_FOUND, "vùng"));
            }

            entity.TenVung = request.TenVung;
            entity.GhiChu = request.GhiChu;
            //thêm vào DB
            _unitOfWork.DM_VungRepository.Update(entity);
            //lưu lại trong DB
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}
