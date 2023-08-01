using Authentication.Application.Commands.BieuGiaTongHopCommand;
using Authentication.Application.Model.BieuGiaTongHop;
using Authentication.Application.Model.ChiTietBieuGia;
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
    public class BieuGiaTongHopController : ControllerBase
    {
        private readonly IBieuGiaTongHopQuery _bieuGiaTongHopQuery; //kế thừa interface
        private readonly IMediator _mediator; //kế thừa để sử dụng command
        private readonly IConverter _converter;

        public BieuGiaTongHopController(IBieuGiaTongHopQuery bieuGiaQuery, IMediator mediator, IConverter converter)
        {
            _bieuGiaTongHopQuery = bieuGiaQuery;
            _mediator = mediator;
            _converter = converter;
        }

        [HttpGet]
        [ProducesResponseType(typeof(ApiSuccessResult<IList<BieuGiaTongHopResponse>>), (int)HttpStatusCode.OK)] // trả về dữ liệu model cho FE
        public async Task<IActionResult> GetList([FromQuery] BieuGiaTongHopRequest request)
        {
            var data = await _bieuGiaTongHopQuery.GetList(request);
            return Ok(new ApiSuccessResult<List<BieuGiaTongHopResponse>>
            {
                Data = data
            });
        }


        [HttpGet("chi-tiet")]
        [ProducesResponseType(typeof(List<CSKHResponse>), (int)HttpStatusCode.OK)] // trả về dữ liệu model cho FE
        public async Task<IActionResult> ChiTiet([FromQuery] ChiTietPDFRequest request)
        {
            var data = await _bieuGiaTongHopQuery.ChiTietPDF(request);
            return Ok(data);

        }


        [HttpPut]
        [ProducesResponseType(typeof(ApiSuccessResult<IList<BieuGiaTongHopResponse>>), (int)HttpStatusCode.OK)] // trả về dữ liệu model cho FE
        public async Task<IActionResult> UpdateBieuGiaTongHop([FromBody] UpdateBieuGiaTongHopCommand request)
        {
            var data = await _mediator.Send(request);
            return Ok(new ApiSuccessResult<bool>(data: data, message: request.TinhTrang == 0 ? "Gửi duyệt thành công" : "Duyệt thành công"));
        }

        [HttpGet("bao-cao")]
        public async Task<IActionResult> BaoCao([FromQuery] ChiTietPDFRequest request)
        {
            var data = await _bieuGiaTongHopQuery.BaoCaoExcel(request);
            var fileName = $"DonGiaCapTrenKhong-{request.Quy}-{request.Nam}.xlsx";
            Response.Headers.Add("Access-Control-Expose-Headers", "Content-Disposition");
            Response.Headers.Add("file-name", fileName);
            return File(data, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
        }

        [HttpGet("get-don-gia-vat-lieu")]
        public async Task<IActionResult> GetDonGiaVatLieu([FromQuery] int vung, string loaiCap)
        {
            var data = await _bieuGiaTongHopQuery.GetDuLieuDonGia(vung, loaiCap);

            return Ok(data);
        }
    }
}
