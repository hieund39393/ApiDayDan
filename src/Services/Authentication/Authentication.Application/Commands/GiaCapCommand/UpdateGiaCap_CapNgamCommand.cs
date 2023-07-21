using Authentication.Infrastructure.Properties;
using Authentication.Infrastructure.Repositories;
using EVN.Core.Exceptions;
using MediatR;

namespace Authentication.Application.Commands.GiaCapCommand
{
    public class UpdateGiaCap_CapNgamCommand : IRequest<bool> // kế thừa IRequest<bool>
    {
        public Guid Id { get; set; } // thêm ID
        public Guid IdLoaiCap { get; set; }
        public string VanBan { get; set; }
        public decimal DonGia { get; set; }
        public int VungKhuVuc { get; set; }
    }

    //Tạo thêm 1 class Handler kế thừa IRequestHandler<UpdateGiaCap_CapNgamCommand, bool> rồi implement
    public class UpdateGiaCap_CapNgamCommandHandler : IRequestHandler<UpdateGiaCap_CapNgamCommand, bool> //
    {
        private readonly IUnitOfWork _unitOfWork; // khai báo 
        public UpdateGiaCap_CapNgamCommandHandler(IUnitOfWork unitOfWork) //cấu hình dependence
        {
            _unitOfWork = unitOfWork; // khai báo
        }
        public async Task<bool> Handle(UpdateGiaCap_CapNgamCommand request, CancellationToken cancellationToken)
        {
            // tìm kiếm xem có ID trong bảng GiaCap không
            var entity = await _unitOfWork.GiaCap_CapNgamRepository.FindOneAsync(x => x.Id == request.Id);
            // nếu không có dữ liệu
            if (entity == null)
            {
                throw new EvnException(string.Format(Resources.MSG_NOT_FOUND, "Giá cáp"));
            }

            entity.DonGia = request.DonGia;
            entity.VungKhuVuc = request.VungKhuVuc;

            //thêm vào DB
            _unitOfWork.GiaCap_CapNgamRepository.Update(entity);
            //lưu lại trong DB
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}
