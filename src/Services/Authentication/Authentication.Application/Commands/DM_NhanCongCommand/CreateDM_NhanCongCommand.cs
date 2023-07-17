using Authentication.Infrastructure.AggregatesModel.DM_NhanCongAggregate;
using Authentication.Infrastructure.Properties;
using Authentication.Infrastructure.Repositories;
using EVN.Core.Exceptions;
using MediatR;

namespace Authentication.Application.Commands.DM_NhanCongCommand
{
    public class CreateDM_NhanCongCommand : IRequest<bool> // kế thừa IRequest<bool>
    {
        public string CapBac { get; set; }
        public string HeSo { get; set; }
        public Guid? IdKhuVuc { get; set; }
    }

    //Tạo thêm 1 class Handler kế thừa IRequestHandler<CreateDM_NhanCongCommand, bool> rồi implement
    public class CreateDM_NhanCongCommandHandler : IRequestHandler<CreateDM_NhanCongCommand, bool> //
    {
        private readonly IUnitOfWork _unitOfWork; // khai báo 
        public CreateDM_NhanCongCommandHandler(IUnitOfWork unitOfWork) //cấu hình dependence
        {
            _unitOfWork = unitOfWork; // khai báo
        }
        public async Task<bool> Handle(CreateDM_NhanCongCommand request, CancellationToken cancellationToken)
        {
            // tìm kiếm xem có mã loại cáp trong db không
            var entity = await _unitOfWork.DM_NhanCongRepository.FindOneAsync(x => x.CapBac == request.CapBac && x.IdKhuVuc == request.IdKhuVuc);
            // nếu không có dữ liệu thì thêm mới
            if (entity == null)
            {
                // Tạo model DM_NhanCong
                var model = new DM_NhanCong
                {
                    CapBac = request.CapBac,
                    HeSo = request.HeSo,
                    IdKhuVuc = request.IdKhuVuc,
                };
                //thêm vào DB
                _unitOfWork.DM_NhanCongRepository.Add(model);
                //lưu lại trong DB
                await _unitOfWork.SaveChangesAsync();
                return true;
            }
            // nếu đã tồn tạo 1 bản ghi
            throw new EvnException(string.Format(Resources.MSG_IS_EXIST, "Nhân công"));
        }
    }
}
