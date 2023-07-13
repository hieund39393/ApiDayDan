using Authentication.Application.Model.BieuGiaTongHop;
using Authentication.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace Authentication.Application.Queries.BieuGiaTongHopQuery
{
    public interface IBieuGiaTongHopQuery
    {
        Task<List<BieuGiaTongHopResponse>> GetList(BieuGiaTongHopRequest request);
        Task<List<string>> ChiTietPDF(ChiTietPDFRequest request);
    }
    public class BieuGiaTongHopQuery : IBieuGiaTongHopQuery
    {
        private readonly IUnitOfWork _unitOfWork;
        public BieuGiaTongHopQuery(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<string>> ChiTietPDF(ChiTietPDFRequest request)
        {
            var loaiBieuGia = await _unitOfWork.DM_LoaiBieuGiaRepository.GetQuery().AsNoTracking().ToListAsync();
            var query = await _unitOfWork.BieuGiaTongHopRepository.GetQuery(x => x.Nam == request.Nam && x.Quy == request.Quy)
                .Include(x => x.DM_BieuGia).ThenInclude(x => x.DM_LoaiBieuGia)
                .Select(x => new
                {
                    IdBieuGia = x.IdBieuGia,
                    TenBieuGia = x.DM_BieuGia.TenBieuGia,
                    IdLoaiBieuGia = x.DM_BieuGia.idLoaiBieuGia,
                    IdKhuVuc = x.DM_BieuGia.DM_LoaiBieuGia.IdKhuVuc,
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

            int index = 0;
            for (int i = 0; i < listResponse.Count; i++)
            {
                stringData.Append($"<tr><td class='center'>{i + 1}</td>");
                stringData.Append($"<td>{listResponse[i].TenBieuGia}</td>");
                stringData.Append($"<td class='center'>{listResponse[i].DonVi}</td>");
                stringData.Append($"<td class='center'>{listData[index]}</td>"); index++;
                stringData.Append($"<td class='center'>{listData[index]}</td>"); index++;
                stringData.Append($"<td class='center'>{listData[index]}</td>"); index++;
                stringData.Append($"<td class='center'>{listData[index]}</td>"); index++;
                stringData.Append($"<td class='center'>{listData[index]}</td>"); index++;
                stringData.Append($"<td class='center'>{listData[index]}</td>"); index++;
                stringData.Append($"<td class='center'>{listData[index]}</td>"); index++;
                stringData.Append($"<td class='center'>{listData[index]}</td>"); index++;
                stringData.Append($"<td class='center'>{listData[index]}</td>"); index++;
                stringData.Append($"<td class='center'>{listData[index]}</td>"); index++;
                stringData.Append($"<td class='center'>{listData[index]}</td>"); index++;
                stringData.Append($"<td class='center'>{listData[index]}</td></tr>"); index++;
            }

            // DG2
            int index1 = 0;
            for (int i = 0; i < listResponse.Count; i++)
            {
                stringData2.Append($"<tr><td class='center'>{i + 1}</td>");
                stringData2.Append($"<td>{listResponse[i].TenBieuGia}</td>");
                stringData2.Append($"<td class='center'>{listResponse[i].DonVi}</td>");
                stringData2.Append($"<td class='center'>{listData2[index1]}</td>"); index1++;
                stringData2.Append($"<td class='center'>{listData2[index1]}</td>"); index1++;
                stringData2.Append($"<td class='center'>{listData2[index1]}</td>"); index1++;
                stringData2.Append($"<td class='center'>{listData2[index1]}</td>"); index1++;
                stringData2.Append($"<td class='center'>{listData2[index1]}</td>"); index1++;
                stringData2.Append($"<td class='center'>{listData2[index1]}</td>"); index1++;
                stringData2.Append($"<td class='center'>{listData2[index1]}</td>"); index1++;
                stringData2.Append($"<td class='center'>{listData2[index1]}</td>"); index1++;
                stringData2.Append($"<td class='center'>{listData2[index1]}</td>"); index1++;
                stringData2.Append($"<td class='center'>{listData2[index1]}</td>"); index1++;
                stringData2.Append($"<td class='center'>{listData2[index1]}</td>"); index1++;
                stringData2.Append($"<td class='center'>{listData2[index1]}</td></tr>"); index1++;
            }
            // DG3
            int index2 = 0;
            for (int i = 0; i < listResponse.Count; i++)
            {
                stringData3.Append($"<tr><td class='center'>{i + 1}</td>");
                stringData3.Append($"<td>{listResponse[i].TenBieuGia}</td>");
                stringData3.Append($"<td class='center'>{listResponse[i].DonVi}</td>");
                stringData3.Append($"<td class='center'>{listData3[index2]}</td>"); index2++;
                stringData3.Append($"<td class='center'>{listData3[index2]}</td>"); index2++;
                stringData3.Append($"<td class='center'>{listData3[index2]}</td>"); index2++;
                stringData3.Append($"<td class='center'>{listData3[index2]}</td>"); index2++;
                stringData3.Append($"<td class='center'>{listData3[index2]}</td>"); index2++;
                stringData3.Append($"<td class='center'>{listData3[index2]}</td>"); index2++;
                stringData3.Append($"<td class='center'>{listData3[index2]}</td>"); index2++;
                stringData3.Append($"<td class='center'>{listData3[index2]}</td>"); index2++;
                stringData3.Append($"<td class='center'>{listData3[index2]}</td>"); index2++;
                stringData3.Append($"<td class='center'>{listData3[index2]}</td>"); index2++;
                stringData3.Append($"<td class='center'>{listData3[index2]}</td>"); index2++;
                stringData3.Append($"<td class='center'>{listData3[index2]}</td></tr>"); index2++;
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
            var loaiBieuGia = await _unitOfWork.DM_LoaiBieuGiaRepository.GetQuery().AsNoTracking().ToListAsync();
            var query = await _unitOfWork.BieuGiaTongHopRepository.GetQuery(x => x.Nam == request.Nam && x.Quy == request.Quy)
                .Include(x => x.DM_BieuGia).ThenInclude(x => x.DM_LoaiBieuGia)
                .Select(x => new
                {
                    IdBieuGia = x.IdBieuGia,
                    TenBieuGia = x.DM_BieuGia.TenBieuGia,
                    IdLoaiBieuGia = x.DM_BieuGia.idLoaiBieuGia,
                    IdKhuVuc = x.DM_BieuGia.DM_LoaiBieuGia.IdKhuVuc,
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
