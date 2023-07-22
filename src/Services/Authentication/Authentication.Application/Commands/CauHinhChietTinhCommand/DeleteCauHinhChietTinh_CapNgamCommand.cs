using Authentication.Infrastructure.Properties;
using Authentication.Infrastructure.Repositories;
using EVN.Core.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Authentication.Application.Commands.CauHinhChietTinhCommand
{
    public record DeleteCauHinhChietTinh_CapNgamCommand(Guid id, int vungKhuVuc) : IRequest<bool>
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
            // tìm kiếm xem có ID trong bảng CauHinhChietTinh không
            var entity = await _unitOfWork.CauHinhChietTinh_CapNgamRepository.GetQuery(x => x.IdCongViec == request.id && x.VungKhuVuc == request.vungKhuVuc).ToListAsync();
            // nếu không có dữ liệu thì thêm mới
            if (entity == null)
            {
                throw new EvnException(string.Format(Resources.MSG_NOT_FOUND, "Cấu hình"));
            }

            foreach (var item in entity)
            {
                item.IsDeleted = true; // xoá mềm 
                _unitOfWork.CauHinhChietTinh_CapNgamRepository.Update(item);
            }
            //lưu lại trong DB
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}
