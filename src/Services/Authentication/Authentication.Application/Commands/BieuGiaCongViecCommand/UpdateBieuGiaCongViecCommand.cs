using Authentication.Infrastructure.Properties;
using Authentication.Infrastructure.Repositories;
using EVN.Core.Exceptions;
using MediatR;

namespace Authentication.Application.Commands.BieuGiaCongViecCommand
{
    public class UpdateBieuGiaCongViecCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
        public Guid BieuGiaID { get; set; }
        public Guid CongViecID { get; set; }
        public Guid VungID { get; set; }
        public Guid KhuVucID { get; set; }
    }

    //Tạo thêm 1 class Handler kế thừa IRequestHandler<UpdateBieuGiaCongViecCommand, bool> rồi implement
    public class UpdateBieuGiaCongViecCommandHandler : IRequestHandler<UpdateBieuGiaCongViecCommand, bool> //
    {
        private readonly IUnitOfWork _unitOfWork; // khai báo 
        public UpdateBieuGiaCongViecCommandHandler(IUnitOfWork unitOfWork) //cấu hình dependence
        {
            _unitOfWork = unitOfWork; // khai báo
        }
        public async Task<bool> Handle(UpdateBieuGiaCongViecCommand request, CancellationToken cancellationToken)
        {
            // tìm kiếm xem có ID trong bảng BieuGiaCongViec không
            var entity = await _unitOfWork.BieuGiaCongViecRepository.FindOneAsync(x => x.Id == request.Id);
            

            // nếu không có dữ liệu
            if (entity == null)
            {
                throw new EvnException(string.Format(Resources.MSG_NOT_FOUND, "biểu giá công việc"));
            }
            var checkEntity = await _unitOfWork.BieuGiaCongViecRepository.FindOneAsync(x =>x.BieuGiaID == request.BieuGiaID &&
                                                                                       x.CongViecID == request.CongViecID &&
                                                                                       x.VungID == request.VungID &&
                                                                                       x.KhuVucID == request.KhuVucID);
            if (checkEntity != null)
            {
                throw new EvnException(string.Format(Resources.MSG_IS_EXIST, "Biểu giá công việc"));
            }

            entity.BieuGiaID = request.BieuGiaID;
            entity.CongViecID = request.CongViecID;
            entity.VungID = request.VungID;
            entity.KhuVucID = request.KhuVucID;
            //thêm vào DB
            _unitOfWork.BieuGiaCongViecRepository.Update(entity);
            //lưu lại trong DB
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}
