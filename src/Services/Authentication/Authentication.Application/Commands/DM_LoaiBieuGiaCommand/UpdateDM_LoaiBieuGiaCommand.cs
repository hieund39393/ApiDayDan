using Authentication.Infrastructure.Properties;
using Authentication.Infrastructure.Repositories;
using EVN.Core.Exceptions;
using MediatR;

namespace Authentication.Application.Commands.DM_LoaiBieuGiaCommand
{
    public class UpdateDM_LoaiBieuGiaCommand : IRequest<bool> // kế thừa IRequest<bool>
    {
        public Guid Id { get; set; } // thêm ID
        public string TenLoaiBieuGia { get; set; }
        public string MaLoaiBieuGia { get; set; }
        public Guid? KhuVucID { get; set; }
    }

    //Tạo thêm 1 class Handler kế thừa IRequestHandler<UpdateDM_LoaiBieuGiaCommand, bool> rồi implement
    public class UpdateDM_LoaiBieuGiaCommandHandler : IRequestHandler<UpdateDM_LoaiBieuGiaCommand, bool> //
    {
        private readonly IUnitOfWork _unitOfWork; // khai báo 
        public UpdateDM_LoaiBieuGiaCommandHandler(IUnitOfWork unitOfWork) //cấu hình dependence
        {
            _unitOfWork = unitOfWork; // khai báo
        }
        public async Task<bool> Handle(UpdateDM_LoaiBieuGiaCommand request, CancellationToken cancellationToken)
        {
            // tìm kiếm xem có ID trong bảng DM_LoaiBieuGia không
            var entity = await _unitOfWork.DM_LoaiBieuGiaRepository.FindOneAsync(x => x.Id == request.Id);
            // nếu không có dữ liệu
            if (entity == null)
            {
                throw new EvnException(string.Format(Resources.MSG_NOT_FOUND, "Loại biểu giá"));
            }
            if (entity.IdKhuVuc == request.KhuVucID && entity.TenLoaiBieuGia == request.TenLoaiBieuGia)
            {
                return true;
            }
            else
            {
                var check = await _unitOfWork.DM_LoaiBieuGiaRepository.FindOneAsync(x => x.TenLoaiBieuGia == request.TenLoaiBieuGia && x.IdKhuVuc == request.KhuVucID);
                if (check != null) throw new EvnException(string.Format(Resources.MSG_IS_EXIST, "Loại biểu giá"));
                entity.TenLoaiBieuGia = request.TenLoaiBieuGia;
                entity.IdKhuVuc = request.KhuVucID;
            }

            _unitOfWork.DM_LoaiBieuGiaRepository.Update(entity);
            //lưu lại trong DB
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}
