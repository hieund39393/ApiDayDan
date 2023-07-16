using Authentication.Infrastructure.AggregatesModel.DonGiaVatLieuAggregate;
using Authentication.Infrastructure.Properties;
using Authentication.Infrastructure.Repositories;
using EVN.Core.Exceptions;
using MediatR;

namespace Authentication.Application.Commands.DonGiaVatLieuCommand
{
    public class CreateDonGiaVatLieu_CapNgamCommand : IRequest<bool> // kế thừa IRequest<bool>
    {
        public Guid IdVatLieu { get; set; }
        public string VanBan { get; set; }
        public decimal DonGia { get; set; }
        public decimal? DinhMuc { get; set; }
    }

    //Tạo thêm 1 class Handler kế thừa IRequestHandler<CreateDonGiaVatLieu_CapNgamCommand, bool> rồi implement
    public class CreateDonGiaVatLieu_CapNgamCommandHandler : IRequestHandler<CreateDonGiaVatLieu_CapNgamCommand, bool> //
    {
        private readonly IUnitOfWork _unitOfWork; // khai báo 
        public CreateDonGiaVatLieu_CapNgamCommandHandler(IUnitOfWork unitOfWork) //cấu hình dependence
        {
            _unitOfWork = unitOfWork; // khai báo
        }
        public async Task<bool> Handle(CreateDonGiaVatLieu_CapNgamCommand request, CancellationToken cancellationToken)
        {
            // tìm kiếm xem có mã loại cáp trong db không
            var entity = await _unitOfWork.DonGiaVatLieu_CapNgamRepository.FindOneAsync(x => x.IdVatLieu == request.IdVatLieu && x.VanBan == request.VanBan);
            // nếu không có dữ liệu thì thêm mới
            if (entity == null)
            {
                // Tạo model DonGiaVatLieu_CapNgam
                var model = new DonGiaVatLieu_CapNgam
                {
                    IdVatLieu = request.IdVatLieu,
                    VanBan = request.VanBan,
                    DonGia = request.DonGia,
                    DinhMuc = request.DinhMuc,
                };
                //thêm vào DB
                _unitOfWork.DonGiaVatLieu_CapNgamRepository.Add(model);
                //lưu lại trong DB
                await _unitOfWork.SaveChangesAsync();
                return true;
            }
            // nếu đã tồn tạo 1 bản ghi
            throw new EvnException(string.Format(Resources.MSG_IS_EXIST, "Đơn giá vật liệu cáp ngầm"));
        }
    }
}
