using Authentication.Infrastructure.Properties;
using Authentication.Infrastructure.Repositories;
using EVN.Core.Exceptions;
using MediatR;

namespace Authentication.Application.Commands.DM_KhuVucCommand
{
    public class UpdateDM_KhuVucCommand : IRequest<bool> // kế thừa IRequest<bool>
    {
        public Guid Id { get; set; } // thêm ID
        public string TenKhuVuc { get; set; }
        public string GhiChu { get; set; }
    }

    //Tạo thêm 1 class Handler kế thừa IRequestHandler<UpdateDM_KhuVucCommand, bool> rồi implement
    public class UpdateDM_KhuVucCommandHandler : IRequestHandler<UpdateDM_KhuVucCommand, bool> //
    {
        private readonly IUnitOfWork _unitOfWork; // khai báo 
        public UpdateDM_KhuVucCommandHandler(IUnitOfWork unitOfWork) //cấu hình dependence
        {
            _unitOfWork = unitOfWork; // khai báo
        }
        public async Task<bool> Handle(UpdateDM_KhuVucCommand request, CancellationToken cancellationToken)
        {
            // tìm kiếm xem có ID trong bảng DM_KhuVuc không
            var entity = await _unitOfWork.DM_KhuVucRepository.FindOneAsync(x => x.Id == request.Id);
            // nếu không có dữ liệu
            if (entity == null)
            {
                throw new EvnException(string.Format(Resources.MSG_NOT_FOUND, "khu vực"));
            }

            entity.TenKhuVuc = request.TenKhuVuc;
            entity.GhiChu = request.GhiChu;
            //thêm vào DB
            _unitOfWork.DM_KhuVucRepository.Update(entity);
            //lưu lại trong DB
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}
