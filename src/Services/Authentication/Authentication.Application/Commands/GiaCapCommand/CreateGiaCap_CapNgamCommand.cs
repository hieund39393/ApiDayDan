using Authentication.Infrastructure.AggregatesModel.GiaCapAggregate;
using Authentication.Infrastructure.Properties;
using Authentication.Infrastructure.Repositories;
using EVN.Core.Exceptions;
using MediatR;

namespace Authentication.Application.Commands.GiaCapCommand
{
    public class CreateGiaCap_CapNgamCommand : IRequest<bool> // kế thừa IRequest<bool>
    {
        public Guid IdLoaiCap { get; set; }
        public string VanBan { get; set; }
        public decimal DonGia { get; set; }
        public int VungKhuVuc { get; set; }
    }

    //Tạo thêm 1 class Handler kế thừa IRequestHandler<CreateGiaCap_CapNgamCommand, bool> rồi implement
    public class CreateGiaCap_CapNgamCommandHandler : IRequestHandler<CreateGiaCap_CapNgamCommand, bool> //
    {
        private readonly IUnitOfWork _unitOfWork; // khai báo 
        public CreateGiaCap_CapNgamCommandHandler(IUnitOfWork unitOfWork) //cấu hình dependence
        {
            _unitOfWork = unitOfWork; // khai báo
        }
        public async Task<bool> Handle(CreateGiaCap_CapNgamCommand request, CancellationToken cancellationToken)
        {
            // tìm kiếm xem có mã loại cáp trong db không
            var entity = await _unitOfWork.GiaCap_CapNgamRepository.FindOneAsync(x => x.IdLoaiCap == request.IdLoaiCap && x.VanBan == request.VanBan);
            // nếu không có dữ liệu thì thêm mới
            if (entity == null)
            {
                // Tạo model GiaCap
                var model = new GiaCap_CapNgam
                {
                    IdLoaiCap = request.IdLoaiCap,
                    VanBan = request.VanBan,
                    DonGia = request.DonGia,
                    VungKhuVuc = request.VungKhuVuc
                };
                //thêm vào DB
                _unitOfWork.GiaCap_CapNgamRepository.Add(model);
                //lưu lại trong DB
                await _unitOfWork.SaveChangesAsync();
                return true;
            }
            // nếu đã tồn tạo 1 bản ghi
            throw new EvnException(string.Format(Resources.MSG_IS_EXIST, "Giá cáp"));
        }
    }
}
