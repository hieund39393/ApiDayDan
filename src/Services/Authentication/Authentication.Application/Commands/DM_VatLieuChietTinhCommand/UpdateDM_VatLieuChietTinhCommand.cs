using Authentication.Infrastructure.Properties;
using Authentication.Infrastructure.Repositories;
using EVN.Core.Exceptions;
using MediatR;

namespace Authentication.Application.Commands.DM_VatLieuChietTinhCommand
{
    public class UpdateDM_VatLieuChietTinhCommand : IRequest<bool> // kế thừa IRequest<bool>
    {
        public Guid Id { get; set; } // thêm ID
        public string TenVatLieuChietTinh { get; set; }
        public string MaVatLieuChietTinh { get; set; }
        public string DonViTinh { get; set; }
    }

    //Tạo thêm 1 class Handler kế thừa IRequestHandler<UpdateDM_VatLieuChietTinhCommand, bool> rồi implement
    public class UpdateDM_VatLieuChietTinhCommandHandler : IRequestHandler<UpdateDM_VatLieuChietTinhCommand, bool> //
    {
        private readonly IUnitOfWork _unitOfWork; // khai báo 
        public UpdateDM_VatLieuChietTinhCommandHandler(IUnitOfWork unitOfWork) //cấu hình dependence
        {
            _unitOfWork = unitOfWork; // khai báo
        }
        public async Task<bool> Handle(UpdateDM_VatLieuChietTinhCommand request, CancellationToken cancellationToken)
        {
            // tìm kiếm xem có ID trong bảng DM_VatLieuChietTinh không
            var entity = await _unitOfWork.DM_VatLieuChietTinhRepository.FindOneAsync(x => x.Id == request.Id);
            // nếu không có dữ liệu
            if (entity == null)
            {
                throw new EvnException(string.Format(Resources.MSG_NOT_FOUND, "Danh mục vật liệu chiết tinh"));
            }

            entity.TenVatLieuChietTinh = request.TenVatLieuChietTinh ;
            entity.MaVatLieuChietTinh = request.MaVatLieuChietTinh ;
            entity.DonViTinh = request.DonViTinh;
            //thêm vào DB
            _unitOfWork.DM_VatLieuChietTinhRepository.Update(entity);
            //lưu lại trong DB
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}
