using Authentication.Application.Model;
using Authentication.Application.Model.ChiTietBieuGia;
using Authentication.Application.Services;
using Authentication.Infrastructure.Repositories;
using EVN.Core.Infrastructure.Factory;
using EVN.Core.Models;
using EVN.Core.SeedWork.ExtendEntities;
using Microsoft.EntityFrameworkCore;
using static EVN.Core.Common.AppEnum;

namespace Authentication.Application.Queries.ChiTietBieuGiaQuery
{

    public interface IChiTietBieuGiaQuery // tạo interface (Quy tắc : có chữ I ở đầu để biết nó là interface)
    {

        //Task<ChiTietBieuGiaResult> GetList(ChiTietBieuGiaRequest request);
        Task<List<SelectItem>> GetBieuGiaByLoaiBieuGia(Guid loaiBieuGia);
        Task<List<SelectItem>> GetDonGiaChietTinh(int IdPhanLoai);
        Task<List<GetDonGiaResponse>> GetDonGia(GetDonGiaRequest request);
    }
    public class ChiTietBieuGiaQuery : BaseExtendEntities, IChiTietBieuGiaQuery // kế thừa interface vừa tạo
    {
        private readonly IUnitOfWork _unitOfWork; // khai báo 
        private readonly IExOneHttpClientFactory _httpClientFactory;
        public ChiTietBieuGiaQuery(IUnitOfWork unitOfWork, IExOneHttpClientFactory httpClientFactory)
        {
            _unitOfWork = unitOfWork;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<List<SelectItem>> GetBieuGiaByLoaiBieuGia(Guid loaiBieuGia)
        {
            return await _unitOfWork.DM_BieuGiaRepository.GetQuery(x => x.idLoaiBieuGia == loaiBieuGia).Select(x => new SelectItem
            {
                Name = x.TenBieuGia,
                Value = x.Id.ToString()
            }).AsNoTracking().ToListAsync();
        }

        public async Task<List<GetDonGiaResponse>> GetDonGia(GetDonGiaRequest request)
        {
            var result = new List<GetDonGiaResponse>();
            switch (request.Nguon)
            {
                case 1:
                    var httpClient = new BaseResponseService<ApiResultData2>(_httpClientFactory);
                    var data = await httpClient.GetResponseData2($"http://10.9.8.157:8087/Get_DonGiaCap_BaoGiaEVNHANOI?nam={request.Nam}&quy={request.Quy}");
                    result = data.Data.Select(x => new GetDonGiaResponse { Ten = x.TenVatTu, Ma = x.MaVatTu, DonGia = x.DonGia }).ToList();
                    break;
                case 2:
                    result = await _unitOfWork.GiaCapRepository.GetQuery()
                        .Select(x => new GetDonGiaResponse
                        {
                            Ten = x.VanBan,
                            DonGia = x.DonGia.ToString(),
                        }).ToListAsync();
                    break;
                case 3:
                    result = await _unitOfWork.DonGiaVatLieuRepository.GetQuery()
                        .Select(x => new GetDonGiaResponse
                        {
                            Ten = x.VanBan,
                            DonGia = x.DonGia.ToString(),
                        }).ToListAsync();
                    break;
                case 4:
                    result = await _unitOfWork.DonGiaChietTinhRepository.GetQuery(x => x.IdPhanLoai == DonGiaChietTinhPhanLoai.VatLieu.GetHashCode()).Include(x => x.DM_VatLieuChietTinh)
                        .Select(x => new GetDonGiaResponse
                        {
                            Ten = x.DM_VatLieuChietTinh.TenVatLieuChietTinh,
                            Ma = x.DM_VatLieuChietTinh.MaVatLieuChietTinh,
                            DonGia = x.DonGia.ToString(),
                        }).ToListAsync();
                    break;
                case 5:
                    result = await _unitOfWork.DonGiaNhanCongRepository.GetQuery()
                       .Select(x => new GetDonGiaResponse
                       {
                           Ten = $"{x.KhuVuc.TenKhuVuc} - Hệ số: {x.HeSo} - Cấp bậc: {x.CapBac}",
                           DonGia = x.DonGia.ToString(),
                       }).ToListAsync();
                    break;
                case 6:
                    result = await _unitOfWork.DonGiaChietTinhRepository.GetQuery(x => x.IdPhanLoai == DonGiaChietTinhPhanLoai.NhanCong.GetHashCode()).Include(x => x.DM_VatLieuChietTinh)
                      .Select(x => new GetDonGiaResponse
                      {
                          Ten = x.DM_VatLieuChietTinh.TenVatLieuChietTinh,
                          Ma = x.DM_VatLieuChietTinh.MaVatLieuChietTinh,
                          DonGia = x.DonGia.ToString(),
                      }).ToListAsync();
                    break;
                case 7:
                    // code block
                    break;
                case 8:
                    result = await _unitOfWork.DonGiaChietTinhRepository.GetQuery(x => x.IdPhanLoai == DonGiaChietTinhPhanLoai.MayThiCong.GetHashCode()).Include(x => x.DM_VatLieuChietTinh)
                    .Select(x => new GetDonGiaResponse
                    {
                        Ten = x.DM_VatLieuChietTinh.TenVatLieuChietTinh,
                        Ma = x.DM_VatLieuChietTinh.MaVatLieuChietTinh,
                        DonGia = x.DonGia.ToString(),
                    }).ToListAsync();
                    break;
                default:
                    break;
            }

            if (!string.IsNullOrEmpty(request.SearchTerm))
            {
                result = result.Where(x => x.Ten.ToLower().Contains(request.SearchTerm.ToLower().Trim()) || x.Ma.ToLower().Contains(request.SearchTerm.ToLower().Trim())
                ).ToList();
            }
            return result;
        }

        public async Task<List<SelectItem>> GetDonGiaChietTinh(int IdPhanLoai)
        {
            return await _unitOfWork.DonGiaChietTinhRepository.GetQuery(x => x.IdPhanLoai == IdPhanLoai)
                .Include(x => x.DM_VatLieuChietTinh)
                .Select(x => new SelectItem
                {
                    Name = x.DM_VatLieuChietTinh.TenVatLieuChietTinh + " / Tổng giá: " + x.TongGia,
                    Value = x.TongGia.ToString()

                }).AsNoTracking().ToListAsync();
        }

        // lấy dữ liệu phân trang, tìm kiếm , số lượng
        public async Task<ChiTietBieuGiaResult> GetList(ChiTietBieuGiaRequest request)
        {
            var chuaCoDuLieu = false;

            var quyTruoc = request.Quy == 1 ? 4 : request.Quy - 1;
            var namTruoc = request.Quy == 1 ? request.Nam - 1 : request.Nam;

            var result = new ChiTietBieuGiaResult();
            decimal soLuongCVC = 0;

            //Tạo câu query
            var query = await _unitOfWork.BieuGiaCongViecRepository.GetQuery(x => x.IdBieuGia == request.IdBieuGia)
                .Include(x => x.DM_BieuGia.ChiTietBieuGia.Where(x => x.Quy == request.Quy && x.Nam == request.Nam)).Include(x => x.DM_CongViec)
                .AsNoTracking()
                .Select(x => new ChiTietBieuGiaResponse
                {
                    Id = x.DM_BieuGia.ChiTietBieuGia.FirstOrDefault(y => y.IDCongViec == x.IdCongViec && y.Nam == request.Nam && y.Quy == request.Quy).Id,
                    IdBieuGia = x.DM_BieuGia.Id,
                    MaNoiDungCongViec = x.DM_CongViec.MaCongViec,
                    NoiDungCongViec = x.DM_CongViec.TenCongViec,
                    DonViTinh = x.DM_CongViec.DonViTinh,
                    IdCongViec = x.DM_CongViec.Id,
                    Nam = request.Nam,
                    Quy = request.Quy,
                    SoLuong = Math.Round(x.DM_BieuGia.ChiTietBieuGia.FirstOrDefault(y => y.IDCongViec == x.IdCongViec && y.Nam == request.Nam && y.Quy == request.Quy) != null
                    ? x.DM_BieuGia.ChiTietBieuGia.FirstOrDefault(y => y.IDCongViec == x.IdCongViec && y.Nam == request.Nam && y.Quy == request.Quy).SoLuong :
                     x.DM_BieuGia.ChiTietBieuGia.FirstOrDefault(y => y.IDCongViec == x.IdCongViec && y.Nam == namTruoc && y.Quy == quyTruoc).SoLuong, 2), //0

                    HeSoDieuChinh_K1nc = Math.Round(x.DM_BieuGia.ChiTietBieuGia.FirstOrDefault(y => y.IDCongViec == x.IdCongViec && y.Nam == request.Nam && y.Quy == request.Quy) != null
                    ? x.DM_BieuGia.ChiTietBieuGia.FirstOrDefault(y => y.IDCongViec == x.IdCongViec && y.Nam == request.Nam && y.Quy == request.Quy).HeSoDieuChinh_K1nc :
                    x.DM_BieuGia.ChiTietBieuGia.FirstOrDefault(y => y.IDCongViec == x.IdCongViec && y.Nam == namTruoc && y.Quy == quyTruoc).HeSoDieuChinh_K1nc, 2), //1

                    HeSoDieuChinh_K2nc = Math.Round(x.DM_BieuGia.ChiTietBieuGia.FirstOrDefault(y => y.IDCongViec == x.IdCongViec && y.Nam == request.Nam && y.Quy == request.Quy) != null
                    ? x.DM_BieuGia.ChiTietBieuGia.FirstOrDefault(y => y.IDCongViec == x.IdCongViec && y.Nam == request.Nam && y.Quy == request.Quy).HeSoDieuChinh_K2nc :
                    x.DM_BieuGia.ChiTietBieuGia.FirstOrDefault(y => y.IDCongViec == x.IdCongViec && y.Nam == namTruoc && y.Quy == quyTruoc).HeSoDieuChinh_K2nc, 2), // 1

                    HeSoDieuChinh_Kmtc = Math.Round(x.DM_BieuGia.ChiTietBieuGia.FirstOrDefault(y => y.IDCongViec == x.IdCongViec && y.Nam == request.Nam && y.Quy == request.Quy) != null
                    ? x.DM_BieuGia.ChiTietBieuGia.FirstOrDefault(y => y.IDCongViec == x.IdCongViec && y.Nam == request.Nam && y.Quy == request.Quy).HeSoDieuChinh_Kmtc :
                    x.DM_BieuGia.ChiTietBieuGia.FirstOrDefault(y => y.IDCongViec == x.IdCongViec && y.Nam == namTruoc && y.Quy == quyTruoc).HeSoDieuChinh_Kmtc, 2), // 1

                    DonGia_VL = Math.Round(x.DM_BieuGia.ChiTietBieuGia.FirstOrDefault(y => y.IDCongViec == x.IdCongViec && y.Nam == request.Nam && y.Quy == request.Quy) != null
                    ? x.DM_BieuGia.ChiTietBieuGia.FirstOrDefault(y => y.IDCongViec == x.IdCongViec && y.Nam == request.Nam && y.Quy == request.Quy).DonGia_VL :
                    x.DM_BieuGia.ChiTietBieuGia.FirstOrDefault(y => y.IDCongViec == x.IdCongViec && y.Nam == namTruoc && y.Quy == quyTruoc).DonGia_VL, 0), // 0

                    DonGia_NC = Math.Round(x.DM_BieuGia.ChiTietBieuGia.FirstOrDefault(y => y.IDCongViec == x.IdCongViec && y.Nam == request.Nam && y.Quy == request.Quy) != null
                    ? x.DM_BieuGia.ChiTietBieuGia.FirstOrDefault(y => y.IDCongViec == x.IdCongViec && y.Nam == request.Nam && y.Quy == request.Quy).DonGia_NC :
                    x.DM_BieuGia.ChiTietBieuGia.FirstOrDefault(y => y.IDCongViec == x.IdCongViec && y.Nam == namTruoc && y.Quy == quyTruoc).DonGia_NC, 0), // 0

                    DonGia_MTC = Math.Round(x.DM_BieuGia.ChiTietBieuGia.FirstOrDefault(y => y.IDCongViec == x.IdCongViec && y.Nam == request.Nam && y.Quy == request.Quy) != null
                    ? x.DM_BieuGia.ChiTietBieuGia.FirstOrDefault(y => y.IDCongViec == x.IdCongViec && y.Nam == request.Nam && y.Quy == request.Quy).DonGia_MTC :
                    x.DM_BieuGia.ChiTietBieuGia.FirstOrDefault(y => y.IDCongViec == x.IdCongViec && y.Nam == namTruoc && y.Quy == quyTruoc).DonGia_MTC, 0), // 0

                    CongViecChinh = x.CongViecChinh,
                }).AsSplitQuery()
                .ToListAsync();

            decimal donGiaVatLieu = 0;
            decimal donGiaNhanCong = 0;


            var stt = 1;
            foreach (var item in query)
            {
                item.SoLuong = item.SoLuong ?? 0;
                item.HeSoDieuChinh_K1nc = item.HeSoDieuChinh_K1nc ?? 1;
                item.HeSoDieuChinh_K2nc = item.HeSoDieuChinh_K2nc ?? 1;
                item.HeSoDieuChinh_Kmtc = item.HeSoDieuChinh_Kmtc ?? 1;
                item.DonGia_VL = item.DonGia_VL ?? 0;
                item.DonGia_NC = item.DonGia_NC ?? 0;
                item.DonGia_MTC = item.DonGia_MTC ?? 0;

                if (stt == 1 && item.Id == null) chuaCoDuLieu = true;
                if (string.IsNullOrEmpty(item.MaNoiDungCongViec))
                {
                    donGiaVatLieu += (item.DonGia_VL.Value * item.SoLuong.Value);
                }
                else
                {
                    donGiaNhanCong += (item.DonGia_VL.Value * item.SoLuong.Value);
                }

                item.Stt = stt.ToString();
                item.CPChung = string.IsNullOrEmpty(item.MaNoiDungCongViec) ? 0 : Math.Round((decimal)65 / 100 * (item.DonGia_NC.Value), 0);                                                  //12            
                item.CPNhaTam = 0;             //13             tạm thời không cho công thức        
                //item.CPNhaTam = Math.Round((item.DonGia_VL.Value + item.DonGia_NC.Value + item.DonGia_MTC.Value) * (decimal)1.2 / 100, 0);             //13                    
                item.CPCongViecKhongXDDuocTuTK = string.IsNullOrEmpty(item.MaNoiDungCongViec) ? 0 : Math.Round((item.DonGia_VL.Value + item.DonGia_NC.Value + item.DonGia_MTC.Value) * (decimal)2 / 100, 0); ; //14     
                item.ThuNhapChiuThue = string.IsNullOrEmpty(item.MaNoiDungCongViec) ? 0 : Math.Round((item.DonGia_VL.Value + item.DonGia_NC.Value + item.DonGia_MTC.Value + item.CPChung.Value + item.CPNhaTam.Value + item.CPCongViecKhongXDDuocTuTK.Value) * (decimal)6 / 100, 0);
                item.DonGiaTruocThue = Math.Round(item.DonGia_VL.Value + item.DonGia_NC.Value + item.DonGia_MTC.Value + item.CPChung.Value + item.CPNhaTam.Value + item.CPCongViecKhongXDDuocTuTK.Value + item.ThuNhapChiuThue.Value, 0); ;
                item.GiaTriTruocThue = Math.Round(item.SoLuong.Value * item.DonGiaTruocThue.Value, 0);
                result.Tong += Math.Round(item.GiaTriTruocThue.Value, 0);
                if (item.CongViecChinh && item.SoLuong > 0)
                {
                    soLuongCVC = item.SoLuong.Value;
                }
                stt++;
            }
            var congViecChinh = query.Where(x => x.CongViecChinh).FirstOrDefault();

            result.ListBieuGia = query.OrderByDescending(x => x.CongViecChinh).ToList();
            result.KhaoSat = 0;
            result.CongTruocThue = result.KhaoSat + result.Tong;
            result.DonGiaTongHopTruocThue = soLuongCVC == 0 ? 0 : Math.Round(result.Tong / soLuongCVC, 0);
            result.DonGiaThu5 = result.DonGiaTongHopTruocThue;
            result.DonGiaThu6 = (congViecChinh == null || congViecChinh.SoLuong.Value == (decimal)0.00) ? 0 : Math.Round((result.CongTruocThue - (congViecChinh.DonGia_VL.Value * congViecChinh.SoLuong.Value)) / congViecChinh.SoLuong.Value, 0);

            var itemLast = new ChiTietBieuGiaResponse();
            itemLast.NoiDungCongViec = "TỔNG CỘNG";
            itemLast.GiaTriTruocThue = result.Tong;

            result.ListBieuGia.Add(itemLast);

            var bieuGiaTongHop = await _unitOfWork.BieuGiaTongHopRepository
                .FindOneAsync(x => x.IdBieuGia == request.IdBieuGia && x.Quy == request.Quy && x.Nam == request.Nam && x.TinhTrang != (int)TinhTrangEnum.DaDuyet);

            result.ChuaCoDuLieu = chuaCoDuLieu;
            result.DonGiaThu7 = congViecChinh.SoLuong == 0 ? 0 : Math.Round((result.CongTruocThue - (donGiaVatLieu + (donGiaNhanCong * (decimal)1.06))) / congViecChinh.SoLuong.Value, 0);

            if (bieuGiaTongHop != null)
            {
                bieuGiaTongHop.DonGia = result.DonGiaThu5;
                bieuGiaTongHop.DonGia2 = result.DonGiaThu6;
                bieuGiaTongHop.DonGia3 = result.DonGiaThu7;
                _unitOfWork.BieuGiaTongHopRepository.Update(bieuGiaTongHop);
                await _unitOfWork.SaveChangesAsync();
            }
            return result;

        }

    }
}
