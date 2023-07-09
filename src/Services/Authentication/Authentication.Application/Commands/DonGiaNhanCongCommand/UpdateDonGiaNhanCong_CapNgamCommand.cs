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
            if (entity.CapBac == request.CapBac && entity.HeSo == request.HeSo && entity.IdKhuVuc == request.IdKhuVuc)
            {
                entity.DonGia = request.DonGia;
            }
            else
            {
                var checkEntity = await _unitOfWork.DonGiaNhanCong_CapNgamRepository.FindOneAsync(x => entity.CapBac == request.CapBac && entity.HeSo == request.HeSo && entity.IdKhuVuc == request.IdKhuVuc);
                if (checkEntity != null)
                {
                    throw new EvnException(string.Format(Resources.MSG_IS_EXIST, "Đơn giá nhân công cáp ngầm"));
                }
                entity.CapBac = request.CapBac;
                entity.HeSo = request.HeSo;
                entity.IdKhuVuc = request.IdKhuVuc;
                entity.DonGia = request.DonGia;
            }

            //thêm vào DB
            _unitOfWork.DonGiaNhanCong_CapNgamRepository.Update(entity);
            //lưu lại trong DB
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}
