using Authentication.Application.Model.BieuGiaTongHop;
using Authentication.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace Authentication.Application.Queries.BieuGiaTongHop_CapNgamQuery
{
    public interface IBieuGiaTongHop_CapNgamQuery
    {
        Task<List<BieuGiaTongHopResponse>> GetList(BieuGiaTongHopRequest request);
        Task<List<string>> ChiTietPDF(ChiTietPDFRequest request);
    }
    public class BieuGiaTongHop_CapNgamQuery : IBieuGiaTongHop_CapNgamQuery
    {
        private readonly IUnitOfWork _unitOfWork;
        public BieuGiaTongHop_CapNgamQuery(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<string>> ChiTietPDF(ChiTietPDFRequest request)
        {
            var loaiBieuGia = await _unitOfWork.DM_LoaiBieuGia_CapNgamRepository.GetQuery().AsNoTracking().ToListAsync();
            var query = await _unitOfWork.BieuGiaTongHop_CapNgamRepository.GetQuery(x => x.Nam == request.Nam && x.Quy == request.Quy)
                .Include(x => x.DM_BieuGia_CapNgam).ThenInclude(x => x.DM_LoaiBieuGia_CapNgam)
                .Select(x => new
                {
                    IdBieuGia = x.IdBieuGia,
                    TenBieuGia = x.DM_BieuGia_CapNgam.TenBieuGia,
                    IdLoaiBieuGia = x.DM_BieuGia_CapNgam.idLoaiBieuGia,
                    IdKhuVuc = x.DM_BieuGia_CapNgam.DM_LoaiBieuGia_CapNgam.IdKhuVuc,
                    DonGia = x.DonGia,
                    DonGia2 = x.DonGia2,
                    DonGia3 = x.DonGia3,
                    TinhTrang = x.TinhTrang
                }).AsNoTracking()
                .ToListAsync();

            var groupBy = query.GroupBy(x => x.TenBieuGia).Select(x => new { name = x.Key, listBG = x.ToList() }).ToList();

            var listResponse = new List<BieuGiaTongHopResponse>();
            var listData = new List<string>();
            var listData2 = new List<string>();
            var listData3 = new List<string>();
            foreach (var r in groupBy)
            {
                var item = new BieuGiaTongHopResponse();
                item.TenBieuGia = r.name;
                item.DonVi = "m";

                foreach (var list in loaiBieuGia)
                {
                    var value = r.listBG.Where(x => x.IdKhuVuc == list.IdKhuVuc && x.IdLoaiBieuGia == list.Id).FirstOrDefault()?.DonGia.ToString() ?? "";
                    var value2 = r.listBG.Where(x => x.IdKhuVuc == list.IdKhuVuc && x.IdLoaiBieuGia == list.Id).FirstOrDefault()?.DonGia2.ToString() ?? "";
                    var value3 = r.listBG.Where(x => x.IdKhuVuc == list.IdKhuVuc && x.IdLoaiBieuGia == list.Id).FirstOrDefault()?.DonGia3.ToString() ?? "";
                    listData.Add(value);
                    listData2.Add(value2);
                    listData3.Add(value3);
                }
                item.ListData = listData;
                item.TinhTrang = r.listBG.FirstOrDefault()?.TinhTrang ?? null;
                listResponse.Add(item);
            }


            string templatePath = Path.Combine(Directory.GetCurrentDirectory(), "Templates", "bieugia.html");
            string tempHtml = File.ReadAllText(templatePath);
            StringBuilder stringData = new StringBuilder();
            StringBuilder stringData2 = new StringBuilder();
            StringBuilder stringData3 = new StringBuilder();

            // DG1
            for (int i = 0; i < listResponse.Count; i++)
            {
                stringData.Append($"<tr><td class='center'>{i + 1}</td>");
                stringData.Append($"<td>{listResponse[i].TenBieuGia}</td>");
                stringData.Append($"<td class='center'>{listResponse[i].DonVi}</td>");
                stringData.Append($"<td class='center'>{listData[0]}</td>");
                stringData.Append($"<td class='center'>{listData[1]}</td>");
                stringData.Append($"<td class='center'>{listData[2]}</td>");
                stringData.Append($"<td class='center'>{listData[3]}</td>");
                stringData.Append($"<td class='center'>{listData[4]}</td>");
                stringData.Append($"<td class='center'>{listData[5]}</td>");
                stringData.Append($"<td class='center'>{listData[6]}</td>");
                stringData.Append($"<td class='center'>{listData[7]}</td>");
                stringData.Append($"<td class='center'>{listData[8]}</td>");
                stringData.Append($"<td class='center'>{listData[9]}</td>");
                stringData.Append($"<td class='center'>{listData[10]}</td>");
                stringData.Append($"<td class='center'>{listData[11]}</td></tr>");
            }

            // DG2
            for (int i = 0; i < listResponse.Count; i++)
            {
                stringData2.Append($"<tr><td class='center'>{i + 1}</td>");
                stringData2.Append($"<td>{listResponse[i].TenBieuGia}</td>");
                stringData2.Append($"<td class='center'>{listResponse[i].DonVi}</td>");
                stringData2.Append($"<td class='center'>{listData2[0]}</td>");
                stringData2.Append($"<td class='center'>{listData2[1]}</td>");
                stringData2.Append($"<td class='center'>{listData2[2]}</td>");
                stringData2.Append($"<td class='center'>{listData2[3]}</td>");
                stringData2.Append($"<td class='center'>{listData2[4]}</td>");
                stringData2.Append($"<td class='center'>{listData2[5]}</td>");
                stringData2.Append($"<td class='center'>{listData2[6]}</td>");
                stringData2.Append($"<td class='center'>{listData2[7]}</td>");
                stringData2.Append($"<td class='center'>{listData2[8]}</td>");
                stringData2.Append($"<td class='center'>{listData2[9]}</td>");
                stringData2.Append($"<td class='center'>{listData2[10]}</td>");
                stringData2.Append($"<td class='center'>{listData2[11]}</td></tr>");
            }

            // DG3
            for (int i = 0; i < listResponse.Count; i++)
            {
                stringData3.Append($"<tr><td class='center'>{i + 1}</td>");
                stringData3.Append($"<td>{listResponse[i].TenBieuGia}</td>");
                stringData3.Append($"<td class='center'>{listResponse[i].DonVi}</td>");
                stringData3.Append($"<td class='center'>{listData3[0]}</td>");
                stringData3.Append($"<td class='center'>{listData3[1]}</td>");
                stringData3.Append($"<td class='center'>{listData3[2]}</td>");
                stringData3.Append($"<td class='center'>{listData3[3]}</td>");
                stringData3.Append($"<td class='center'>{listData3[4]}</td>");
                stringData3.Append($"<td class='center'>{listData3[5]}</td>");
                stringData3.Append($"<td class='center'>{listData3[6]}</td>");
                stringData3.Append($"<td class='center'>{listData3[7]}</td>");
                stringData3.Append($"<td class='center'>{listData3[8]}</td>");
                stringData3.Append($"<td class='center'>{listData3[9]}</td>");
                stringData3.Append($"<td class='center'>{listData3[10]}</td>");
                stringData3.Append($"<td class='center'>{listData3[11]}</td></tr>");
            }

            var data = tempHtml.Replace("{data}", stringData.ToString());
            var data2 = tempHtml.Replace("{data}", stringData2.ToString());
            var data3 = tempHtml.Replace("{data}", stringData3.ToString());

            var listDGTH = new List<string>();
            listDGTH.Add(data);
            listDGTH.Add(data2);
            listDGTH.Add(data3);
            return listDGTH;

        }

        public async Task<List<BieuGiaTongHopResponse>> GetList(BieuGiaTongHopRequest request)
        {
            var loaiBieuGia = await _unitOfWork.DM_LoaiBieuGia_CapNgamRepository.GetQuery().AsNoTracking().ToListAsync();
            var query = await _unitOfWork.BieuGiaTongHop_CapNgamRepository.GetQuery(x => x.Nam == request.Nam && x.Quy == request.Quy)
                .Include(x => x.DM_BieuGia_CapNgam).ThenInclude(x => x.DM_LoaiBieuGia_CapNgam)
                .Select(x => new
                {
                    IdBieuGia = x.IdBieuGia,
                    TenBieuGia = x.DM_BieuGia_CapNgam.TenBieuGia,
                    IdLoaiBieuGia = x.DM_BieuGia_CapNgam.idLoaiBieuGia,
                    IdKhuVuc = x.DM_BieuGia_CapNgam.DM_LoaiBieuGia_CapNgam.IdKhuVuc,
                    DonGia = request.PhanLoai == 1 ? x.DonGia : (request.PhanLoai == 2 ? x.DonGia2 : x.DonGia3),
                    TinhTrang = x.TinhTrang
                }).AsNoTracking()
                .ToListAsync();

            var groupBy = query.GroupBy(x => x.TenBieuGia).Select(x => new { name = x.Key, listBG = x.ToList() }).ToList();

            var listResponse = new List<BieuGiaTongHopResponse>();
            foreach (var r in groupBy)
            {
                var item = new BieuGiaTongHopResponse();
                item.TenBieuGia = r.name;
                item.DonVi = "m";
                var listData = new List<string>();
                foreach (var list in loaiBieuGia)
                {
                    var value = r.listBG.Where(x => x.IdKhuVuc == list.IdKhuVuc && x.IdLoaiBieuGia == list.Id).FirstOrDefault()?.DonGia.ToString() ?? "";
                    listData.Add(value);
                }
                item.ListData = listData;
                item.TinhTrang = r.listBG.FirstOrDefault()?.TinhTrang ?? null;
                listResponse.Add(item);
            }

            return listResponse;
        }
    }
}
