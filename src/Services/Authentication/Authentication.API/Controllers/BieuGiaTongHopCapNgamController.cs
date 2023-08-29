using Authentication.Application.Commands.BieuGiaTongHopCommand;
using Authentication.Application.Model.BieuGiaTongHop;
using Authentication.Application.Model.ChiTietBieuGia;
using Authentication.Application.Queries.BieuGiaTongHop_CapNgamQuery;
using Authentication.Application.Queries.BieuGiaTongHopQuery;
using Authentication.Infrastructure.AggregatesModel.UserAggregate;
using Authentication.Infrastructure.Properties;
using DinkToPdf;
using DinkToPdf.Contracts;
using EVN.Core.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Authentication.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BieuGiaTongHopCapNgamController : ControllerBase
    {
        private readonly IBieuGiaTongHop_CapNgamQuery _bieuGiaTongHop_CapNgamQuery; //kế thừa interface
        private readonly IMediator _mediator; //kế thừa để sử dụng command
        private readonly IConverter _converter;

        public BieuGiaTongHopCapNgamController(IBieuGiaTongHop_CapNgamQuery bieuGiaQuery, IMediator mediator, IConverter converter)
        {
            _bieuGiaTongHop_CapNgamQuery = bieuGiaQuery;
            _mediator = mediator;
            _converter = converter;
        }

        [HttpGet]
        [ProducesResponseType(typeof(ApiSuccessResult<IList<BieuGiaTongHopResponse>>), (int)HttpStatusCode.OK)] // trả về dữ liệu model cho FE
        public async Task<IActionResult> GetList([FromQuery] BieuGiaTongHopRequest request)
        {
            var data = await _bieuGiaTongHop_CapNgamQuery.GetList(request);
            return Ok(new ApiSuccessResult<List<BieuGiaTongHopResponse>>
            {
                Data = data
            });
        }



        [HttpGet("chi-tiet")]
        [ProducesResponseType(typeof(List<CSKHCapNgamResponse>), (int)HttpStatusCode.OK)] // trả về dữ liệu model cho FE
        public async Task<IActionResult> ChiTiet([FromQuery] ChiTietPDFRequest request)
        {
            var data = await _bieuGiaTongHop_CapNgamQuery.ChiTietPDF(request);
            return Ok(data);
        }


        [HttpPut]
        [ProducesResponseType(typeof(ApiSuccessResult<IList<BieuGiaTongHopResponse>>), (int)HttpStatusCode.OK)] // trả về dữ liệu model cho FE
        public async Task<IActionResult> UpdateBieuGiaTongHop([FromForm] UpdateBieuGiaTongHop_CapNgamCommand request)
        {
            var data = await _mediator.Send(request);
            return Ok(new ApiSuccessResult<bool>(data: data, message: request.TinhTrang == 0 ? "Gửi duyệt thành công" : "Duyệt thành công"));
        }

        [HttpGet("bao-cao")]
        public async Task<IActionResult> BaoCao([FromQuery] ChiTietPDFRequest request)
        {
            var data = await _bieuGiaTongHop_CapNgamQuery.BaoCaoExcel(request);
            var fileName = $"DonGiaCapTrenNgam-{request.Quy}-{request.Nam}.xlsx";
            Response.Headers.Add("Access-Control-Expose-Headers", "Content-Disposition");
            Response.Headers.Add("file-name", fileName);
            return File(data, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
        }

        [HttpGet("get-bao-cao")]
        public async Task<IActionResult> GetBaoCao([FromQuery] ChiTietPDFRequest request)
        {
            var data = await _bieuGiaTongHop_CapNgamQuery.GetBaoCao(request);
            return Ok(new ApiSuccessResult<object>(data: data));
        }

        [HttpGet("xuat-excel")]
        public async Task<IActionResult> XuatExcel([FromQuery] BieuGiaTongHopRequest request)
        {
            var data = await _bieuGiaTongHop_CapNgamQuery.XuatExcel(request);
            var fileName = $"BieuGiaTongHopCapNgam-{request.Quy}-{request.Nam}.xlsx";
            Response.Headers.Add("Access-Control-Expose-Headers", "Content-Disposition");
            Response.Headers.Add("file-name", fileName);
            return File(data, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
        }


        [HttpGet("get-don-gia-vat-lieu")]
        [ProducesResponseType(typeof(List<ApiDonGiaVatLieuResponse>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetDonGiaVatLieu([FromQuery] int vung)
        {
            var data = await _bieuGiaTongHop_CapNgamQuery.GetDuLieuDonGia(vung);

            return Ok(data);
        }

        [HttpGet("van-ban")]
        public async Task<IActionResult> GetVanBan([FromQuery] GetVanBanRequest request)
        {
            var data = await _bieuGiaTongHop_CapNgamQuery.GetVanBan(request);

            return Ok(new ApiSuccessResult<object>(data: data));
        }
    }
}
