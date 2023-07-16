using Authentication.Infrastructure.AggregatesModel.DonGiaNhanCongAggregate;
using Authentication.Infrastructure.AggregatesModel.DonGiaVatLieuAggregate;
using Authentication.Infrastructure.Properties;
using Authentication.Infrastructure.Repositories;
using EVN.Core.Exceptions;
using MediatR;

namespace Authentication.Application.Commands.DonGiaVatLieuCommand
{
    public class UpdateDonGiaVatLieuCommand : IRequest<bool> // kế thừa IRequest<bool>
    {
        public Guid Id { get; set; } // thêm ID
        public Guid IdVatLieu { get; set; }
        public string VanBan { get; set; }
        public decimal DonGia { get; set; }
        public decimal? DinhMuc { get; set; }
    }

    //Tạo thêm 1 class Handler kế thừa IRequestHandler<UpdateDonGiaVatLieuCommand, bool> rồi implement
    public class UpdateDonGiaVatLieuCommandHandler : IRequestHandler<UpdateDonGiaVatLieuCommand, bool> //
    {
        private readonly IUnitOfWork _unitOfWork; // khai báo 
        public UpdateDonGiaVatLieuCommandHandler(IUnitOfWork unitOfWork) //cấu hình dependence
        {
            _unitOfWork = unitOfWork; // khai báo
        }
        public async Task<bool> Handle(UpdateDonGiaVatLieuCommand request, CancellationToken cancellationToken)
        {
            // tìm kiếm xem có ID trong bảng DonGiaVatLieu không
            var entity = await _unitOfWork.DonGiaVatLieuRepository.FindOneAsync(x => x.Id == request.Id);
            // nếu không có dữ liệu
            if (entity == null)
            {
                throw new EvnException(string.Format(Resources.MSG_NOT_FOUND, "Đơn giá vật liệu"));
            }


            //var model = new DonGiaVatLieu()
            //{
            //    IdVatLieu = entity.IdVatLieu,
            //    VanBan = entity.VanBan,
            //    DonGiaCu = entity.DonGia,
            //    DonGia = request.DonGia,
            //    DinhMucCu = entity.DinhMuc,
            //    DinhMuc = request.DinhMuc,
            //};
            //_unitOfWork.DonGiaVatLieuRepository.Add(model);


            if (entity.IdVatLieu == request.IdVatLieu && entity.VanBan == request.VanBan)
            {
                entity.DonGiaCu = entity.DonGia;
                entity.DonGia = request.DonGia;
                entity.DinhMucCu = entity.DinhMuc;
                entity.DinhMuc = request.DinhMuc;
            }
            else
            {
                var checkEntity = await _unitOfWork.DonGiaVatLieuRepository.FindOneAsync(x => x.IdVatLieu == request.IdVatLieu && x.VanBan == request.VanBan);
                if (checkEntity != null)
                {
                    throw new EvnException(string.Format(Resources.MSG_IS_EXIST, "Đơn giá vật liệu"));
                }
                entity.IdVatLieu = request.IdVatLieu;
                entity.VanBan = request.VanBan;
                entity.DonGiaCu = entity.DonGia;
                entity.DonGia = request.DonGia;
                entity.DinhMucCu = entity.DinhMuc;
                entity.DinhMuc = request.DinhMuc;
            }

            _unitOfWork.DonGiaVatLieuRepository.Update(entity);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}
