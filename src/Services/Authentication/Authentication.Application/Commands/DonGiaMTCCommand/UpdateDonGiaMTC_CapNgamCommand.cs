using Authentication.Infrastructure.AggregatesModel.DonGiaVatLieuAggregate;
using Authentication.Infrastructure.Properties;
using Authentication.Infrastructure.Repositories;
using EVN.Core.Exceptions;
using MediatR;

namespace Authentication.Application.Commands.DonGiaMTC_CapNgamCommand
{
    public class UpdateDonGiaMTC_CapNgamCommand : IRequest<bool> // kế thừa IRequest<bool>
    {
        public Guid Id { get; set; } // thêm ID
        public Guid IdMTC { get; set; }
        public string VanBan { get; set; }
        public decimal DonGia { get; set; }
        public decimal? DinhMuc { get; set; }
        public int VungKhuVuc { get; set; }
    }

    //Tạo thêm 1 class Handler kế thừa IRequestHandler<UpdateDonGiaMTC_CapNgamCommand, bool> rồi implement
    public class UpdateDonGiaMTC_CapNgamCommandHandler : IRequestHandler<UpdateDonGiaMTC_CapNgamCommand, bool> //
    {
        private readonly IUnitOfWork _unitOfWork; // khai báo 
        public UpdateDonGiaMTC_CapNgamCommandHandler(IUnitOfWork unitOfWork) //cấu hình dependence
        {
            _unitOfWork = unitOfWork; // khai báo
        }
        public async Task<bool> Handle(UpdateDonGiaMTC_CapNgamCommand request, CancellationToken cancellationToken)
        {
            var entity = await _unitOfWork.DonGiaMTC_CapNgamRepository.FindOneAsync(x => x.Id == request.Id);
            if (entity == null)
            {
                throw new EvnException(string.Format(Resources.MSG_NOT_FOUND, "Đơn giá máy thi công cáp ngầm"));
            }
         
            entity.DonGia = request.DonGia;
            entity.DinhMuc = request.DinhMuc;
            _unitOfWork.DonGiaMTC_CapNgamRepository.Update(entity);

            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}
