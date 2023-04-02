using Authentication.Infrastructure.AggregatesModel.DM_Vung;
using Authentication.Infrastructure.Properties;
using Authentication.Infrastructure.Repositories;
using EVN.Core.Exceptions;
using MediatR;

namespace Authentication.Application.Commands.DM_VungCommand
{
    public class CreateDM_VungCommand : IRequest<bool> // kế thừa IRequest<bool>
    {
        public string TenVung { get; set; } 
        public string GhiChu { get; set; } 
    }

    //Tạo thêm 1 class Handler kế thừa IRequestHandler<CreateDM_VungCommand, bool> rồi implement
    public class CreateDM_VungCommandHandler : IRequestHandler<CreateDM_VungCommand, bool> //
    {
        private readonly IUnitOfWork _unitOfWork; // khai báo 
        public CreateDM_VungCommandHandler(IUnitOfWork unitOfWork) //cấu hình dependence
        {
            _unitOfWork = unitOfWork; // khai báo
        }
        public async Task<bool> Handle(CreateDM_VungCommand request, CancellationToken cancellationToken) 
        {
            // tìm kiếm xem có mã biểu giá trong db không
            var entity = await _unitOfWork.DM_VungRepository.FindOneAsync(x => x.TenVung == request.TenVung);
            // nếu không có dữ liệu thì thêm mới
            if (entity == null)
            {
                // Tạo model DM_Vung
                var model = new DM_Vung
                {
                    TenVung = request.TenVung,
                    GhiChu = request.GhiChu,
                };
                //thêm vào DB
                _unitOfWork.DM_VungRepository.Add(model);
                //lưu lại trong DB
                await _unitOfWork.SaveChangesAsync(); 
                return true;
            }
            // nếu đã tồn tạo 1 bản ghi
            throw new EvnException(string.Format(Resources.MSG_IS_EXIST, "vùng"));
        }
    }
}
