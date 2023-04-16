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
            var query = await _unitOfWork.BieuGiaCongViecRepository.GetQuery(x => x.IdBieuGia == request.IdBieuGia)
                .Include(x => x.DM_BieuGias)
                .Include(x => x.DM_CongViecs)
                .Include(x => x.ChiTietBieuGia)
                .Select(x => new ChiTietBieuGiaResponse()         // select dữ liệu
                {
                    Id = x.Id,
                    IdBieuGia = x.IdBieuGia,
                    MaNoiDungCongViec = x.DM_CongViecs.MaCongViec,
                    NoiDungCongViec = x.DM_CongViecs.TenCongViec,
                    DonViTinh = x.DM_CongViecs.DonViTinh,
                    IdCongViec = x.IdCongViec,
                    Nam = x.ChiTietBieuGia.FirstOrDefault(x => x.IDBieuGia == request.IdBieuGia && x.Nam == request.Nam && x.Quy == request.Quy).Nam,
                    Quy = x.ChiTietBieuGia.FirstOrDefault(x => x.IDBieuGia == request.IdBieuGia && x.Nam == request.Nam && x.Quy == request.Quy).Quy,
                    SoLuong = x.ChiTietBieuGia.FirstOrDefault(x => x.IDBieuGia == request.IdBieuGia && x.Nam == request.Nam && x.Quy == request.Quy).SoLuong,                          //5
                    HeSoDieuChinh_K1nc = x.ChiTietBieuGia.FirstOrDefault(x => x.IDBieuGia == request.IdBieuGia && x.Nam == request.Nam && x.Quy == request.Quy).HeSoDieuChinh_K1nc,    //6     
                    HeSoDieuChinh_K2nc = x.ChiTietBieuGia.FirstOrDefault(x => x.IDBieuGia == request.IdBieuGia && x.Nam == request.Nam && x.Quy == request.Quy).HeSoDieuChinh_K2nc,    //7
                    HeSoDieuChinh_K2mnc = x.ChiTietBieuGia.FirstOrDefault(x => x.IDBieuGia == request.IdBieuGia && x.Nam == request.Nam && x.Quy == request.Quy).HeSoDieuChinh_K2mnc,  //8           
                    DonGia_VL = x.ChiTietBieuGia.FirstOrDefault(x => x.IDBieuGia == request.IdBieuGia && x.Nam == request.Nam && x.Quy == request.Quy).DonGia_VL,                      //9   
                    DonGia_NC = x.ChiTietBieuGia.FirstOrDefault(x => x.IDBieuGia == request.IdBieuGia && x.Nam == request.Nam && x.Quy == request.Quy).DonGia_NC,                      //10   
                    DonGia_MTC = x.ChiTietBieuGia.FirstOrDefault(x => x.IDBieuGia == request.IdBieuGia && x.Nam == request.Nam && x.Quy == request.Quy).DonGia_MTC,                    //11    
                    CPChung = 0,
                    CPNhaTam = 0,
                    CPCongViecKhongXDDuocTuTK = 0,
                    NgayTao = x.CreatedDate,
                }).AsNoTracking().ToListAsync();




            foreach (var item in query)
            {
                item.CPChung = (decimal)0.65 * (item.DonGia_NC);                                                      //12            
                item.CPNhaTam = (item.DonGia_VL + item.DonGia_NC + item.DonGia_MTC) * (decimal)0.012;                 //13                    
                item.CPCongViecKhongXDDuocTuTK = (item.DonGia_VL + item.DonGia_NC + item.DonGia_MTC) * (decimal)0.02; //14     
                item.ThuNhapChiuThue = (item.DonGia_VL / item.CPCongViecKhongXDDuocTuTK) * (decimal)0.06;
                item.DonGiaTruocThue = item.DonGia_VL + item.DonGia_NC + item.DonGia_MTC + item.CPChung + item.CPNhaTam + item.CPCongViecKhongXDDuocTuTK + item.ThuNhapChiuThue;
                item.GiaTriTruocThue = item.SoLuong * item.DonGiaTruocThue;
            }

            return query;

        }

    }
}
