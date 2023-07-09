using Authentication.Infrastructure.AggregatesModel.DM_LoaiBieuGiaAggregate;
using Authentication.Infrastructure.Properties;
using Authentication.Infrastructure.Repositories;
using EVN.Core.Exceptions;
using MediatR;

namespace Authentication.Application.Commands.DM_LoaiBieuGia_CapNgamCommand
{
    public class CreateDM_LoaiBieuGia_CapNgamCommand : IRequest<bool> // kế thừa IRequest<bool>
    {
        public string TenLoaiBieuGia { get; set; }
        public string MaLoaiBieuGia { get; set; }
        public Guid? KhuVucID { get; set; }
    }

    //Tạo thêm 1 class Handler kế thừa IRequestHandler<CreateDM_LoaiBieuGia_CapNgamCommand, bool> rồi implement
    public class CreateDM_LoaiBieuGia_CapNgamCommandHandler : IRequestHandler<CreateDM_LoaiBieuGia_CapNgamCommand, bool> //
    {
        private readonly IUnitOfWork _unitOfWork; // khai báo 
        public CreateDM_LoaiBieuGia_CapNgamCommandHandler(IUnitOfWork unitOfWork) //cấu hình dependence
        {
            _unitOfWork = unitOfWork; // khai báo
        }
        public async Task<bool> Handle(CreateDM_LoaiBieuGia_CapNgamCommand request, CancellationToken cancellationToken)
        {
            // tìm kiếm xem có mã biểu giá trong db không
            var entity = await _unitOfWork.DM_LoaiBieuGia_CapNgamRepository.FindOneAsync(x => x.TenLoaiBieuGia == request.TenLoaiBieuGia && x.IdKhuVuc == request.KhuVucID);
            // nếu không có dữ liệu thì thêm mới
            if (entity == null)
            {
                // Tạo model DM_LoaiBieuGia_CapNgam
                var model = new DM_LoaiBieuGia_CapNgam
                {
                    TenLoaiBieuGia = request.TenLoaiBieuGia,
                    IdKhuVuc = request.KhuVucID,
                    MaLoaiBieuGia = request.MaLoaiBieuGia
                };
                //thêm vào DB
                _unitOfWork.DM_LoaiBieuGia_CapNgamRepository.Add(model);
                //lưu lại trong DB
                await _unitOfWork.SaveChangesAsync();
                return true;
            }
            // nếu đã tồn tạo 1 bản ghi
            throw new EvnException(string.Format(Resources.MSG_IS_EXIST, "Loại biểu giá"));
        }
    }
}
