using Authentication.Infrastructure.Properties;
using Authentication.Infrastructure.Repositories;
using EVN.Core.Exceptions;
using MediatR;

namespace Authentication.Application.Commands.DM_LoaiBieuGia_CapNgamCommand
{
    public class UpdateDM_LoaiBieuGia_CapNgamCommand : IRequest<bool> // kế thừa IRequest<bool>
    {
        public Guid Id { get; set; } // thêm ID
        public string TenLoaiBieuGia { get; set; }
        public string MaLoaiBieuGia { get; set; }
        public Guid? KhuVucID { get; set; }
    }

    //Tạo thêm 1 class Handler kế thừa IRequestHandler<UpdateDM_LoaiBieuGia_CapNgamCommand, bool> rồi implement
    public class UpdateDM_LoaiBieuGia_CapNgamCommandHandler : IRequestHandler<UpdateDM_LoaiBieuGia_CapNgamCommand, bool> //
    {
        private readonly IUnitOfWork _unitOfWork; // khai báo 
        public UpdateDM_LoaiBieuGia_CapNgamCommandHandler(IUnitOfWork unitOfWork) //cấu hình dependence
        {
            _unitOfWork = unitOfWork; // khai báo
        }
        public async Task<bool> Handle(UpdateDM_LoaiBieuGia_CapNgamCommand request, CancellationToken cancellationToken)
        {
            // tìm kiếm xem có ID trong bảng DM_LoaiBieuGia_CapNgam không
            var entity = await _unitOfWork.DM_LoaiBieuGia_CapNgamRepository.FindOneAsync(x => x.Id == request.Id);
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
                var check = await _unitOfWork.DM_LoaiBieuGia_CapNgamRepository.FindOneAsync(x => x.TenLoaiBieuGia == request.TenLoaiBieuGia && x.IdKhuVuc == request.KhuVucID);
                if (check != null) throw new EvnException(string.Format(Resources.MSG_IS_EXIST, "Loại biểu giá cáp ngầm"));
                entity.TenLoaiBieuGia = request.TenLoaiBieuGia;
                entity.IdKhuVuc = request.KhuVucID;
            }

            _unitOfWork.DM_LoaiBieuGia_CapNgamRepository.Update(entity);
            //lưu lại trong DB
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}
