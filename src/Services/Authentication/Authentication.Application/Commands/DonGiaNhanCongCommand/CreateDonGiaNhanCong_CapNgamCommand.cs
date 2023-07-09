using Authentication.Infrastructure.AggregatesModel.DonGiaNhanCongAggregate;
using Authentication.Infrastructure.Properties;
using Authentication.Infrastructure.Repositories;
using EVN.Core.Exceptions;
using MediatR;

namespace Authentication.Application.Commands.DonGiaNhanCong_CapNgamCommand
{
    public class CreateDonGiaNhanCong_CapNgamCommand : IRequest<bool> // kế thừa IRequest<bool>
    {
        public string CapBac { get; set; }
        public string HeSo { get; set; }
        public Guid? IdVung { get; set; }
        public Guid? IdKhuVuc { get; set; }
        public decimal DonGia { get; set; }
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
            // tìm kiếm xem có mã loại cáp trong db không
            var entity = await _unitOfWork.DonGiaNhanCong_CapNgamRepository.FindOneAsync(x =>
            x.CapBac == request.CapBac &&
            x.HeSo == request.HeSo &&
            x.IdKhuVuc == request.IdKhuVuc &&
            x.DonGia == request.DonGia);
            // nếu không có dữ liệu thì thêm mới
            if (entity == null)
            {
                // Tạo model DonGiaNhanCong_CapNgam
                var model = new DonGiaNhanCong_CapNgam
                {
                    CapBac = request.CapBac ,
                    HeSo = request.HeSo ,
                    IdKhuVuc = request.IdKhuVuc ,
                    DonGia = request.DonGia,
                };
                //thêm vào DB
                _unitOfWork.DonGiaNhanCong_CapNgamRepository.Add(model);
                //lưu lại trong DB
                await _unitOfWork.SaveChangesAsync();
                return true;
            }
            // nếu đã tồn tạo 1 bản ghi
            throw new EvnException(string.Format(Resources.MSG_IS_EXIST, "Đơn giá nhân công cáp ngầm"));
        }
    }
}
