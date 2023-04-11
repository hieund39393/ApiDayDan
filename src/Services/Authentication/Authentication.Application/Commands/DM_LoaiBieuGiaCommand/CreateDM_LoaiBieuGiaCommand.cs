﻿using Authentication.Infrastructure.AggregatesModel.DM_LoaiBieuGiaAggregate;
using Authentication.Infrastructure.Properties;
using Authentication.Infrastructure.Repositories;
using EVN.Core.Exceptions;
using MediatR;

namespace Authentication.Application.Commands.DM_LoaiBieuGiaCommand
{
    public class CreateDM_LoaiBieuGiaCommand : IRequest<bool> // kế thừa IRequest<bool>
    {
        public string TenLoaiBieuGia { get; set; } 
        public string MaLoaiBieuGia { get; set; }
        public Guid? VungID { get; set; }
        public Guid? KhuVucID { get; set; }
    }

    //Tạo thêm 1 class Handler kế thừa IRequestHandler<CreateDM_LoaiBieuGiaCommand, bool> rồi implement
    public class CreateDM_LoaiBieuGiaCommandHandler : IRequestHandler<CreateDM_LoaiBieuGiaCommand, bool> //
    {
        private readonly IUnitOfWork _unitOfWork; // khai báo 
        public CreateDM_LoaiBieuGiaCommandHandler(IUnitOfWork unitOfWork) //cấu hình dependence
        {
            _unitOfWork = unitOfWork; // khai báo
        }
        public async Task<bool> Handle(CreateDM_LoaiBieuGiaCommand request, CancellationToken cancellationToken) 
        {
            // tìm kiếm xem có mã biểu giá trong db không
            var entity = await _unitOfWork.DM_LoaiBieuGiaRepository.FindOneAsync(x => x.MaLoaiBieuGia == request.MaLoaiBieuGia);
            // nếu không có dữ liệu thì thêm mới
            if (entity == null)
            {
                // Tạo model DM_LoaiBieuGia
                var model = new DM_LoaiBieuGia
                {
                    MaLoaiBieuGia = request.MaLoaiBieuGia,
                    TenLoaiBieuGia = request.TenLoaiBieuGia,
                    KhuVucID = request.KhuVucID,
                    VungID= request.VungID,
                };
                //thêm vào DB
                _unitOfWork.DM_LoaiBieuGiaRepository.Add(model);
                //lưu lại trong DB
                await _unitOfWork.SaveChangesAsync(); 
                return true;
            }
            // nếu đã tồn tạo 1 bản ghi
            throw new EvnException(string.Format(Resources.MSG_IS_EXIST, "Loại biểu giá"));
        }
    }
}
