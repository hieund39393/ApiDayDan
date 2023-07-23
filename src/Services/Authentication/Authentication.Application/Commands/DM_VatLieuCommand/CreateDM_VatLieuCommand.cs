using Authentication.Infrastructure.AggregatesModel.DM_VatLieuAggregate;
using Authentication.Infrastructure.Properties;
using Authentication.Infrastructure.Repositories;
using EVN.Core.Exceptions;
using MediatR;

namespace Authentication.Application.Commands.DM_VatLieuCommand
{
    public class CreateDM_VatLieuCommand : IRequest<bool> // kế thừa IRequest<bool>
    {
        public string TenVatLieu { get; set; }
        public string MaVatLieu { get; set; }
        public string DonViTinh { get; set; }
        public int ThuTuHienThi { get; set; }
    }

    //Tạo thêm 1 class Handler kế thừa IRequestHandler<CreateDM_VatLieuCommand, bool> rồi implement
    public class CreateDM_VatLieuCommandHandler : IRequestHandler<CreateDM_VatLieuCommand, bool> //
    {
        private readonly IUnitOfWork _unitOfWork; // khai báo 
        public CreateDM_VatLieuCommandHandler(IUnitOfWork unitOfWork) //cấu hình dependence
        {
            _unitOfWork = unitOfWork; // khai báo
        }
        public async Task<bool> Handle(CreateDM_VatLieuCommand request, CancellationToken cancellationToken) 
        {
            // tìm kiếm xem có mã loại cáp trong db không
            var entity = await _unitOfWork.DM_VatLieuRepository.FindOneAsync(x => x.TenVatLieu == request.TenVatLieu && x.MaVatLieu == request.MaVatLieu);
            // nếu không có dữ liệu thì thêm mới
            if (entity == null)
            {
                // Tạo model DM_VatLieu
                var model = new DM_VatLieu
                {
                    TenVatLieu = request.TenVatLieu ,
                    MaVatLieu = request.MaVatLieu ,
                    DonViTinh = request.DonViTinh,
                    ThuTuHienThi = request.ThuTuHienThi,
                };
                //thêm vào DB
                _unitOfWork.DM_VatLieuRepository.Add(model);
                //lưu lại trong DB
                await _unitOfWork.SaveChangesAsync(); 
                return true;
            }
            // nếu đã tồn tạo 1 bản ghi
            throw new EvnException(string.Format(Resources.MSG_IS_EXIST, "Danh mục vật liệu"));
        }
    }
}
