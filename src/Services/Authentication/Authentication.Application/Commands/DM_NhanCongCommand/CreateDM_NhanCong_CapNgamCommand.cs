using Authentication.Infrastructure.AggregatesModel.DM_NhanCongAggregate;
using Authentication.Infrastructure.Properties;
using Authentication.Infrastructure.Repositories;
using EVN.Core.Exceptions;
using MediatR;

namespace Authentication.Application.Commands.DM_NhanCong_CapNgamCommand
{
    public class CreateDM_NhanCong_CapNgamCommand : IRequest<bool> // kế thừa IRequest<bool>
    {
        public string CapBac { get; set; }
        public string HeSo { get; set; }
        public Guid? IdKhuVuc { get; set; }
    }

    //Tạo thêm 1 class Handler kế thừa IRequestHandler<CreateDM_NhanCong_CapNgamCommand, bool> rồi implement
    public class CreateDM_NhanCong_CapNgamCommandHandler : IRequestHandler<CreateDM_NhanCong_CapNgamCommand, bool> //
    {
        private readonly IUnitOfWork _unitOfWork; // khai báo 
        public CreateDM_NhanCong_CapNgamCommandHandler(IUnitOfWork unitOfWork) //cấu hình dependence
        {
            _unitOfWork = unitOfWork; // khai báo
        }
        public async Task<bool> Handle(CreateDM_NhanCong_CapNgamCommand request, CancellationToken cancellationToken)
        {
            var entity = await _unitOfWork.DM_NhanCong_CapNgamRepository.FindOneAsync(x => x.CapBac == request.CapBac && x.IdKhuVuc == request.IdKhuVuc);
            // nếu không có dữ liệu thì thêm mới
            if (entity == null)
            {
                // Tạo model DM_NhanCong
                var model = new DM_NhanCong_CapNgam
                {
                    CapBac = request.CapBac,
                    HeSo = request.HeSo,
                    IdKhuVuc = request.IdKhuVuc,
                };
                //thêm vào DB
                _unitOfWork.DM_NhanCong_CapNgamRepository.Add(model);
                //lưu lại trong DB
                await _unitOfWork.SaveChangesAsync();
                return true;
            }
            // nếu đã tồn tạo 1 bản ghi
            throw new EvnException(string.Format(Resources.MSG_IS_EXIST, "Nhân công cáp ngầm"));
        }
    }
}
