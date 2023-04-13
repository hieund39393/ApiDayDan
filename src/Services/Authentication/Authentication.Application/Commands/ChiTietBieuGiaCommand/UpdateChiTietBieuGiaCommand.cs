using Authentication.Infrastructure.Properties;
using Authentication.Infrastructure.Repositories;
using EVN.Core.Exceptions;
using MediatR;

namespace Authentication.Application.Commands.ChiTietBieuGiaCommand
{
    public class UpdateChiTietBieuGiaCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
        public Guid? IdBieuGia { get; set; }
        public Guid? IdCongViec { get; set; }
        public int Nam { get; set; }
        public int Quy { get; set; }
        public decimal SoLuong { get; set; }
        public decimal HeSoDieuChinh_K1nc { get; set; }
        public decimal HeSoDieuChinh_K2nc { get; set; }
        public decimal HeSoDieuChinh_K2mnc { get; set; }
        public decimal DonGia_VL { get; set; }
        public decimal DonGia_NC { get; set; }
        public decimal DonGia_MTC { get; set; }
    }
    public class UpdateChiTietBieuGiaCommandHandler : IRequestHandler<UpdateChiTietBieuGiaCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        public UpdateChiTietBieuGiaCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<bool> Handle(UpdateChiTietBieuGiaCommand request, CancellationToken cancellationToken)
        {
            // tìm kiếm xem có ID trong bảng DM_LoaiCap không
            var entity = await _unitOfWork.ChiTietBieuGiaRepository.FindOneAsync(x => x.Id == request.Id);
            // nếu không có dữ liệu
            if (entity == null)
            {
                throw new EvnException(string.Format(Resources.MSG_NOT_FOUND, "Chi tiết biểu giá"));
            }
            if (entity.IDBieuGia == request.IdBieuGia && entity.IDCongViec == request.IdCongViec)
            {
                entity.Nam = request.Nam;
                entity.Quy = request.Quy;
                entity.SoLuong = request.SoLuong;
                entity.HeSoDieuChinh_K1nc = request.HeSoDieuChinh_K1nc;
                entity.HeSoDieuChinh_K2nc = request.HeSoDieuChinh_K2nc;
                entity.HeSoDieuChinh_K2mnc = request.HeSoDieuChinh_K2mnc;
                entity.DonGia_VL = request.DonGia_VL;
                entity.DonGia_NC = request.DonGia_NC;
                entity.DonGia_MTC = request.DonGia_MTC;

            }
            else
            {
                var checkEntity = await _unitOfWork.ChiTietBieuGiaRepository.FindOneAsync(x => entity.IDBieuGia == request.IdBieuGia && entity.IDCongViec == request.IdCongViec);
                if (checkEntity != null)
                {
                    throw new EvnException(string.Format(Resources.MSG_IS_EXIST, "Chi tiết biểu giá"));
                }
                entity.Nam = request.Nam;
                entity.Quy = request.Quy;
                entity.SoLuong = request.SoLuong;
                entity.HeSoDieuChinh_K1nc = request.HeSoDieuChinh_K1nc;
                entity.HeSoDieuChinh_K2nc = request.HeSoDieuChinh_K2nc;
                entity.HeSoDieuChinh_K2mnc = request.HeSoDieuChinh_K2mnc;
                entity.DonGia_VL = request.DonGia_VL;
                entity.DonGia_NC = request.DonGia_NC;
                entity.DonGia_MTC = request.DonGia_MTC;
            }


            //thêm vào DB
            _unitOfWork.ChiTietBieuGiaRepository.Update(entity);
            //lưu lại trong DB
            await _unitOfWork.SaveChangesAsync();
            return true;

        }
    }
}
