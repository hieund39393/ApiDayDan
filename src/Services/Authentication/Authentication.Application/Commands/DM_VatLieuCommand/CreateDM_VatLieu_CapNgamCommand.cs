using Authentication.Infrastructure.AggregatesModel.DM_VatLieuAggregate;
using Authentication.Infrastructure.Properties;
using Authentication.Infrastructure.Repositories;
using EVN.Core.Exceptions;
using MediatR;

namespace Authentication.Application.Commands.DM_VatLieu_CapNgamCommand
{
    public class CreateDM_VatLieu_CapNgamCommand : IRequest<bool> // kế thừa IRequest<bool>
    {
        public string TenVatLieu { get; set; }
        public string MaVatLieu { get; set; }
        public string DonViTinh { get; set; }
    }

    //Tạo thêm 1 class Handler kế thừa IRequestHandler<CreateDM_VatLieu_CapNgamCommand, bool> rồi implement
    public class CreateDM_VatLieu_CapNgamCommandHandler : IRequestHandler<CreateDM_VatLieu_CapNgamCommand, bool> //
    {
        private readonly IUnitOfWork _unitOfWork; // khai báo 
        public CreateDM_VatLieu_CapNgamCommandHandler(IUnitOfWork unitOfWork) //cấu hình dependence
        {
            _unitOfWork = unitOfWork; // khai báo
        }
        public async Task<bool> Handle(CreateDM_VatLieu_CapNgamCommand request, CancellationToken cancellationToken) 
        {
            // tìm kiếm xem có mã loại cáp trong db không
            var entity = await _unitOfWork.DM_VatLieu_CapNgamRepository.FindOneAsync(x => x.TenVatLieu == request.TenVatLieu && x.MaVatLieu == request.MaVatLieu);
            // nếu không có dữ liệu thì thêm mới
            if (entity == null)
            {
                // Tạo model DM_VatLieu_CapNgam
                var model = new DM_VatLieu_CapNgam
                {
                    TenVatLieu = request.TenVatLieu ,
                    MaVatLieu = request.MaVatLieu ,
                    DonViTinh = request.DonViTinh,
                };
                //thêm vào DB
                _unitOfWork.DM_VatLieu_CapNgamRepository.Add(model);
                //lưu lại trong DB
                await _unitOfWork.SaveChangesAsync(); 
                return true;
            }
            // nếu đã tồn tạo 1 bản ghi
            throw new EvnException(string.Format(Resources.MSG_IS_EXIST, "Danh mục vật liệu cáp ngầm"));
        }
    }
}
