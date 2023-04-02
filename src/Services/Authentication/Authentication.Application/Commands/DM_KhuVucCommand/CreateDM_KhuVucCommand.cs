using Authentication.Infrastructure.AggregatesModel.DM_KhuVuc;
using Authentication.Infrastructure.Properties;
using Authentication.Infrastructure.Repositories;
using EVN.Core.Exceptions;
using MediatR;

namespace Authentication.Application.Commands.DM_KhuVucCommand
{
    public class CreateDM_KhuVucCommand : IRequest<bool> // kế thừa IRequest<bool>
    {
        public string TenKhuVuc { get; set; } 
        public string GhiChu { get; set; } 
    }

    //Tạo thêm 1 class Handler kế thừa IRequestHandler<CreateDM_KhuVucCommand, bool> rồi implement
    public class CreateDM_KhuVucCommandHandler : IRequestHandler<CreateDM_KhuVucCommand, bool> //
    {
        private readonly IUnitOfWork _unitOfWork; // khai báo 
        public CreateDM_KhuVucCommandHandler(IUnitOfWork unitOfWork) //cấu hình dependence
        {
            _unitOfWork = unitOfWork; // khai báo
        }
        public async Task<bool> Handle(CreateDM_KhuVucCommand request, CancellationToken cancellationToken) 
        {
            // tìm kiếm xem có mã biểu giá trong db không
            var entity = await _unitOfWork.DM_KhuVucRepository.FindOneAsync(x => x.TenKhuVuc == request.TenKhuVuc);
            // nếu không có dữ liệu thì thêm mới
            if (entity == null)
            {
                // Tạo model DM_KhuVuc
                var model = new DM_KhuVuc
                {
                    TenKhuVuc = request.TenKhuVuc,
                    GhiChu = request.GhiChu,
                };
                //thêm vào DB
                _unitOfWork.DM_KhuVucRepository.Add(model);
                //lưu lại trong DB
                await _unitOfWork.SaveChangesAsync(); 
                return true;
            }
            // nếu đã tồn tạo 1 bản ghi
            throw new EvnException(string.Format(Resources.MSG_IS_EXIST, "khu vực"));
        }
    }
}
