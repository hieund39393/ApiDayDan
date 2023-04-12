using Authentication.Infrastructure.AggregatesModel.DM_LoaiCapAggregate;
using Authentication.Infrastructure.Properties;
using Authentication.Infrastructure.Repositories;
using EVN.Core.Exceptions;
using MediatR;

namespace Authentication.Application.Commands.DM_LoaiCapCommand
{
    public class CreateDM_LoaiCapCommand : IRequest<bool> // kế thừa IRequest<bool>
    {
        public string TenLoaiCap { get; set; }
        public string MaLoaiCap { get; set; }
        public string DonViTinh { get; set; }
    }

    //Tạo thêm 1 class Handler kế thừa IRequestHandler<CreateDM_LoaiCapCommand, bool> rồi implement
    public class CreateDM_LoaiCapCommandHandler : IRequestHandler<CreateDM_LoaiCapCommand, bool> //
    {
        private readonly IUnitOfWork _unitOfWork; // khai báo 
        public CreateDM_LoaiCapCommandHandler(IUnitOfWork unitOfWork) //cấu hình dependence
        {
            _unitOfWork = unitOfWork; // khai báo
        }
        public async Task<bool> Handle(CreateDM_LoaiCapCommand request, CancellationToken cancellationToken) 
        {
            // tìm kiếm xem có mã loại cáp trong db không
            var entity = await _unitOfWork.DM_LoaiCapRepository.FindOneAsync(x => x.MaLoaiCap == request.MaLoaiCap);
            // nếu không có dữ liệu thì thêm mới
            if (entity == null)
            {
                // Tạo model DM_LoaiCap
                var model = new DM_LoaiCap
                {
                    TenLoaiCap = request.TenLoaiCap ,
                    MaLoaiCap = request.MaLoaiCap ,
                    DonViTinh = request.DonViTinh,
                };
                //thêm vào DB
                _unitOfWork.DM_LoaiCapRepository.Add(model);
                //lưu lại trong DB
                await _unitOfWork.SaveChangesAsync(); 
                return true;
            }
            // nếu đã tồn tạo 1 bản ghi
            throw new EvnException(string.Format(Resources.MSG_IS_EXIST, "Loại cáp"));
        }
    }
}
