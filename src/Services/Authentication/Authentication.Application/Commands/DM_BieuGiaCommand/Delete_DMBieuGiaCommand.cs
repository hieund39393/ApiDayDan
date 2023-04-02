using Authentication.Infrastructure.Properties;
using Authentication.Infrastructure.Repositories;
using EVN.Core.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authentication.Application.Commands.DM_BieuGiaCommand
{
    public record Delete_DMBieuGiaCommand(Guid id) : IRequest<bool> // kế thừa IRequest<bool>
    {

    }

    //Tạo thêm 1 class Handler kế thừa IRequestHandler<DeleteDM_LoaiBieuGiaCommand, bool> rồi implement
    public class Delete_DMBieuGiaCommandHandler : IRequestHandler<Delete_DMBieuGiaCommand, bool> //
    {
        private readonly IUnitOfWork _unitOfWork; // khai báo 
        public Delete_DMBieuGiaCommandHandler(IUnitOfWork unitOfWork) //cấu hình dependence
        {
            _unitOfWork = unitOfWork; // khai báo
        }
        public async Task<bool> Handle(Delete_DMBieuGiaCommand request, CancellationToken cancellationToken)
        {
            // tìm kiếm xem có ID trong bảng DM_LoaiBieuGia không
            var entity = await _unitOfWork.DM_BieuGiaRepository.FindOneAsync(x => x.Id == request.id);
            // nếu không có dữ liệu thì thêm mới
            if (entity == null)
            {
                throw new EvnException(string.Format(Resources.MSG_NOT_FOUND, "biểu giá"));
            }

            entity.IsDeleted = true; // xoá mềm 
            //xoá trong DB
            _unitOfWork.DM_BieuGiaRepository.Update(entity);
            //lưu lại trong DB
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}
