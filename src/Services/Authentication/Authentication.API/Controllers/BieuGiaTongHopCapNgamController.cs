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


        [HttpGet("chi-tiet-pdf")]
        [ProducesResponseType(typeof(ApiSuccessResult<IList<BieuGiaTongHopResponse>>), (int)HttpStatusCode.OK)] // trả về dữ liệu model cho FE
        public async Task<IActionResult> ChiTietPDF([FromQuery] ChiTietPDFRequest request)
        {
            var data = await _bieuGiaTongHop_CapNgamQuery.ChiTietPDF(request);
            var i = 0;
            var listDonGia = new List<byte[]>();

            foreach (var item in data)
            {
                string fileName = $"BieuGia_Quy{request.Quy}_Nam{request.Nam}_PhanLoai{i + 1}";


                var glb = new GlobalSettings
                {
                    ColorMode = ColorMode.Color,
                    Orientation = Orientation.Landscape,
                    PaperSize = PaperKind.A4,
                    Margins = new MarginSettings()
                    {
                        Bottom = 10,
                        Left = 10,
                        Right = 10,
                        Top = 15,
                    },
                    DocumentTitle = fileName,
                };

                var objectSettings = new ObjectSettings
                {
                    PagesCount = true,
                    HtmlContent = data[i],
                    WebSettings = { DefaultEncoding = "utf-8", UserStyleSheet = null }
                };

                var pdf = new HtmlToPdfDocument
                {
                    GlobalSettings = glb,
                    Objects = { objectSettings }
                };

                listDonGia.Add(_converter.Convert(pdf));
                i++;
            }

            return Ok(listDonGia);
        }


        [HttpPut]
        [ProducesResponseType(typeof(ApiSuccessResult<IList<BieuGiaTongHopResponse>>), (int)HttpStatusCode.OK)] // trả về dữ liệu model cho FE
        public async Task<IActionResult> UpdateBieuGiaTongHop([FromBody] UpdateBieuGiaTongHop_CapNgamCommand request)
        {
            var data = await _mediator.Send(request);
            return Ok(new ApiSuccessResult<bool>(data: data, message: request.TinhTrang == 0 ? "Gửi duyệt thành công" : "Duyệt thành công"));
        }
    }
}
