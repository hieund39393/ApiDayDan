using Authentication.Infrastructure.Properties;
using Authentication.Infrastructure.Repositories;
using EVN.Core.Exceptions;
using MediatR;

namespace Authentication.Application.Commands.DonGiaMTCCommand
{
    public record DeleteDonGiaMTC_CapNgamCommand(Guid Id) : IRequest<bool> // kế thừa IRequest<bool>
    {
    }

    //Tạo thêm 1 class Handler kế thừa IRequestHandler<DeleteDonGiaMTC_CapNgamCommand, bool> rồi implement
    public class DeleteDonGiaMTC_CapNgamCommandHandler : IRequestHandler<DeleteDonGiaMTC_CapNgamCommand, bool> //
    {
        private readonly IUnitOfWork _unitOfWork; // khai báo 
        public DeleteDonGiaMTC_CapNgamCommandHandler(IUnitOfWork unitOfWork) //cấu hình dependence
        {
            _unitOfWork = unitOfWork; // khai báo
        }
        public async Task<bool> Handle(DeleteDonGiaMTC_CapNgamCommand request, CancellationToken cancellationToken)
        {
            // tìm kiếm xem có ID trong bảng DonGiaMTC_CapNgam không
            var entity = await _unitOfWork.DonGiaMTC_CapNgamRepository.FindOneAsync(x => x.Id == request.Id);
            // nếu không có dữ liệu thì thêm mới
            if (entity == null)
            {
                throw new EvnException(string.Format(Resources.MSG_NOT_FOUND, "Đơn giá máy thi công cáp ngầm"));
            }

            entity.IsDeleted = true; // xoá mềm 
            //xoá trong DB
            _unitOfWork.DonGiaMTC_CapNgamRepository.Update(entity);
            //lưu lại trong DB
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}
