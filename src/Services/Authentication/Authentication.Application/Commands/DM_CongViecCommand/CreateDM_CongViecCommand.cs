using Authentication.Infrastructure.AggregatesModel.DM_CongViecAggregate;
using Authentication.Infrastructure.Properties;
using Authentication.Infrastructure.Repositories;
using EVN.Core.Exceptions;
using MediatR;

namespace Authentication.Application.Commands.DM_CongViecCommand
{
    public class CreateDM_CongViecCommand : IRequest<bool> // kế thừa IRequest<bool>
    {
        public string TenCongViec { get; set; } 
        public string MaCongViec { get; set; } 
        public string DonViTinh { get; set; } 
        public int ThuTuHienThi { get; set; }
    }

    //Tạo thêm 1 class Handler kế thừa IRequestHandler<CreateDM_CongViecCommand, bool> rồi implement
    public class CreateDM_CongViecCommandHandler : IRequestHandler<CreateDM_CongViecCommand, bool> //
    {
        private readonly IUnitOfWork _unitOfWork; // khai báo 
        public CreateDM_CongViecCommandHandler(IUnitOfWork unitOfWork) //cấu hình dependence
        {
            _unitOfWork = unitOfWork; // khai báo
        }
        public async Task<bool> Handle(CreateDM_CongViecCommand request, CancellationToken cancellationToken) 
        {
            // tìm kiếm xem có mã công việc trong db không
            var entity = await _unitOfWork.DM_CongViecRepository.FindOneAsync(x => x.TenCongViec == request.TenCongViec && x.MaCongViec == request.MaCongViec);
            // nếu không có dữ liệu thì thêm mới
            if (entity == null)
            {
                // Tạo model DM_CongViec
                var model = new DM_CongViec
                {
                    MaCongViec = request.MaCongViec,
                    TenCongViec = request.TenCongViec,
                    DonViTinh= request.DonViTinh,
                    ThuTuHienThi = request.ThuTuHienThi,
                };
                //thêm vào DB
                _unitOfWork.DM_CongViecRepository.Add(model);
                //lưu lại trong DB
                await _unitOfWork.SaveChangesAsync(); 
                return true;
            }
            // nếu đã tồn tạo 1 bản ghi
            throw new EvnException(string.Format(Resources.MSG_IS_EXIST, "Công việc"));
        }
    }
}
