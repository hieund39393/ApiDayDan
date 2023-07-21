using Authentication.Infrastructure.AggregatesModel.DonGiaVatLieuAggregate;
using Authentication.Infrastructure.Properties;
using Authentication.Infrastructure.Repositories;
using EVN.Core.Exceptions;
using MediatR;

namespace Authentication.Application.Commands.DonGiaMTCCommand
{
    public class CreateDonGiaMTC_CapNgamCommand : IRequest<bool> // kế thừa IRequest<bool>
    {
        public Guid IdMTC { get; set; }
        public string VanBan { get; set; }
        public decimal DonGia { get; set; }
        public decimal? DinhMuc { get; set; }
        public int VungKhuVuc { get; set; }
    }

    //Tạo thêm 1 class Handler kế thừa IRequestHandler<CreateDonGiaMTC_CapNgamCommand, bool> rồi implement
    public class CreateDonGiaMTC_CapNgamCommandHandler : IRequestHandler<CreateDonGiaMTC_CapNgamCommand, bool> //
    {
        private readonly IUnitOfWork _unitOfWork; // khai báo 
        public CreateDonGiaMTC_CapNgamCommandHandler(IUnitOfWork unitOfWork) //cấu hình dependence
        {
            _unitOfWork = unitOfWork; // khai báo
        }
        public async Task<bool> Handle(CreateDonGiaMTC_CapNgamCommand request, CancellationToken cancellationToken)
        {
            // tìm kiếm xem có mã loại cáp trong db không
            var entity = await _unitOfWork.DonGiaMTC_CapNgamRepository.FindOneAsync(x => x.IdMTC == request.IdMTC && x.VanBan == request.VanBan);
            // nếu không có dữ liệu thì thêm mới
            if (entity == null)
            {
                // Tạo model DonGiaMTC_CapNgam
                var model = new DonGiaMTC_CapNgam
                {
                    IdMTC = request.IdMTC,
                    VanBan = request.VanBan,
                    DonGia = request.DonGia,
                    DinhMuc = request.DinhMuc,
                    VungKhuVuc = request.VungKhuVuc,
                };
                //thêm vào DB
                _unitOfWork.DonGiaMTC_CapNgamRepository.Add(model);
                //lưu lại trong DB
                await _unitOfWork.SaveChangesAsync();
                return true;
            }
            // nếu đã tồn tạo 1 bản ghi
            throw new EvnException(string.Format(Resources.MSG_IS_EXIST, "Đơn giá máy thi công cáp ngầm"));
        }
    }
}
