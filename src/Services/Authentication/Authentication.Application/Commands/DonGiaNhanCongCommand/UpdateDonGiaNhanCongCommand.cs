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
            if (entity.CapBac == request.CapBac && entity.HeSo == request.HeSo && entity.IdKhuVuc == request.IdKhuVuc)
            {
                entity.DonGia = request.DonGia;
            }
            else
            {
                var checkEntity = await _unitOfWork.DonGiaNhanCongRepository.FindOneAsync(x => entity.CapBac == request.CapBac && entity.HeSo == request.HeSo && entity.IdKhuVuc == request.IdKhuVuc);
                if (checkEntity != null)
                {
                    throw new EvnException(string.Format(Resources.MSG_IS_EXIST, "Đơn giá nhân công"));
                }
                entity.CapBac = request.CapBac;
                entity.HeSo = request.HeSo;
                entity.IdKhuVuc = request.IdKhuVuc;
                entity.DonGia = request.DonGia;
            }

            //thêm vào DB
            _unitOfWork.DonGiaNhanCongRepository.Update(entity);
            //lưu lại trong DB
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}
