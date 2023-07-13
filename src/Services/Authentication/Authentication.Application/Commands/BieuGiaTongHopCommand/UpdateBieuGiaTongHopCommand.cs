using Authentication.Infrastructure.Properties;
using Authentication.Infrastructure.Repositories;
using EVN.Core.Exceptions;
using EVN.Core.Extensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Authentication.Application.Commands.BieuGiaTongHopCommand
{
    public class UpdateBieuGiaTongHopCommand : IRequest<bool>
    {
        public int Nam { get; set; }
        public int Quy { get; set; }
        public int TinhTrang { get; set; }
    }
    public class UpdateBieuGiaTongHopCommandHandler : IRequestHandler<UpdateBieuGiaTongHopCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        public UpdateBieuGiaTongHopCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<bool> Handle(UpdateBieuGiaTongHopCommand request, CancellationToken cancellationToken)
        {
            var userId = Guid.Parse(TokenExtensions.GetUserId());
            var data = await _unitOfWork.BieuGiaTongHopRepository.GetQuery(x => x.Nam == request.Nam && x.Quy == request.Quy && x.TinhTrang == request.TinhTrang).ToListAsync();
            if (!data.Any())
            {
                throw new EvnException("Không có dữ liệu");
            }

            foreach (var item in data)
            {
                item.TinhTrang = request.TinhTrang + 1;

                if (item.TinhTrang >= 2)
                {
                    item.NguoiXacNhan = userId;
                    item.NgayXacNhan = DateTime.Now;
                }
                _unitOfWork.BieuGiaTongHopRepository.Update(item);
            }

            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}
