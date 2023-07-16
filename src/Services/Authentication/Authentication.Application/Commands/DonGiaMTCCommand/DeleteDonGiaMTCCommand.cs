using Authentication.Infrastructure.Properties;
using Authentication.Infrastructure.Repositories;
using EVN.Core.Exceptions;
using MediatR;

namespace Authentication.Application.Commands.DonGiaMTCCommand
{
    public record DeleteDonGiaMTCCommand(Guid Id) : IRequest<bool> // kế thừa IRequest<bool>
    {
    }

    //Tạo thêm 1 class Handler kế thừa IRequestHandler<DeleteDonGiaMTCCommand, bool> rồi implement
    public class DeleteDonGiaMTCCommandHandler : IRequestHandler<DeleteDonGiaMTCCommand, bool> //
    {
        private readonly IUnitOfWork _unitOfWork; // khai báo 
        public DeleteDonGiaMTCCommandHandler(IUnitOfWork unitOfWork) //cấu hình dependence
        {
            _unitOfWork = unitOfWork; // khai báo
        }
        public async Task<bool> Handle(DeleteDonGiaMTCCommand request, CancellationToken cancellationToken)
        {
            // tìm kiếm xem có ID trong bảng DonGiaMTC không
            var entity = await _unitOfWork.DonGiaMTCRepository.FindOneAsync(x => x.Id == request.Id);
            // nếu không có dữ liệu thì thêm mới
            if (entity == null)
            {
                throw new EvnException(string.Format(Resources.MSG_NOT_FOUND, "Đơn giá máy thi công"));
            }

            entity.IsDeleted = true; // xoá mềm 
            //xoá trong DB
            _unitOfWork.DonGiaMTCRepository.Update(entity);
            //lưu lại trong DB
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}
