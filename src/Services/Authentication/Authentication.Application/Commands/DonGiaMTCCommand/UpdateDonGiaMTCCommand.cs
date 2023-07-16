using Authentication.Infrastructure.AggregatesModel.DonGiaVatLieuAggregate;
using Authentication.Infrastructure.Properties;
using Authentication.Infrastructure.Repositories;
using EVN.Core.Exceptions;
using MediatR;

namespace Authentication.Application.Commands.DonGiaMTCCommand
{
    public class UpdateDonGiaMTCCommand : IRequest<bool> // kế thừa IRequest<bool>
    {
        public Guid Id { get; set; } // thêm ID
        public Guid IdMTC { get; set; }
        public string VanBan { get; set; }
        public decimal DonGia { get; set; }
        public decimal? DinhMuc { get; set; }
    }

    //Tạo thêm 1 class Handler kế thừa IRequestHandler<UpdateDonGiaMTCCommand, bool> rồi implement
    public class UpdateDonGiaMTCCommandHandler : IRequestHandler<UpdateDonGiaMTCCommand, bool> //
    {
        private readonly IUnitOfWork _unitOfWork; // khai báo 
        public UpdateDonGiaMTCCommandHandler(IUnitOfWork unitOfWork) //cấu hình dependence
        {
            _unitOfWork = unitOfWork; // khai báo
        }
        public async Task<bool> Handle(UpdateDonGiaMTCCommand request, CancellationToken cancellationToken)
        {
            // tìm kiếm xem có ID trong bảng DonGiaMTC không
            var entity = await _unitOfWork.DonGiaMTCRepository.FindOneAsync(x => x.Id == request.Id);
            // nếu không có dữ liệu
            if (entity == null)
            {
                throw new EvnException(string.Format(Resources.MSG_NOT_FOUND, "Đơn giá máy thi công"));
            }

            //var model = new DonGiaMTC()
            //{
            //    IdMTC = entity.IdMTC,
            //    VanBan = request.VanBan,
            //    DonGiaCu = entity.DonGia,
            //    DonGia = request.DonGia,
            //    DinhMucCu = entity.DinhMuc,
            //    DinhMuc = request.DinhMuc,
            //};
            //_unitOfWork.DonGiaMTCRepository.Add(model);


            if (entity.IdMTC == request.IdMTC && entity.VanBan == request.VanBan)
            {
                entity.DonGiaCu = entity.DonGia;
                entity.DonGia = request.DonGia;
                entity.DinhMucCu = entity.DinhMuc;
                entity.DinhMuc = request.DinhMuc;
            }
            else
            {
                var checkEntity = await _unitOfWork.DonGiaMTCRepository.FindOneAsync(x => x.IdMTC == request.IdMTC && x.VanBan == request.VanBan);
                if (checkEntity != null)
                {
                    throw new EvnException(string.Format(Resources.MSG_IS_EXIST, "Đơn giá máy thi công"));
                }
                entity.IdMTC = request.IdMTC;
                entity.VanBan = request.VanBan;
                entity.DonGiaCu = entity.DonGia;
                entity.DonGia = request.DonGia;
                entity.DinhMucCu = entity.DinhMuc;
                entity.DinhMuc = request.DinhMuc;
            }
            _unitOfWork.DonGiaMTCRepository.Update(entity);

            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}
