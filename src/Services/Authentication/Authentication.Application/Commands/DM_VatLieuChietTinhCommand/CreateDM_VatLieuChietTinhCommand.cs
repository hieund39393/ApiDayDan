using Authentication.Infrastructure.AggregatesModel.DM_VatLieuChietTinhAggregate;
using Authentication.Infrastructure.Properties;
using Authentication.Infrastructure.Repositories;
using EVN.Core.Exceptions;
using MediatR;

namespace Authentication.Application.Commands.DM_VatLieuChietTinhCommand
{
    public class CreateDM_VatLieuChietTinhCommand : IRequest<bool> // kế thừa IRequest<bool>
    {
        public string TenVatLieuChietTinh { get; set; }
        public string MaVatLieuChietTinh { get; set; }
        public string DonViTinh { get; set; }
    }

    //Tạo thêm 1 class Handler kế thừa IRequestHandler<CreateDM_VatLieuChietTinhCommand, bool> rồi implement
    public class CreateDM_VatLieuChietTinhCommandHandler : IRequestHandler<CreateDM_VatLieuChietTinhCommand, bool> //
    {
        private readonly IUnitOfWork _unitOfWork; // khai báo 
        public CreateDM_VatLieuChietTinhCommandHandler(IUnitOfWork unitOfWork) //cấu hình dependence
        {
            _unitOfWork = unitOfWork; // khai báo
        }
        public async Task<bool> Handle(CreateDM_VatLieuChietTinhCommand request, CancellationToken cancellationToken) 
        {
            // tìm kiếm xem có mã loại cáp trong db không
            var entity = await _unitOfWork.DM_VatLieuChietTinhRepository.FindOneAsync(x => x.TenVatLieuChietTinh == request.TenVatLieuChietTinh && x.MaVatLieuChietTinh == request.MaVatLieuChietTinh );
            // nếu không có dữ liệu thì thêm mới
            if (entity == null)
            {
                // Tạo model DM_VatLieuChietTinh
                var model = new DM_VatLieuChietTinh
                {
                    TenVatLieuChietTinh = request.TenVatLieuChietTinh,
                    MaVatLieuChietTinh = request.MaVatLieuChietTinh,
                    DonViTinh = request.DonViTinh,
                };
                //thêm vào DB
                _unitOfWork.DM_VatLieuChietTinhRepository.Add(model);
                //lưu lại trong DB
                await _unitOfWork.SaveChangesAsync(); 
                return true;
            }
            // nếu đã tồn tạo 1 bản ghi
            throw new EvnException(string.Format(Resources.MSG_IS_EXIST, "Vật liệu chiết tinh"));
        }
    }
}
