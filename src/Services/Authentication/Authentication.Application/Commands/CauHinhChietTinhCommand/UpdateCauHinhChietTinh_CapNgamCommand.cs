﻿using Authentication.Infrastructure.AggregatesModel.DonGiaChietTinhAggregate;
using Authentication.Infrastructure.Properties;
using Authentication.Infrastructure.Repositories;
using EVN.Core.Exceptions;
using EVN.Core.Helpers;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static EVN.Core.Common.AppEnum;

namespace Authentication.Application.Commands.CauHinhChietTinhCommand
{
    public class UpdateCauHinhChietTinh_CapNgamCommand : IRequest<bool>
    {
        public Guid IdCongViec { get; set; }
        public List<Guid> IdVatLieu { get; set; }
        public List<Guid> IdNhanCong { get; set; }
        public List<Guid> IdMTC { get; set; }
        public int VungKhuVuc { get; set; }
    }

    //Tạo thêm 1 class Handler kế thừa IRequestHandler<UpdateCauHinhChietTinh_CapNgamCommand, bool> rồi implement
    public class UpdateCauHinhChietTinh_CapNgamCommandHandler : IRequestHandler<UpdateCauHinhChietTinh_CapNgamCommand, bool> //
    {
        private readonly IUnitOfWork _unitOfWork; // khai báo 
        public UpdateCauHinhChietTinh_CapNgamCommandHandler(IUnitOfWork unitOfWork) //cấu hình dependence
        {
            _unitOfWork = unitOfWork; // khai báo
        }
        public async Task<bool> Handle(UpdateCauHinhChietTinh_CapNgamCommand request, CancellationToken cancellationToken)
        {


            // tìm kiếm xem có ID trong bảng CauHinhChietTinh không
            var listData = await _unitOfWork.CauHinhChietTinh_CapNgamRepository.GetQuery(x => x.IdCongViec == request.IdCongViec && x.VungKhuVuc == request.VungKhuVuc).ToListAsync();
            foreach (var item in listData)
            {
                item.IsDeleted = true;
                _unitOfWork.CauHinhChietTinh_CapNgamRepository.Update(item);
            }

            var listCauHinh = new List<CauHinhChietTinh_CapNgam>();
            var listVatLieu = await _unitOfWork.DM_VatLieu_CapNgamRepository.GetQuery().ToListAsync();

            if (request.IdVatLieu.Any())
            {
                foreach (var item in request.IdVatLieu)
                {
                    var thuTu = listVatLieu.Where(x => x.Id == item).FirstOrDefault();

                    listCauHinh.Add(new CauHinhChietTinh_CapNgam { ThuTuHienThi = thuTu.ThuTuHienThi, IdCongViec = request.IdCongViec, IdChiTiet = item, PhanLoai = PhanLoaiChietTinhEnum.VatLieu.GetHashCode() });
                }
            }
            if (request.IdNhanCong.Any())
            {
                foreach (var item in request.IdNhanCong)
                {
                    listCauHinh.Add(new CauHinhChietTinh_CapNgam { IdCongViec = request.IdCongViec, IdChiTiet = item, PhanLoai = PhanLoaiChietTinhEnum.NhanCong.GetHashCode() });
                }
            }
            if (request.IdMTC.Any())
            {
                foreach (var item in request.IdMTC)
                {
                    listCauHinh.Add(new CauHinhChietTinh_CapNgam { IdCongViec = request.IdCongViec, IdChiTiet = item, PhanLoai = PhanLoaiChietTinhEnum.MTC.GetHashCode()});
                }
            }
            if (listCauHinh.Any())
            {
                _unitOfWork.CauHinhChietTinh_CapNgamRepository.AddRange(listCauHinh);
            }

            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}
