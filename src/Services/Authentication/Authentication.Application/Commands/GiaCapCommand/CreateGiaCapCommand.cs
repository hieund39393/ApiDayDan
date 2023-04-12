using Authentication.Infrastructure.AggregatesModel.GiaCapAggregate;
using Authentication.Infrastructure.Properties;
using Authentication.Infrastructure.Repositories;
using EVN.Core.Exceptions;
using MediatR;

namespace Authentication.Application.Commands.GiaCapCommand
{
    public class CreateGiaCapCommand : IRequest<bool> // kế thừa IRequest<bool>
    {
        public Guid IdLoaiCap { get; set; }
        public string VanBan { get; set; }
        public decimal DonGia { get; set; }
    }

    //Tạo thêm 1 class Handler kế thừa IRequestHandler<CreateGiaCapCommand, bool> rồi implement
    public class CreateGiaCapCommandHandler : IRequestHandler<CreateGiaCapCommand, bool> //
    {
        private readonly IUnitOfWork _unitOfWork; // khai báo 
        public CreateGiaCapCommandHandler(IUnitOfWork unitOfWork) //cấu hình dependence
        {
            _unitOfWork = unitOfWork; // khai báo
        }
        public async Task<bool> Handle(CreateGiaCapCommand request, CancellationToken cancellationToken)
        {
            // tìm kiếm xem có mã loại cáp trong db không
            var entity = await _unitOfWork.GiaCapRepository.FindOneAsync(x => x.IdLoaiCap == request.IdLoaiCap && x.VanBan == request.VanBan);
            // nếu không có dữ liệu thì thêm mới
            if (entity == null)
            {
                // Tạo model GiaCap
                var model = new GiaCap
                {
                    IdLoaiCap = request.IdLoaiCap ,
                    VanBan = request.VanBan ,
                    DonGia = request.DonGia,
                };
                //thêm vào DB
                _unitOfWork.GiaCapRepository.Add(model);
                //lưu lại trong DB
                await _unitOfWork.SaveChangesAsync();
                return true;
            }
            // nếu đã tồn tạo 1 bản ghi
            throw new EvnException(string.Format(Resources.MSG_IS_EXIST, "Giá cáp"));
        }
    }
}
