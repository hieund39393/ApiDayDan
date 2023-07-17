using Authentication.Infrastructure.AggregatesModel.DonGiaNhanCongAggregate;
using Authentication.Infrastructure.Repositories;
using MediatR;

namespace Authentication.Application.Commands.DonGiaNhanCongCommand
{
    public class CreateDonGiaNhanCongCommand : IRequest<bool> // kế thừa IRequest<bool>
    {
        public Guid? IdNhanCong { get; set; }
        public decimal DonGia { get; set; }
        public decimal? DinhMuc { get; set; }
    }

    //Tạo thêm 1 class Handler kế thừa IRequestHandler<CreateDonGiaNhanCongCommand, bool> rồi implement
    public class CreateDonGiaNhanCongCommandHandler : IRequestHandler<CreateDonGiaNhanCongCommand, bool> //
    {
        private readonly IUnitOfWork _unitOfWork; // khai báo 
        public CreateDonGiaNhanCongCommandHandler(IUnitOfWork unitOfWork) //cấu hình dependence
        {
            _unitOfWork = unitOfWork; // khai báo
        }
        public async Task<bool> Handle(CreateDonGiaNhanCongCommand request, CancellationToken cancellationToken)
        {
            // Tạo model DonGiaNhanCong_CapNgam
            var model = new DonGiaNhanCong
            {
                IdNhanCong = request.IdNhanCong,
                DonGia = request.DonGia,
                DinhMuc = request.DinhMuc,
            };
            _unitOfWork.DonGiaNhanCongRepository.Add(model);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}
