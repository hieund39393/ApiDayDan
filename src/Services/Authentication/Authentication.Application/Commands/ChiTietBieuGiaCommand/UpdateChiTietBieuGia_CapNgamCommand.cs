using Authentication.Application.Model.ChiTietBieuGia;
using Authentication.Infrastructure.AggregatesModel.BieuGiaTongHopAggregate;
using Authentication.Infrastructure.AggregatesModel.ChiTietBieuGiaAggregate;
using Authentication.Infrastructure.Properties;
using Authentication.Infrastructure.Repositories;
using AutoMapper;
using EVN.Core.Exceptions;
using MediatR;
using static EVN.Core.Common.AppEnum;

namespace Authentication.Application.Commands.ChiTietBieuGiaCommand
{
    public class UpdateChiTietBieuGia_CapNgamCommand : IRequest<bool>
    {
        public List<UpdateChiTietBieuGiaRequest> ChiSos { get; set; }
    }
    public class UpdateChiTietBieuGia_CapNgamCommandHandler : IRequestHandler<UpdateChiTietBieuGia_CapNgamCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public UpdateChiTietBieuGia_CapNgamCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<bool> Handle(UpdateChiTietBieuGia_CapNgamCommand request, CancellationToken cancellationToken)
        {
            var chiTietBG = request.ChiSos.FirstOrDefault();
            if (chiTietBG == null) { return true; }

            var bieuGiaTongHop = await _unitOfWork.BieuGiaTongHop_CapNgamRepository
                .FindOneAsync(x => x.IdBieuGia == chiTietBG.IdBieuGia && x.Quy == chiTietBG.Quy && x.Nam == chiTietBG.Nam);
            if (bieuGiaTongHop == null)
            {
                _unitOfWork.BieuGiaTongHop_CapNgamRepository.Add(new BieuGiaTongHop_CapNgam { IdBieuGia = chiTietBG.IdBieuGia, Quy = chiTietBG.Quy, Nam = chiTietBG.Nam });
            }
            else if (bieuGiaTongHop.TinhTrang >= TinhTrangEnum.DaDuyet.GetHashCode())
            {
                throw new EvnException($"Biểu giá đã được duyệt, không thể cập nhật");
            }

            var createList = new List<ChiTietBieuGia_CapNgam>();

            foreach (var item in request.ChiSos)
            {
                if (item.Id == null)
                {
                    var data = new ChiTietBieuGia_CapNgam()
                    {
                        IDBieuGia = item.IdBieuGia,
                        IDCongViec = item.IdCongViec,
                        SoLuong = item.SoLuong,
                        HeSoDieuChinh_K1nc = item.HeSoDieuChinh_K1nc,
                        HeSoDieuChinh_K2nc = item.HeSoDieuChinh_K2nc,
                        HeSoDieuChinh_Kmtc = item.HeSoDieuChinh_Kmtc,
                        DonGia_VL = item.DonGia_VL,
                        DonGia_NC = item.DonGia_NC,
                        DonGia_MTC = item.DonGia_MTC,
                        Nam = item.Nam,
                        Quy = item.Quy,
                    };
                    createList.Add(data);
                }
                else
                {
                    var data = await _unitOfWork.ChiTietBieuGia_CapNgamRepository.FindOneAsync(x => x.Id == item.Id);
                    if (data != null)
                    {
                        data.SoLuong = item.SoLuong;
                        data.HeSoDieuChinh_K1nc = item.HeSoDieuChinh_K1nc;
                        data.HeSoDieuChinh_K2nc = item.HeSoDieuChinh_K2nc;
                        data.HeSoDieuChinh_Kmtc = item.HeSoDieuChinh_Kmtc;
                        data.DonGia_VL = item.DonGia_VL;
                        data.DonGia_NC = item.DonGia_NC;
                        data.DonGia_MTC = item.DonGia_MTC;

                        _unitOfWork.ChiTietBieuGia_CapNgamRepository.Update(data);
                    }
                }
            }
            if (createList.Any())
            {
                _unitOfWork.ChiTietBieuGia_CapNgamRepository.AddRange(createList);
            }
            await _unitOfWork.SaveChangesAsync();
            return true;


        }
    }
}
