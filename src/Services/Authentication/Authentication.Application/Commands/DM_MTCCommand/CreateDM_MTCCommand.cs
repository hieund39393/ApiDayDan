using Authentication.Infrastructure.AggregatesModel.DM_MTCAggregate;
using Authentication.Infrastructure.Properties;
using Authentication.Infrastructure.Repositories;
using EVN.Core.Exceptions;
using MediatR;

namespace Authentication.Application.Commands.DM_MTCCommand
{
    public class CreateDM_MTCCommand : IRequest<bool> // kế thừa IRequest<bool>
    {
        public string TenMTC { get; set; }
        public string MaMTC { get; set; }
        public string DonViTinh { get; set; }
    }

    //Tạo thêm 1 class Handler kế thừa IRequestHandler<CreateDM_MTCCommand, bool> rồi implement
    public class CreateDM_MTCCommandHandler : IRequestHandler<CreateDM_MTCCommand, bool> //
    {
        private readonly IUnitOfWork _unitOfWork; // khai báo 
        public CreateDM_MTCCommandHandler(IUnitOfWork unitOfWork) //cấu hình dependence
        {
            _unitOfWork = unitOfWork; // khai báo
        }
        public async Task<bool> Handle(CreateDM_MTCCommand request, CancellationToken cancellationToken) 
        {
            // tìm kiếm xem có mã loại cáp trong db không
            var entity = await _unitOfWork.DM_MTCRepository.FindOneAsync(x => x.TenMayThiCong == request.TenMTC && x.MaMTC == request.MaMTC);
            // nếu không có dữ liệu thì thêm mới
            if (entity == null)
            {
                // Tạo model DM_MTC
                var model = new DM_MTC
                {
                    TenMayThiCong = request.TenMTC ,
                    MaMTC = request.MaMTC ,
                    DonViTinh = request.DonViTinh,
                };
                //thêm vào DB
                _unitOfWork.DM_MTCRepository.Add(model);
                //lưu lại trong DB
                await _unitOfWork.SaveChangesAsync(); 
                return true;
            }
            // nếu đã tồn tạo 1 bản ghi
            throw new EvnException(string.Format(Resources.MSG_IS_EXIST, "Máy thi công"));
        }
    }
}
