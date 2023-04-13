using Authentication.Application.Model.ChiTietBieuGia;
using Authentication.Infrastructure.Repositories;
using EVN.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Authentication.Application.Queries.ChiTietBieuGiaQuery
{

    public interface IChiTietBieuGiaQuery // tạo interface (Quy tắc : có chữ I ở đầu để biết nó là interface)
    {

        Task<List<ChiTietBieuGiaResponse>> GetList(ChiTietBieuGiaRequest request); 
        Task<List<SelectItem>> GetBieuGiaByLoaiBieuGia(Guid loaiBieuGia);
    }
    public class ChiTietBieuGiaQuery : IChiTietBieuGiaQuery // kế thừa interface vừa tạo
    {
        private readonly IUnitOfWork _unitOfWork; // khai báo 
        public ChiTietBieuGiaQuery(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<SelectItem>> GetBieuGiaByLoaiBieuGia(Guid loaiBieuGia)
        {
           return await _unitOfWork.DM_BieuGiaRepository.GetQuery(x => x.idLoaiBieuGia == loaiBieuGia).Select(x => new SelectItem
           {
               Name = x.TenBieuGia,
               Value = x.Id.ToString()
           }).AsNoTracking().ToListAsync();
        }

        // lấy dữ liệu phân trang, tìm kiếm , số lượng
        public async Task<List<ChiTietBieuGiaResponse>> GetList(ChiTietBieuGiaRequest request)
        {

            //Tạo câu query
            var query = await _unitOfWork.ChiTietBieuGiaRepository.GetQuery(x => x.IDBieuGia == request.IDBieuGia && x.Nam == request.Nam && x.Quy == request.Quy)
                .Include(x => x.DM_CongViec)
                .Select(x => new ChiTietBieuGiaResponse()         // select dữ liệu
                {
                    Id = x.Id,                                                                                                      
                    IDBieuGia = x.IDBieuGia,                                                                                        
                    MaNoiDungCongViec = x.DM_CongViec.MaCongViec,                                                                                                   
                    NoiDungCongViec = x.DM_CongViec.TenCongViec,                                                                                 
                    DonViTinh = x.DM_CongViec.DonViTinh,                                                          
                    IDCongViec = x.IDCongViec,                                                                                                                 
                    Nam = x.Nam,                                                                                                                                                              
                    Quy = x.Quy,                                                                                                                           
                    SoLuong = x.SoLuong,                                                                    //5
                    HeSoDieuChinh_K1nc = x.HeSoDieuChinh_K1nc,                                              //6     
                    HeSoDieuChinh_K2nc = x.HeSoDieuChinh_K2nc,                                              //7
                    HeSoDieuChinh_K2mnc = x.HeSoDieuChinh_K2mnc,                                            //8           
                    DonGia_VL = x.DonGia_VL,                                                                //9   
                    DonGia_NC = x.DonGia_NC,                                                                //10   
                    DonGia_MTC = x.DonGia_MTC,                                                              //11    
                    CPChung = (decimal)0.65 * (x.DonGia_NC),                                                //12            
                    CPNhaTam = (x.DonGia_VL + x.DonGia_NC + x.DonGia_MTC) * (decimal)0.012,                 //13                    
                    CPCongViecKhongXDDuocTuTK = (x.DonGia_VL + x.DonGia_NC + x.DonGia_MTC) * (decimal)0.02, //14            
                    NgayTao = x.CreatedDate,
                }).AsNoTracking().ToListAsync();

            foreach (var item in query)
            {
                item.ThuNhapChiuThue = (item.DonGia_VL / item.CPCongViecKhongXDDuocTuTK) * (decimal)0.06;
                item.DonGiaTruocThue = item.DonGia_VL + item.DonGia_NC + item.DonGia_MTC + item.CPChung + item.CPNhaTam + item.CPCongViecKhongXDDuocTuTK + item.ThuNhapChiuThue;
                item.GiaTriTruocThue = item.SoLuong * item.DonGiaTruocThue;
            }

            return query;

        }

    }
}
