using Authentication.Infrastructure.Properties;
using Authentication.Infrastructure.Repositories;
using EVN.Core.Exceptions;
using MediatR;

namespace Authentication.Application.Commands.BieuGiaCongViec_CapNgamCommand
{
    public record DeleteBieuGiaCongViec_CapNgamCommand(Guid id) : IRequest<bool>
    {
    }

    //Tạo thêm 1 class Handler kế thừa IRequestHandler<DeleteBieuGiaCongViec_CapNgamCommand, bool> rồi implement
    public class DeleteBieuGiaCongViec_CapNgamCommandHandler : IRequestHandler<DeleteBieuGiaCongViec_CapNgamCommand, bool> //
    {
        private readonly IUnitOfWork _unitOfWork; // khai báo 
        public DeleteBieuGiaCongViec_CapNgamCommandHandler(IUnitOfWork unitOfWork) //cấu hình dependence
        {
            _unitOfWork = unitOfWork; // khai báo
        }

        public async Task<bool> Handle(DeleteBieuGiaCongViec_CapNgamCommand request, CancellationToken cancellationToken)
        {
            // tìm kiếm xem có ID trong bảng BieuGiaCongViec_CapNgam không
            var entity = await _unitOfWork.BieuGiaCongViec_CapNgamRepository.FindOneAsync(x => x.Id == request.id);
            // nếu không có dữ liệu thì thêm mới
            if (entity == null)
            {
                throw new EvnException(string.Format(Resources.MSG_NOT_FOUND, "biểu giá công việc"));
            }

            entity.IsDeleted = true; // xoá mềm 
            //xoá trong DB
            _unitOfWork.BieuGiaCongViec_CapNgamRepository.Update(entity);
            //lưu lại trong DB
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}
