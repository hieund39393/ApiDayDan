using Authentication.Infrastructure.Properties;
using Authentication.Infrastructure.Repositories;
using EVN.Core.Exceptions;
using MediatR;

namespace Authentication.Application.Commands.DM_NhanCongCommand
{
    public record DeleteDM_NhanCongCommand(Guid Id) : IRequest<bool> // kế thừa IRequest<bool>
    {
    }

    //Tạo thêm 1 class Handler kế thừa IRequestHandler<DeleteDM_NhanCongCommand, bool> rồi implement
    public class DeleteDM_NhanCongCommandHandler : IRequestHandler<DeleteDM_NhanCongCommand, bool> //
    {
        private readonly IUnitOfWork _unitOfWork; // khai báo 
        public DeleteDM_NhanCongCommandHandler(IUnitOfWork unitOfWork) //cấu hình dependence
        {
            _unitOfWork = unitOfWork; // khai báo
        }
        public async Task<bool> Handle(DeleteDM_NhanCongCommand request, CancellationToken cancellationToken)
        {
            // tìm kiếm xem có ID trong bảng DM_NhanCong không
            var entity = await _unitOfWork.DM_NhanCongRepository.FindOneAsync(x => x.Id == request.Id);
            // nếu không có dữ liệu thì thêm mới
            if (entity == null)
            {
                throw new EvnException(string.Format(Resources.MSG_NOT_FOUND, "Danh mục nhân công"));
            }

            entity.IsDeleted = true; // xoá mềm 
            //xoá trong DB
            _unitOfWork.DM_NhanCongRepository.Update(entity);
            //lưu lại trong DB
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}
