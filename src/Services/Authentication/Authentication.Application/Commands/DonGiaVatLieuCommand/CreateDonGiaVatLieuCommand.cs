using Authentication.Infrastructure.AggregatesModel.DonGiaVatLieuAggregate;
using Authentication.Infrastructure.Properties;
using Authentication.Infrastructure.Repositories;
using EVN.Core.Exceptions;
using MediatR;

namespace Authentication.Application.Commands.DonGiaVatLieuCommand
{
    public class CreateDonGiaVatLieuCommand : IRequest<bool> // kế thừa IRequest<bool>
    {
        public Guid IdVatLieu { get; set; }
        public string VanBan { get; set; }
        public decimal DonGia { get; set; }
        public decimal? DinhMuc { get; set; }
    }

    //Tạo thêm 1 class Handler kế thừa IRequestHandler<CreateDonGiaVatLieuCommand, bool> rồi implement
    public class CreateDonGiaVatLieuCommandHandler : IRequestHandler<CreateDonGiaVatLieuCommand, bool> //
    {
        private readonly IUnitOfWork _unitOfWork; // khai báo 
        public CreateDonGiaVatLieuCommandHandler(IUnitOfWork unitOfWork) //cấu hình dependence
        {
            _unitOfWork = unitOfWork; // khai báo
        }
        public async Task<bool> Handle(CreateDonGiaVatLieuCommand request, CancellationToken cancellationToken)
        {
            // tìm kiếm xem có mã loại cáp trong db không
            var entity = await _unitOfWork.DonGiaVatLieuRepository.FindOneAsync(x => x.IdVatLieu == request.IdVatLieu && x.VanBan == request.VanBan);
            // nếu không có dữ liệu thì thêm mới
            if (entity == null)
            {
                // Tạo model DonGiaVatLieu
                var model = new DonGiaVatLieu
                {
                    IdVatLieu = request.IdVatLieu ,
                    VanBan = request.VanBan ,
                    DonGia = request.DonGia,
                    DinhMuc = request.DinhMuc,
                };
                //thêm vào DB
                _unitOfWork.DonGiaVatLieuRepository.Add(model);
                //lưu lại trong DB
                await _unitOfWork.SaveChangesAsync();
                return true;
            }
            // nếu đã tồn tạo 1 bản ghi
            throw new EvnException(string.Format(Resources.MSG_IS_EXIST, "Đơn giá vật liệu"));
        }
    }
}
