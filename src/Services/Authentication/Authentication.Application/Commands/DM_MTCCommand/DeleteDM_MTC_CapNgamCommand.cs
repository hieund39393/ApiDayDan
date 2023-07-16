using Authentication.Infrastructure.Properties;
using Authentication.Infrastructure.Repositories;
using EVN.Core.Exceptions;
using MediatR;

namespace Authentication.Application.Commands.DM_MTC_CapNgamCommand
{
    public record DeleteDM_MTC_CapNgamCommand(Guid Id) : IRequest<bool> // kế thừa IRequest<bool>
    {
    }

    //Tạo thêm 1 class Handler kế thừa IRequestHandler<DeleteDM_MTC_CapNgamCommand, bool> rồi implement
    public class DeleteDM_MTC_CapNgamCommandHandler : IRequestHandler<DeleteDM_MTC_CapNgamCommand, bool> //
    {
        private readonly IUnitOfWork _unitOfWork; // khai báo 
        public DeleteDM_MTC_CapNgamCommandHandler(IUnitOfWork unitOfWork) //cấu hình dependence
        {
            _unitOfWork = unitOfWork; // khai báo
        }
        public async Task<bool> Handle(DeleteDM_MTC_CapNgamCommand request, CancellationToken cancellationToken)
        {
            // tìm kiếm xem có ID trong bảng DM_MTC_CapNgam không
            var entity = await _unitOfWork.DM_MTC_CapNgamRepository.FindOneAsync(x => x.Id == request.Id);
            // nếu không có dữ liệu thì thêm mới
            if (entity == null)
            {
                throw new EvnException(string.Format(Resources.MSG_NOT_FOUND, "Máy thi công cáp ngầm"));
            }

            entity.IsDeleted = true; // xoá mềm 
            //xoá trong DB
            _unitOfWork.DM_MTC_CapNgamRepository.Update(entity);
            //lưu lại trong DB
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}
