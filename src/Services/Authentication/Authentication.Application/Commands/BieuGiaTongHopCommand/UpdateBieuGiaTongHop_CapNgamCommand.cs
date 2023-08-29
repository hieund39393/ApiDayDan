using Authentication.Infrastructure.AggregatesModel.BieuGiaTongHopAggregate;
using Authentication.Infrastructure.Properties;
using Authentication.Infrastructure.Repositories;
using EVN.Core.Exceptions;
using EVN.Core.Extensions;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Authentication.Application.Commands.BieuGiaTongHopCommand
{
    public class UpdateBieuGiaTongHop_CapNgamCommand : IRequest<bool>
    {
        public int Nam { get; set; }
        public int Quy { get; set; }
        public int TinhTrang { get; set; }
        public string GhiChu { get; set; }
        public IFormFile File { get; set; }
    }
    public class UpdateBieuGiaTongHop_CapNgamCommandHandler : IRequestHandler<UpdateBieuGiaTongHop_CapNgamCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public UpdateBieuGiaTongHop_CapNgamCommandHandler(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }
        public async Task<bool> Handle(UpdateBieuGiaTongHop_CapNgamCommand request, CancellationToken cancellationToken)
        {
            var userId = Guid.Parse(TokenExtensions.GetUserId());

            try
            {
                var position = int.Parse(TokenExtensions.GetPosition());

                if (position != request.TinhTrang)

                {
                    request.TinhTrang = request.TinhTrang + 1;
                }
            }
            catch
            {
                throw new EvnException("Người dùng có chức vụ không đúng");
            }

            var data = await _unitOfWork.BieuGiaTongHop_CapNgamRepository.GetQuery(x => x.Nam == request.Nam && x.Quy == request.Quy).ToListAsync();
            if (!data.Any())
            {
                throw new EvnException("Không có dữ liệu");
            }

            foreach (var item in data)
            {
                item.TinhTrang = request.TinhTrang;

                if (item.TinhTrang >= 1)
                {
                    item.NguoiXacNhan = userId;
                    item.NgayXacNhan = DateTime.Now;
                }
                _unitOfWork.BieuGiaTongHop_CapNgamRepository.Update(item);
            }

            var vanBanCu = await _unitOfWork.BieuGiaTongHopChiTiet_CapNgamRepository.FindOneAsync(x => x.Quy == request.Quy && x.Nam == request.Nam && x.TrangThai == request.TinhTrang);
            if (vanBanCu != null)
            {
                vanBanCu.IsDeleted = true;
                _unitOfWork.BieuGiaTongHopChiTiet_CapNgamRepository.Update(vanBanCu);
            }

            var chiTiet = new BieuGiaTongHopChiTiet_CapNgam();
            chiTiet.Nam = request.Nam;
            chiTiet.Quy = request.Quy;
            chiTiet.GhiChu = request.GhiChu;
            chiTiet.TrangThai = request.TinhTrang;

            if (request.File != null && request.File.Length > 0)
            {
                string uploadDirectory = Path.Combine(_webHostEnvironment.WebRootPath + "/VanBan");
                if (!Directory.Exists(uploadDirectory))
                {
                    Directory.CreateDirectory(uploadDirectory);
                }
                string uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(request.File.FileName);
                string filePath = Path.Combine(uploadDirectory, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    request.File.CopyTo(fileStream);
                }
                chiTiet.VanBan = $"/VanBan/{uniqueFileName}";
            }
            _unitOfWork.BieuGiaTongHopChiTiet_CapNgamRepository.Add(chiTiet);

            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}
