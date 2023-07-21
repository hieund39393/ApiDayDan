using Authentication.Infrastructure.AggregatesModel.DonGiaNhanCongAggregate;
using Authentication.Infrastructure.AggregatesModel.DonGiaVatLieuAggregate;
using Authentication.Infrastructure.Properties;
using Authentication.Infrastructure.Repositories;
using EVN.Core.Exceptions;
using MediatR;

namespace Authentication.Application.Commands.DonGiaNhanCong_CapNgamCommand
{
    public class UpdateDonGiaNhanCong_CapNgamCommand : IRequest<bool> // kế thừa IRequest<bool>
    {
        public Guid Id { get; set; } // thêm ID
        public string CapBac { get; set; }
        public string HeSo { get; set; }
        public Guid? IdVung { get; set; }
        public Guid? IdKhuVuc { get; set; }
        public decimal DonGia { get; set; }
        public decimal? DinhMuc { get; set; }
        public int VungKhuVuc { get; set; }
    }

    //Tạo thêm 1 class Handler kế thừa IRequestHandler<UpdateDonGiaNhanCong_CapNgamCommand, bool> rồi implement
    public class UpdateDonGiaNhanCong_CapNgamCommandHandler : IRequestHandler<UpdateDonGiaNhanCong_CapNgamCommand, bool> //
    {
        private readonly IUnitOfWork _unitOfWork; // khai báo 
        public UpdateDonGiaNhanCong_CapNgamCommandHandler(IUnitOfWork unitOfWork) //cấu hình dependence
        {
            _unitOfWork = unitOfWork; // khai báo
        }
        public async Task<bool> Handle(UpdateDonGiaNhanCong_CapNgamCommand request, CancellationToken cancellationToken)
        {
            // tìm kiếm xem có ID trong bảng DonGiaNhanCong_CapNgam không
            var entity = await _unitOfWork.DonGiaNhanCong_CapNgamRepository.FindOneAsync(x => x.Id == request.Id);
            // nếu không có dữ liệu
            if (entity == null)
            {
                throw new EvnException(string.Format(Resources.MSG_NOT_FOUND, "Đơn giá nhân công cáp ngầm"));
            }


            entity.DonGia = request.DonGia;
            entity.DinhMuc = request.DinhMuc;

            _unitOfWork.DonGiaNhanCong_CapNgamRepository.Update(entity);


            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}
