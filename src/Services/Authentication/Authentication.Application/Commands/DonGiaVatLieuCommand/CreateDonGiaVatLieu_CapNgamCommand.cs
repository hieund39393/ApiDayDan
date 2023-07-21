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
        public int VungKhuVuc { get; set; }
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
                // Tạo model DonGiaVatLieu_CapNgam
                var model = new DonGiaVatLieu_CapNgam
                {
                    IdVatLieu = request.IdVatLieu,
                    VanBan = request.VanBan,
                    DonGia = request.DonGia,
                    DinhMuc = request.DinhMuc,
                    VungKhuVuc = request.VungKhuVuc,
                };
                //thêm vào DB
                _unitOfWork.DonGiaVatLieu_CapNgamRepository.Add(model);
                //lưu lại trong DB
                await _unitOfWork.SaveChangesAsync();
                return true;
          
        }
    }
}
