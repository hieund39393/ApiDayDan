using AAuthentication.Infrastructure.AggregatesModel.DM_BieuGia;
using Authentication.Infrastructure.Properties;
using Authentication.Infrastructure.Repositories;
using EVN.Core.Exceptions;
using MediatR;

namespace Authentication.Application.Commands.DM_BieuGiaCommand
{
    public class Create_DMBieuGia_CapNgamCommand : IRequest<bool>
    {
        public Guid idLoaiBieuGia { get; set; }
        public string TenBieuGia { get; set; }
        public string MaBieuGia { get; set; }
    }

    //Tạo thêm 1 class Handler kế thừa IRequestHandler<CreateDM_LoaiBieuGiaCommand, bool> rồi implement
    public class Create_DMBieuGia_CapNgamCommandHandler : IRequestHandler<Create_DMBieuGia_CapNgamCommand, bool> //
    {
        private readonly IUnitOfWork _unitOfWork; // khai báo 
        public Create_DMBieuGia_CapNgamCommandHandler(IUnitOfWork unitOfWork) //cấu hình dependence
        {
            _unitOfWork = unitOfWork; // khai báo
        }
        public async Task<bool> Handle(Create_DMBieuGia_CapNgamCommand request, CancellationToken cancellationToken)
        {
            // tìm kiếm xem có mã biểu giá trong db không
            var entity = await _unitOfWork.DM_BieuGia_CapNgamRepository.FindOneAsync(x => x.TenBieuGia == request.TenBieuGia && x.idLoaiBieuGia == request.idLoaiBieuGia);
            // nếu không có dữ liệu thì thêm mới
            if (entity == null)
            {
                // Tạo model DM_LoaiBieuGia
                var model = new DM_BieuGia_CapNgam
                {
                    idLoaiBieuGia = request.idLoaiBieuGia,
                    MaBieuGia = request.MaBieuGia,
                    TenBieuGia = request.TenBieuGia,
                };
                //thêm vào DB
                _unitOfWork.DM_BieuGia_CapNgamRepository.Add(model);
                //lưu lại trong DB
                await _unitOfWork.SaveChangesAsync();
                return true;
            }
            // nếu đã tồn tạo 1 bản ghi
            throw new EvnException(string.Format(Resources.MSG_IS_EXIST, "Biểu giá"));
        }
    }
}
