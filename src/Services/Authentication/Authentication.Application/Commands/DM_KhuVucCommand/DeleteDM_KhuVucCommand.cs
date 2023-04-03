using Authentication.Infrastructure.Properties;
using Authentication.Infrastructure.Repositories;
using EVN.Core.Exceptions;
using MediatR;

namespace Authentication.Application.Commands.DM_KhuVucCommand
{
    public record DeleteDM_KhuVucCommand(Guid id) : IRequest<bool> // kế thừa IRequest<bool>
    {
    }

    //Tạo thêm 1 class Handler kế thừa IRequestHandler<DeleteDM_KhuVucCommand, bool> rồi implement
    public class DeleteDM_KhuVucCommandHandler : IRequestHandler<DeleteDM_KhuVucCommand, bool> //
    {
        private readonly IUnitOfWork _unitOfWork; // khai báo 
        public DeleteDM_KhuVucCommandHandler(IUnitOfWork unitOfWork) //cấu hình dependence
        {
            _unitOfWork = unitOfWork; // khai báo
        }
        public async Task<bool> Handle(DeleteDM_KhuVucCommand request, CancellationToken cancellationToken)
        {
            // tìm kiếm xem có ID trong bảng DM_KhuVuc không
            var entity = await _unitOfWork.DM_KhuVucRepository.FindOneAsync(x => x.Id == request.id);
            // nếu không có dữ liệu thì thêm mới
            if (entity == null)
            {
                throw new EvnException(string.Format(Resources.MSG_NOT_FOUND, "khu vực"));
            }

            entity.IsDeleted = true; // xoá mềm 
            //xoá trong DB
            _unitOfWork.DM_KhuVucRepository.Update(entity);
            //lưu lại trong DB
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}
