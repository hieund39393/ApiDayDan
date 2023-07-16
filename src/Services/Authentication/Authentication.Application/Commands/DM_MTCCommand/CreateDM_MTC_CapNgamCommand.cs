using Authentication.Infrastructure.AggregatesModel.DM_MTCAggregate;
using Authentication.Infrastructure.Properties;
using Authentication.Infrastructure.Repositories;
using EVN.Core.Exceptions;
using MediatR;

namespace Authentication.Application.Commands.DM_MTC_CapNgamCommand
{
    public class CreateDM_MTC_CapNgamCommand : IRequest<bool> // kế thừa IRequest<bool>
    {
        public string TenMTC { get; set; }
        public string MaMTC { get; set; }
        public string DonViTinh { get; set; }
    }

    //Tạo thêm 1 class Handler kế thừa IRequestHandler<CreateDM_MTC_CapNgamCommand, bool> rồi implement
    public class CreateDM_MTC_CapNgamCommandHandler : IRequestHandler<CreateDM_MTC_CapNgamCommand, bool> //
    {
        private readonly IUnitOfWork _unitOfWork; // khai báo 
        public CreateDM_MTC_CapNgamCommandHandler(IUnitOfWork unitOfWork) //cấu hình dependence
        {
            _unitOfWork = unitOfWork; // khai báo
        }
        public async Task<bool> Handle(CreateDM_MTC_CapNgamCommand request, CancellationToken cancellationToken) 
        {
            // tìm kiếm xem có mã loại cáp trong db không
            var entity = await _unitOfWork.DM_MTC_CapNgamRepository.FindOneAsync(x => x.TenMTC == request.TenMTC && x.MaMTC == request.MaMTC);
            // nếu không có dữ liệu thì thêm mới
            if (entity == null)
            {
                // Tạo model DM_MTC_CapNgam
                var model = new DM_MTC_CapNgam
                {
                    TenMTC = request.TenMTC ,
                    MaMTC = request.MaMTC ,
                    DonViTinh = request.DonViTinh,
                };
                //thêm vào DB
                _unitOfWork.DM_MTC_CapNgamRepository.Add(model);
                //lưu lại trong DB
                await _unitOfWork.SaveChangesAsync(); 
                return true;
            }
            // nếu đã tồn tạo 1 bản ghi
            throw new EvnException(string.Format(Resources.MSG_IS_EXIST, "Máy thi công cáp ngầm"));
        }
    }
}
