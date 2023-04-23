using AAuthentication.Infrastructure.AggregatesModel.DM_BieuGia;
using Authentication.Infrastructure.Properties;
using Authentication.Infrastructure.Repositories;
using EVN.Core.Exceptions;
using MediatR;

namespace Authentication.Application.Commands.DM_BieuGiaCommand
{
    public class Create_DMBieuGiaCommand : IRequest<bool>
    {
        public Guid idLoaiBieuGia { get; set; }
        public string TenBieuGia { get; set; }
        public string MaBieuGia { get; set; }
    }

    //Tạo thêm 1 class Handler kế thừa IRequestHandler<CreateDM_LoaiBieuGiaCommand, bool> rồi implement
    public class Create_DMBieuGiaCommandHandler : IRequestHandler<Create_DMBieuGiaCommand, bool> //
    {
        private readonly IUnitOfWork _unitOfWork; // khai báo 
        public Create_DMBieuGiaCommandHandler(IUnitOfWork unitOfWork) //cấu hình dependence
        {
            _unitOfWork = unitOfWork; // khai báo
        }
        public async Task<bool> Handle(Create_DMBieuGiaCommand request, CancellationToken cancellationToken)
        {
            // tìm kiếm xem có mã biểu giá trong db không
            var entity = await _unitOfWork.DM_BieuGiaRepository.FindOneAsync(x => x.TenBieuGia == request.TenBieuGia && x.idLoaiBieuGia == request.idLoaiBieuGia);
            // nếu không có dữ liệu thì thêm mới
            if (entity == null)
            {
                // Tạo model DM_LoaiBieuGia
                var model = new DM_BieuGia
                {
                    idLoaiBieuGia = request.idLoaiBieuGia,
                    MaBieuGia = request.MaBieuGia,
                    TenBieuGia = request.TenBieuGia,
                };
                //thêm vào DB
                _unitOfWork.DM_BieuGiaRepository.Add(model);
                //lưu lại trong DB
                await _unitOfWork.SaveChangesAsync();
                return true;
            }
            // nếu đã tồn tạo 1 bản ghi
            throw new EvnException(string.Format(Resources.MSG_IS_EXIST, "Biểu giá"));
        }
    }
}
