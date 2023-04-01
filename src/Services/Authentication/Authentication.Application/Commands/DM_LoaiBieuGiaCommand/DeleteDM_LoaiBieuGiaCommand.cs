using Authentication.Infrastructure.Properties;
using Authentication.Infrastructure.Repositories;
using EVN.Core.Exceptions;
using MediatR;

namespace Authentication.Application.Commands.DM_LoaiBieuGiaCommand
{
    public record DeleteDM_LoaiBieuGiaCommand(Guid id) : IRequest<bool> // kế thừa IRequest<bool>
    {
    }

    //Tạo thêm 1 class Handler kế thừa IRequestHandler<DeleteDM_LoaiBieuGiaCommand, bool> rồi implement
    public class DeleteDM_LoaiBieuGiaCommandHandler : IRequestHandler<DeleteDM_LoaiBieuGiaCommand, bool> //
    {
        private readonly IUnitOfWork _unitOfWork; // khai báo 
        public DeleteDM_LoaiBieuGiaCommandHandler(IUnitOfWork unitOfWork) //cấu hình dependence
        {
            _unitOfWork = unitOfWork; // khai báo
        }
        public async Task<bool> Handle(DeleteDM_LoaiBieuGiaCommand request, CancellationToken cancellationToken)
        {
            // tìm kiếm xem có ID trong bảng DM_LoaiBieuGia không
            var entity = await _unitOfWork.DM_LoaiBieuGiaRepository.FindOneAsync(x => x.Id == request.id);
            // nếu không có dữ liệu thì thêm mới
            if (entity == null)
            {
                throw new EvnException(string.Format(Resources.MSG_NOT_FOUND, "Loại biểu giá"));
            }

            entity.IsDeleted = true; // xoá mềm 
            //xoá trong DB
            _unitOfWork.DM_LoaiBieuGiaRepository.Update(entity);
            //lưu lại trong DB
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}
