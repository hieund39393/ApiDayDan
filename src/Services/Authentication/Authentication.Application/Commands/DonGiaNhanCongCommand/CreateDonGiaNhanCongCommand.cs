using Authentication.Infrastructure.AggregatesModel.DonGiaNhanCongAggregate;
using Authentication.Infrastructure.Repositories;
using MediatR;

namespace Authentication.Application.Commands.DonGiaNhanCongCommand
{
    public class CreateDonGiaNhanCongCommand : IRequest<bool> // kế thừa IRequest<bool>
    {
        public string CapBac { get; set; }
        public string HeSo { get; set; }
        public Guid? IdVung { get; set; }
        public Guid? IdKhuVuc { get; set; }
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
            // tìm kiếm xem có mã loại cáp trong db không
            //var entity = await _unitOfWork.DonGiaNhanCongRepository.FindOneAsync(x =>
            //x.CapBac == request.CapBac &&
            //x.HeSo == request.HeSo &&
            //x.IdKhuVuc == request.IdKhuVuc &&
            //x.DonGia == request.DonGia);
            //// nếu không có dữ liệu thì thêm mới
            //if (entity == null)
            //{
            // Tạo model DonGiaNhanCong
            var model = new DonGiaNhanCong
            {
                CapBac = request.CapBac,
                HeSo = request.HeSo,
                IdKhuVuc = request.IdKhuVuc,
                DonGia = request.DonGia,
                DinhMuc = request.DinhMuc,
            };
            //thêm vào DB
            _unitOfWork.DonGiaNhanCongRepository.Add(model);
            //lưu lại trong DB
            await _unitOfWork.SaveChangesAsync();
            return true;
            //}
            //// nếu đã tồn tạo 1 bản ghi
            //throw new EvnException(string.Format(Resources.MSG_IS_EXIST, "Đơn giá nhân công"));
        }
    }
}
