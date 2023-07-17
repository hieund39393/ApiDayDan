using Authentication.Infrastructure.AggregatesModel.DonGiaNhanCongAggregate;
using Authentication.Infrastructure.Properties;
using Authentication.Infrastructure.Repositories;
using EVN.Core.Exceptions;
using MediatR;

namespace Authentication.Application.Commands.DonGiaNhanCong_CapNgamCommand
{
    public class CreateDonGiaNhanCong_CapNgamCommand : IRequest<bool> // kế thừa IRequest<bool>
    {
        public Guid? IdNhanCong { get; set; }
        public decimal DonGia { get; set; }
        public decimal? DinhMuc { get; set; }
    }

    //Tạo thêm 1 class Handler kế thừa IRequestHandler<CreateDonGiaNhanCong_CapNgamCommand, bool> rồi implement
    public class CreateDonGiaNhanCong_CapNgamCommandHandler : IRequestHandler<CreateDonGiaNhanCong_CapNgamCommand, bool> //
    {
        private readonly IUnitOfWork _unitOfWork; // khai báo 
        public CreateDonGiaNhanCong_CapNgamCommandHandler(IUnitOfWork unitOfWork) //cấu hình dependence
        {
            _unitOfWork = unitOfWork; // khai báo
        }
        public async Task<bool> Handle(CreateDonGiaNhanCong_CapNgamCommand request, CancellationToken cancellationToken)
        {
            // Tạo model DonGiaNhanCong_CapNgam
            var model = new DonGiaNhanCong_CapNgam
            {
                IdNhanCong = request.IdNhanCong,
                DonGia = request.DonGia,
                DinhMuc = request.DinhMuc,
            };
            _unitOfWork.DonGiaNhanCong_CapNgamRepository.Add(model);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}
