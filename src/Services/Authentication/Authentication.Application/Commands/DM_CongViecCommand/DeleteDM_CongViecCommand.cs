using Authentication.Infrastructure.Properties;
using Authentication.Infrastructure.Repositories;
using EVN.Core.Exceptions;
using MediatR;

namespace Authentication.Application.Commands.DM_CongViecCommand
{
    public class DeleteDM_CongViecCommand : IRequest<bool> // kế thừa IRequest<bool>
    {
        public Guid Id { get; set; } // thêm ID
    }

    //Tạo thêm 1 class Handler kế thừa IRequestHandler<DeleteDM_CongViecCommand, bool> rồi implement
    public class DeleteDM_CongViecCommandHandler : IRequestHandler<DeleteDM_CongViecCommand, bool> //
    {
        private readonly IUnitOfWork _unitOfWork; // khai báo 
        public DeleteDM_CongViecCommandHandler(IUnitOfWork unitOfWork) //cấu hình dependence
        {
            _unitOfWork = unitOfWork; // khai báo
        }
        public async Task<bool> Handle(DeleteDM_CongViecCommand request, CancellationToken cancellationToken)
        {
            // tìm kiếm xem có ID trong bảng DM_CongViec không
            var entity = await _unitOfWork.DM_CongViecRepository.FindOneAsync(x => x.Id == request.Id);
            // nếu không có dữ liệu thì thêm mới
            if (entity == null)
            {
                throw new EvnException(string.Format(Resources.MSG_NOT_FOUND, "công việc"));
            }

            entity.IsDeleted = true; // xoá mềm 
            //xoá trong DB
            _unitOfWork.DM_CongViecRepository.Update(entity);
            //lưu lại trong DB
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}
