using Authentication.Infrastructure.AggregatesModel.DonGiaVatLieuAggregate;
using Authentication.Infrastructure.Properties;
using Authentication.Infrastructure.Repositories;
using EVN.Core.Exceptions;
using MediatR;

namespace Authentication.Application.Commands.DonGiaMTCCommand
{
    public class CreateDonGiaMTCCommand : IRequest<bool> // kế thừa IRequest<bool>
    {
        public Guid IdMTC { get; set; }
        public string VanBan { get; set; }
        public decimal DonGia { get; set; }
        public decimal? DinhMuc { get; set; }
    }

    //Tạo thêm 1 class Handler kế thừa IRequestHandler<CreateDonGiaMTCCommand, bool> rồi implement
    public class CreateDonGiaMTCCommandHandler : IRequestHandler<CreateDonGiaMTCCommand, bool> //
    {
        private readonly IUnitOfWork _unitOfWork; // khai báo 
        public CreateDonGiaMTCCommandHandler(IUnitOfWork unitOfWork) //cấu hình dependence
        {
            _unitOfWork = unitOfWork; // khai báo
        }
        public async Task<bool> Handle(CreateDonGiaMTCCommand request, CancellationToken cancellationToken)
        {
            // tìm kiếm xem có mã loại cáp trong db không
            //var entity = await _unitOfWork.DonGiaMTCRepository.FindOneAsync(x => x.IdMTC == request.IdMTC && x.VanBan == request.VanBan);
            //// nếu không có dữ liệu thì thêm mới
            //if (entity == null)
            //{
                // Tạo model DonGiaMTC
                var model = new DonGiaMTC
                {
                    IdMTC = request.IdMTC ,
                    VanBan = request.VanBan ,
                    DonGia = request.DonGia,
                    DinhMuc = request.DinhMuc,
                };
                //thêm vào DB
                _unitOfWork.DonGiaMTCRepository.Add(model);
                //lưu lại trong DB
                await _unitOfWork.SaveChangesAsync();
                return true;
            //}
            //// nếu đã tồn tạo 1 bản ghi
            //throw new EvnException(string.Format(Resources.MSG_IS_EXIST, "Đơn giá máy thi công"));
        }
    }
}
