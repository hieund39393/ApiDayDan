using Authentication.Application.Commands.DM_LoaiBieuGiaCommand;
using Authentication.Infrastructure.Properties;
using Authentication.Infrastructure.Repositories;
using EVN.Core.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authentication.Application.Commands.DM_BieuGia_CapNgamCommand
{
    public class Update_DM_BieuGia_CapNgam_CapNgamCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
        public string TenBieuGia { get; set; }
        public string MaBieuGia { get; set; }
        public Guid idLoaiBieuGia { get; set; }
    }

    public class Update_DM_BieuGia_CapNgam_CapNgamCommandHandler : IRequestHandler<Update_DM_BieuGia_CapNgam_CapNgamCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork; // khai báo 
        public Update_DM_BieuGia_CapNgam_CapNgamCommandHandler(IUnitOfWork unitOfWork) //cấu hình dependence
        {
            _unitOfWork = unitOfWork; // khai báo
        }
        public async Task<bool> Handle(Update_DM_BieuGia_CapNgam_CapNgamCommand request, CancellationToken cancellationToken)
        {
            // tìm kiếm xem có ID trong bảng DM_BieuGia_CapNgam không
            var entity = await _unitOfWork.DM_BieuGia_CapNgamRepository.FindOneAsync(x => x.Id == request.Id);
            // nếu không có dữ liệu
            if (entity == null)
            {
                throw new EvnException(string.Format(Resources.MSG_NOT_FOUND, "biểu giá"));
            }
            if (entity.idLoaiBieuGia == request.idLoaiBieuGia && entity.TenBieuGia == request.TenBieuGia)
            {
                entity.MaBieuGia = request.MaBieuGia;
            }
            else
            {
                var check = await _unitOfWork.DM_BieuGia_CapNgamRepository.FindOneAsync(x => x.TenBieuGia == request.TenBieuGia && x.idLoaiBieuGia == request.idLoaiBieuGia);
                if (check != null) throw new EvnException(string.Format(Resources.MSG_IS_EXIST, "Biểu giá"));
                entity.MaBieuGia = request.MaBieuGia;
                entity.TenBieuGia = request.TenBieuGia;
                entity.idLoaiBieuGia = request.idLoaiBieuGia;
            }

            //thêm vào DB
            _unitOfWork.DM_BieuGia_CapNgamRepository.Update(entity);
            //lưu lại trong DB
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}
