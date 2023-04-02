using Authentication.Infrastructure.Properties;
using Authentication.Infrastructure.Repositories;
using EVN.Core.Exceptions;
using MediatR;

namespace Authentication.Application.Commands.DM_VungCommand
{
    public record DeleteDM_VungCommand(Guid Id) : IRequest<bool> // kế thừa IRequest<bool>
    {
    }

    //Tạo thêm 1 class Handler kế thừa IRequestHandler<DeleteDM_VungCommand, bool> rồi implement
    public class DeleteDM_VungCommandHandler : IRequestHandler<DeleteDM_VungCommand, bool> //
    {
        private readonly IUnitOfWork _unitOfWork; // khai báo 
        public DeleteDM_VungCommandHandler(IUnitOfWork unitOfWork) //cấu hình dependence
        {
            _unitOfWork = unitOfWork; // khai báo
        }
        public async Task<bool> Handle(DeleteDM_VungCommand request, CancellationToken cancellationToken)
        {
            // tìm kiếm xem có ID trong bảng DM_Vung không
            var entity = await _unitOfWork.DM_VungRepository.FindOneAsync(x => x.Id == request.Id);
            // nếu không có dữ liệu thì thêm mới
            if (entity == null)
            {
                throw new EvnException(string.Format(Resources.MSG_NOT_FOUND, "vùng"));
            }

            entity.IsDeleted = true; // xoá mềm 
            //xoá trong DB
            _unitOfWork.DM_VungRepository.Update(entity);
            //lưu lại trong DB
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}
