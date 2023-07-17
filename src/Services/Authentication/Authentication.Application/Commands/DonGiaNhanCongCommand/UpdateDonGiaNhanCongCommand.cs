using Authentication.Infrastructure.AggregatesModel.DonGiaNhanCongAggregate;
using Authentication.Infrastructure.Properties;
using Authentication.Infrastructure.Repositories;
using EVN.Core.Exceptions;
using MediatR;

namespace Authentication.Application.Commands.DonGiaNhanCongCommand
{
    public class UpdateDonGiaNhanCongCommand : IRequest<bool> // kế thừa IRequest<bool>
    {
        public Guid Id { get; set; } // thêm ID
        public string CapBac { get; set; }
        public string HeSo { get; set; }
        public Guid? IdVung { get; set; }
        public Guid? IdKhuVuc { get; set; }
        public decimal DonGia { get; set; }
        public decimal? DinhMuc { get; set; }
    }

    //Tạo thêm 1 class Handler kế thừa IRequestHandler<UpdateDonGiaNhanCongCommand, bool> rồi implement
    public class UpdateDonGiaNhanCongCommandHandler : IRequestHandler<UpdateDonGiaNhanCongCommand, bool> //
    {
        private readonly IUnitOfWork _unitOfWork; // khai báo 
        public UpdateDonGiaNhanCongCommandHandler(IUnitOfWork unitOfWork) //cấu hình dependence
        {
            _unitOfWork = unitOfWork; // khai báo
        }
        public async Task<bool> Handle(UpdateDonGiaNhanCongCommand request, CancellationToken cancellationToken)
        {
            // tìm kiếm xem có ID trong bảng DonGiaNhanCong không
            var entity = await _unitOfWork.DonGiaNhanCongRepository.FindOneAsync(x => x.Id == request.Id);
            // nếu không có dữ liệu
            if (entity == null)
            {
                throw new EvnException(string.Format(Resources.MSG_NOT_FOUND, "Đơn giá nhân công"));
            }

            entity.DonGia = request.DonGia;
            entity.DinhMuc = request.DinhMuc;

            _unitOfWork.DonGiaNhanCongRepository.Update(entity);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}
