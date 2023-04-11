using Authentication.Infrastructure.AggregatesModel.BieuGiaCongViecAggregate;
using Authentication.Infrastructure.Properties;
using Authentication.Infrastructure.Repositories;
using EVN.Core.Exceptions;
using MediatR;

namespace Authentication.Application.Commands.BieuGiaCongViecCommand
{
    public class CreateBieuGiaCongViecCommand : IRequest<bool>
    {
        public Guid IdBieuGia { get; set; }
        public Guid IdCongViec { get; set; }
        public Guid VungID { get; set; }
        public Guid KhuVucID { get; set; }
    }

    //Tạo thêm 1 class Handler kế thừa IRequestHandler<CreateBieuGiaCongViecCommand, bool> rồi implement
    public class CreateBieuGiaCongViecCommandHandler : IRequestHandler<CreateBieuGiaCongViecCommand, bool> //
    {
        private readonly IUnitOfWork _unitOfWork; // khai báo 
        public CreateBieuGiaCongViecCommandHandler(IUnitOfWork unitOfWork) //cấu hình dependence
        {
            _unitOfWork = unitOfWork; // khai báo
        }
        public async Task<bool> Handle(CreateBieuGiaCongViecCommand request, CancellationToken cancellationToken)
        {
            // tìm kiếm xem có trùng trong db không
            var entity = await _unitOfWork.BieuGiaCongViecRepository.FindOneAsync(x => x.IdBieuGia == request.IdBieuGia &&
                                                                                       x.IdCongViec == request.IdCongViec);
            // nếu không có dữ liệu thì thêm mới
            if (entity == null)
            {
                // Tạo model BieuGiaCongViec
                var model = new BieuGiaCongViec
                {
                    IdBieuGia = request.IdBieuGia,
                    IdCongViec = request.IdCongViec,
                };
                //thêm vào DB
                _unitOfWork.BieuGiaCongViecRepository.Add(model);
                //lưu lại trong DB
                await _unitOfWork.SaveChangesAsync();
                return true;
            }
            // nếu đã tồn tạo 1 bản ghi
            throw new EvnException(string.Format(Resources.MSG_IS_EXIST, "Biểu giá công việc"));
        }
    }
}
