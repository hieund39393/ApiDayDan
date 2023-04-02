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

namespace Authentication.Application.Commands.DM_BieuGiaCommand
{
    public class Update_DMBieuGiaCommand : IRequest<bool>
    {
        public Guid Id { get; set; } 
        public string TenBieuGia { get; set; }
        public string MaBieuGia { get; set; }
    }

    public class Update_DMBieuGiaCommandHandler : IRequestHandler<Update_DMBieuGiaCommand, bool> 
    {
        private readonly IUnitOfWork _unitOfWork; // khai báo 
        public Update_DMBieuGiaCommandHandler(IUnitOfWork unitOfWork) //cấu hình dependence
        {
            _unitOfWork = unitOfWork; // khai báo
        }
        public async Task<bool> Handle(Update_DMBieuGiaCommand request, CancellationToken cancellationToken)
        {
            // tìm kiếm xem có ID trong bảng DM_BieuGia không
            var entity = await _unitOfWork.DM_BieuGiaRepository.FindOneAsync(x => x.Id == request.Id);
            // nếu không có dữ liệu
            if (entity == null)
            {
                throw new EvnException(string.Format(Resources.MSG_NOT_FOUND, "biểu giá"));
            }

            entity.MaBieuGia = request.MaBieuGia;
            entity.TenBieuGia = request.TenBieuGia;
            //thêm vào DB
            _unitOfWork.DM_BieuGiaRepository.Update(entity);
            //lưu lại trong DB
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}
