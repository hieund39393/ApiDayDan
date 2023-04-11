using Authentication.Infrastructure.Properties;
using Authentication.Infrastructure.Repositories;
using EVN.Core.Exceptions;
using MediatR;

namespace Authentication.Application.Commands.BieuGiaCongViecCommand
{
    public record DeleteBieuGiaCongViecCommand(Guid id) : IRequest<bool>
    {
    }

    //Tạo thêm 1 class Handler kế thừa IRequestHandler<DeleteBieuGiaCongViecCommand, bool> rồi implement
    public class DeleteBieuGiaCongViecCommandHandler : IRequestHandler<DeleteBieuGiaCongViecCommand, bool> //
    {
        private readonly IUnitOfWork _unitOfWork; // khai báo 
        public DeleteBieuGiaCongViecCommandHandler(IUnitOfWork unitOfWork) //cấu hình dependence
        {
            _unitOfWork = unitOfWork; // khai báo
        }

        public async Task<bool> Handle(DeleteBieuGiaCongViecCommand request, CancellationToken cancellationToken)
        {
            // tìm kiếm xem có ID trong bảng BieuGiaCongViec không
            var entity = await _unitOfWork.BieuGiaCongViecRepository.FindOneAsync(x => x.Id == request.id);
            // nếu không có dữ liệu thì thêm mới
            if (entity == null)
            {
                throw new EvnException(string.Format(Resources.MSG_NOT_FOUND, "biểu giá công việc"));
            }

            entity.IsDeleted = true; // xoá mềm 
            //xoá trong DB
            _unitOfWork.BieuGiaCongViecRepository.Update(entity);
            //lưu lại trong DB
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}
