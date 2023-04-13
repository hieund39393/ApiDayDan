using Authentication.Infrastructure.AggregatesModel.DM_KhuVucAggregate;
using Authentication.Infrastructure.AggregatesModel.DM_VungAggregate;
using Authentication.Infrastructure.AggregatesModel.DonGiaChietTinhAggregate;
using Authentication.Infrastructure.Properties;
using Authentication.Infrastructure.Repositories;
using EVN.Core.Exceptions;
using MediatR;

namespace Authentication.Application.Commands.DonGiaChietTinhCommand
{
    public class CreateDonGiaChietTinhCommand : IRequest<bool> // kế thừa IRequest<bool>
    {
        public Guid? IdVatLieu { get; set; }
        public decimal DonGia { get; set; }
        public int IdPhanLoai{ get; set; }
    }

    //Tạo thêm 1 class Handler kế thừa IRequestHandler<CreateDonGiaChietTinhCommand, bool> rồi implement
    public class CreateDonGiaChietTinhCommandHandler : IRequestHandler<CreateDonGiaChietTinhCommand, bool> //
    {
        private readonly IUnitOfWork _unitOfWork; // khai báo 
        public CreateDonGiaChietTinhCommandHandler(IUnitOfWork unitOfWork) //cấu hình dependence
        {
            _unitOfWork = unitOfWork; // khai báo
        }
        public async Task<bool> Handle(CreateDonGiaChietTinhCommand request, CancellationToken cancellationToken)
        {
            // tìm kiếm xem có mã loại cáp trong db không
            var entity = await _unitOfWork.DonGiaChietTinhRepository.FindOneAsync(x =>
            x.IdVatLieu == request.IdVatLieu);
            // nếu không có dữ liệu thì thêm mới
            if (entity == null)
            {
                // Tạo model DonGiaChietTinh
                var model = new DonGiaChietTinh
                {
                    IdVatLieu = request.IdVatLieu,
                    DonGia = request.DonGia,
                    IdPhanLoai = request.IdPhanLoai,
                };
                //thêm vào DB
                _unitOfWork.DonGiaChietTinhRepository.Add(model);
                //lưu lại trong DB
                await _unitOfWork.SaveChangesAsync();
                return true;
            }
            // nếu đã tồn tạo 1 bản ghi
            throw new EvnException(string.Format(Resources.MSG_IS_EXIST, "Đơn giá chiết tinh"));
        }
    }
}
