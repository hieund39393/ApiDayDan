using Authentication.Infrastructure.Properties;
using Authentication.Infrastructure.Repositories;
using EVN.Core.Exceptions;
using MediatR;

namespace Authentication.Application.Commands.DM_VatLieuCommand
{
    public record DeleteVanBanThongBaoCommand(Guid Id) : IRequest<bool> // kế thừa IRequest<bool>
    {
    }

    public class DeleteVanBanThongBaoCommandHandler : IRequestHandler<DeleteVanBanThongBaoCommand, bool> //
    {
        private readonly IUnitOfWork _unitOfWork; // khai báo 
        public DeleteVanBanThongBaoCommandHandler(IUnitOfWork unitOfWork) //cấu hình dependence
        {
            _unitOfWork = unitOfWork; // khai báo
        }
        public async Task<bool> Handle(DeleteVanBanThongBaoCommand request, CancellationToken cancellationToken)
        {
            var entity = await _unitOfWork.VanBanThongBaoRepository.FindOneAsync(x => x.Id == request.Id);
            if (entity == null)
            {
                throw new EvnException(string.Format(Resources.MSG_NOT_FOUND, "Văn bản thông báo"));
            }

            entity.IsDeleted = true; // xoá mềm 
            //xoá trong DB
            _unitOfWork.VanBanThongBaoRepository.Update(entity);
            //lưu lại trong DB
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}
