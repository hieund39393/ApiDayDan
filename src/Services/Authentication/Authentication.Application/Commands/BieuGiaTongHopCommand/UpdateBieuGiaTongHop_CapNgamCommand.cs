using Authentication.Infrastructure.Properties;
using Authentication.Infrastructure.Repositories;
using EVN.Core.Exceptions;
using EVN.Core.Extensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Authentication.Application.Commands.BieuGiaTongHopCommand
{
    public class UpdateBieuGiaTongHop_CapNgamCommand : IRequest<bool>
    {
        public int Nam { get; set; }
        public int Quy { get; set; }
        public int TinhTrang { get; set; }
    }
    public class UpdateBieuGiaTongHop_CapNgamCommandHandler : IRequestHandler<UpdateBieuGiaTongHop_CapNgamCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        public UpdateBieuGiaTongHop_CapNgamCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<bool> Handle(UpdateBieuGiaTongHop_CapNgamCommand request, CancellationToken cancellationToken)
        {
            var userId = Guid.Parse(TokenExtensions.GetUserId());
            var data = await _unitOfWork.BieuGiaTongHop_CapNgamRepository.GetQuery(x => x.Nam == request.Nam && x.Quy == request.Quy && x.TinhTrang == request.TinhTrang).ToListAsync();
            if (!data.Any())
            {
                throw new EvnException("Không có dữ liệu");
            }

            foreach (var item in data)
            {
                item.TinhTrang = request.TinhTrang + 1;

                if (item.TinhTrang == 2)
                {
                    item.NguoiXacNhan = userId;
                    item.NgayXacNhan = DateTime.Now;
                }
                _unitOfWork.BieuGiaTongHop_CapNgamRepository.Update(item);
            }

            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}
