using Authentication.Infrastructure.AggregatesModel.DM_LoaiCapAggregate;
using Authentication.Infrastructure.Properties;
using Authentication.Infrastructure.Repositories;
using EVN.Core.Exceptions;
using MediatR;

namespace Authentication.Application.Commands.DM_LoaiCap_CapNgamCommand
{
    public class CreateDM_LoaiCap_CapNgamCommand : IRequest<bool> // kế thừa IRequest<bool>
    {
        public string TenLoaiCap { get; set; }
        public string MaLoaiCap { get; set; }
        public string DonViTinh { get; set; }
    }

    //Tạo thêm 1 class Handler kế thừa IRequestHandler<CreateDM_LoaiCap_CapNgamCommand, bool> rồi implement
    public class CreateDM_LoaiCap_CapNgamCommandHandler : IRequestHandler<CreateDM_LoaiCap_CapNgamCommand, bool> //
    {
        private readonly IUnitOfWork _unitOfWork; // khai báo 
        public CreateDM_LoaiCap_CapNgamCommandHandler(IUnitOfWork unitOfWork) //cấu hình dependence
        {
            _unitOfWork = unitOfWork; // khai báo
        }
        public async Task<bool> Handle(CreateDM_LoaiCap_CapNgamCommand request, CancellationToken cancellationToken)
        {
            // tìm kiếm xem có mã loại cáp trong db không
            var entity = await _unitOfWork.DM_LoaiCap_CapNgamRepository.FindOneAsync(x => x.MaLoaiCap == request.MaLoaiCap && x.TenLoaiCap == request.TenLoaiCap);
            // nếu không có dữ liệu thì thêm mới
            if (entity == null)
            {
                // Tạo model DM_LoaiCap_CapNgam
                var model = new DM_LoaiCap_CapNgam
                {
                    TenLoaiCap = request.TenLoaiCap,
                    MaLoaiCap = request.MaLoaiCap,
                    DonViTinh = request.DonViTinh,
                };
                //thêm vào DB
                _unitOfWork.DM_LoaiCap_CapNgamRepository.Add(model);
                //lưu lại trong DB
                await _unitOfWork.SaveChangesAsync();
                return true;
            }
            // nếu đã tồn tạo 1 bản ghi
            throw new EvnException(string.Format(Resources.MSG_IS_EXIST, "Loại cáp"));
        }
    }
}
