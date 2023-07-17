using Authentication.Application.Model.CauHinhChietTinh;
using Authentication.Application.Model.DonGiaChietTinh;
using Authentication.Infrastructure.Repositories;
using EVN.Core.Extensions;
using EVN.Core.SeedWork;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using static EVN.Core.Common.AppEnum;

namespace Authentication.Application.Queries.DonGiaChietTinhQuery
{

    public interface IDonGiaChietTinhQuery // tạo interface (Quy tắc : có chữ I ở đầu để biết nó là interface)
    {

        Task<List<DonGiaChietTinhResponse>> GetList(DonGiaChietTinhRequest request); // lấy danh sách có phân trang và tìm kiếm
                                                                                     //  Task<List<SelectItem>> GetAll(); // lấy Tất cả danh sách trả về tên và value
    }
    public class DonGiaChietTinhQuery : IDonGiaChietTinhQuery // kế thừa interface vừa tạo
    {
        private readonly IUnitOfWork _unitOfWork; // khai báo 
        public DonGiaChietTinhQuery(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // lấy Tất cả danh sách trả về tên và value thường dùng cho combobox
        //public async Task<List<SelectItem>> GetAll()
        //{
        //    var query = await _unitOfWork.DonGiaChietTinhRepository.GetQuery().Select(x => new SelectItem
        //    {
        //        Name = x.,
        //        Value = x.Id.ToString(),
        //    }).AsNoTracking().ToListAsync();
        //    return query;
        //}

        // lấy dữ liệu phân trang, tìm kiếm , số lượng

        public async Task<List<DonGiaChietTinhResponse>> GetList(DonGiaChietTinhRequest request)
        {
            var dGVL = await _unitOfWork.DonGiaVatLieuRepository.GetQuery().Include(x => x.DM_VatLieu).AsNoTracking()
                .GroupBy(x => x.IdVatLieu).Select(x => x.OrderByDescending(y => y.CreatedDate).First())
                .ToListAsync();

            var dGNC = await _unitOfWork.DonGiaNhanCongRepository.GetQuery().Include(x => x.NhanCong).AsNoTracking()
                 .GroupBy(x => new { x.IdNhanCong }).Select(x => x.OrderByDescending(y => y.CreatedDate).First())
                .ToListAsync();

            var dGMTC = await _unitOfWork.DonGiaMTCRepository.GetQuery().Include(x => x.DM_MTC).AsNoTracking()
                 .GroupBy(x => x.IdMTC).Select(x => x.OrderByDescending(y => y.CreatedDate).First())
                .ToListAsync();

            var dGCT = await _unitOfWork.DonGiaChietTinhRepository.GetQuery().AsNoTracking().ToListAsync();

            var data = await _unitOfWork.CauHinhChietTinhRepository.GetQuery().Include(x => x.DM_CongViec).GroupBy(x => x.IdCongViec).Select(x => new
            {
                IdCongViec = x.Key,
                IdChiTiet = x.OrderBy(x => x.PhanLoai).ToList()
            }).ToListAsync();

            var listResult = new List<DonGiaChietTinhResponse>();
            int stt = 1;
            foreach (var item in data)
            {
                var ct = dGCT.Where(x => x.IdCongViec == item.IdCongViec).OrderByDescending(x=>x.CreatedDate).FirstOrDefault();
                int index = 1;
                listResult.Add(new DonGiaChietTinhResponse
                {
                    STT = stt,
                    Ma = item.IdChiTiet.First().DM_CongViec.MaCongViec,
                    IdCongViec = item.IdCongViec.Value,
                    TenVatLieu = item.IdChiTiet.First().DM_CongViec.TenCongViec,
                    TongGia_VL = ct?.DonGiaVatLieu,
                    TongGia_NC = ct?.DonGiaNhanCong,
                    TongGia_MTC = ct?.DonGiaMTC,
                    Level = 1
                });
                foreach (var child in item.IdChiTiet)
                {
                    if (index == child.PhanLoai)
                    {
                        listResult.Add(new DonGiaChietTinhResponse { IdCongViec = item.IdCongViec.Value, TenVatLieu = GetDescription((PhanLoaiChietTinhEnum)child.PhanLoai), Level = 2 });
                        index++;
                    }
                    if (child.PhanLoai == 1)
                    {
                        var vatLieu = dGVL.Where(x => x.IdVatLieu == child.IdChiTiet).FirstOrDefault();
                        listResult.Add(new DonGiaChietTinhResponse
                        {
                            IdVatLieu = vatLieu.Id,
                            IdCongViec = item.IdCongViec.Value,
                            TenVatLieu = vatLieu?.DM_VatLieu?.TenVatLieu,
                            DonVi = vatLieu?.DM_VatLieu?.DonViTinh,
                            DGVL = vatLieu?.DonGia,
                            DinhMuc = vatLieu?.DinhMuc,
                            Level = 3,
                            PhanLoai = 1,
                            //IsDinhMucCu = (vatLieu.DinhMucCu != null && vatLieu.DinhMucCu != vatLieu.DinhMuc) ? true : false,
                            //IsDonGiaCu = (vatLieu.DonGiaCu != null && vatLieu.DonGiaCu != vatLieu.DonGia) ? true : false,
                            IsDonGiaCu = ct?.CreatedDate < vatLieu.CreatedDate ? true : false,
                            IsDinhMucCu = ct?.CreatedDate < vatLieu.CreatedDate ? true : false,
                        }); ;
                    }
                    else if (child.PhanLoai == 2)
                    {
                        var nhanCong = dGNC.Where(x => x.IdNhanCong == child.IdChiTiet).FirstOrDefault();
                        listResult.Add(new DonGiaChietTinhResponse
                        {
                            IdVatLieu = nhanCong.Id,
                            IdCongViec = item.IdCongViec.Value,
                            TenVatLieu = nhanCong.NhanCong.CapBac,
                            DonVi = "công",
                            DGNC = nhanCong.DonGia,
                            DinhMuc = nhanCong?.DinhMuc,
                            Level = 3,
                            PhanLoai = 2,
                            IsDonGiaCu = ct?.CreatedDate < nhanCong.CreatedDate ? true : false,
                            IsDinhMucCu = ct?.CreatedDate < nhanCong.CreatedDate ? true : false,
                        });
                    }
                    else
                    {
                        var mTC = dGMTC.Where(x => x.IdMTC == child.IdChiTiet).FirstOrDefault();
                        listResult.Add(new DonGiaChietTinhResponse
                        {
                            IdVatLieu = mTC.Id,
                            IdCongViec = item.IdCongViec.Value,
                            TenVatLieu = mTC.DM_MTC.TenMayThiCong,
                            DonVi = mTC.DM_MTC.DonViTinh,
                            DGMTC = mTC.DonGia,
                            DinhMuc = mTC?.DinhMuc,
                            Level = 3,
                            PhanLoai = 3,
                            IsDonGiaCu = ct?.CreatedDate < mTC.CreatedDate ? true : false,
                            IsDinhMucCu = ct?.CreatedDate < mTC.CreatedDate ? true : false,
                        });
                    }
                }
                stt++;
            }
            int counter = 1;
            listResult.ForEach(item =>
             {
                 item.tt = counter;
                 counter++; // Tăng giá trị biến số đếm để gán giá trị tiếp theo
             });
            return listResult;
        }

        public static string GetDescription(Enum value)
        {
            var field = value.GetType().GetField(value.ToString());
            var attribute = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) as DescriptionAttribute;
            return attribute == null ? value.ToString() : attribute.Description;
        }

    }
}
