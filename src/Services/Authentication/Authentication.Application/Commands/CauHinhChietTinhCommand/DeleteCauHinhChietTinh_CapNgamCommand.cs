using Authentication.Infrastructure.Properties;
using Authentication.Infrastructure.Repositories;
using EVN.Core.Exceptions;
using MediatR;

namespace Authentication.Application.Commands.CauHinhChietTinhCommand
{
    public record DeleteCauHinhChietTinh_CapNgamCommand(Guid id) : IRequest<bool>
    {
    }

    //Tạo thêm 1 class Handler kế thừa IRequestHandler<DeleteCauHinhChietTinh_CapNgamCommand, bool> rồi implement
    public class DeleteCauHinhChietTinh_CapNgamCommandHandler : IRequestHandler<DeleteCauHinhChietTinh_CapNgamCommand, bool> //
    {
        private readonly IUnitOfWork _unitOfWork; // khai báo 
        public DeleteCauHinhChietTinh_CapNgamCommandHandler(IUnitOfWork unitOfWork) //cấu hình dependence
        {
            _unitOfWork = unitOfWork; // khai báo
        }

        public async Task<bool> Handle(DeleteCauHinhChietTinh_CapNgamCommand request, CancellationToken cancellationToken)
        {
            // tìm kiếm xem có ID trong bảng CauHinhChietTinh_CapNgam không
            var entity = await _unitOfWork.CauHinhChietTinh_CapNgamRepository.FindOneAsync(x => x.Id == request.id);
            // nếu không có dữ liệu thì thêm mới
            if (entity == null)
            {
                throw new EvnException(string.Format(Resources.MSG_NOT_FOUND, "biểu giá công việc"));
            }

            entity.IsDeleted = true; // xoá mềm 
            //xoá trong DB
            _unitOfWork.CauHinhChietTinh_CapNgamRepository.Update(entity);
            //lưu lại trong DB
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}
