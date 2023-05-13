using Authentication.Application.Commands.BieuGiaTongHopCommand;
using Authentication.Application.Model.BieuGiaTongHop;
using Authentication.Application.Model.ChiTietBieuGia;
using Authentication.Application.Queries.BieuGiaTongHopQuery;
using Authentication.Infrastructure.AggregatesModel.UserAggregate;
using Authentication.Infrastructure.Properties;
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

        public BieuGiaTongHopController(IBieuGiaTongHopQuery bieuGiaQuery, IMediator mediator)
        {
            _bieuGiaTongHopQuery = bieuGiaQuery;
            _mediator = mediator;
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


        [HttpGet("chi-tiet-pdf")]
        [ProducesResponseType(typeof(ApiSuccessResult<IList<BieuGiaTongHopResponse>>), (int)HttpStatusCode.OK)] // trả về dữ liệu model cho FE
        public async Task<IActionResult> ChiTietPDF([FromQuery] BieuGiaTongHopRequest request)
        {
            var data = await _bieuGiaTongHopQuery.GetList(request);
            return Ok(new ApiSuccessResult<List<BieuGiaTongHopResponse>>
            {
                Data = data
            });
        }


        [HttpPut]
        [ProducesResponseType(typeof(ApiSuccessResult<IList<BieuGiaTongHopResponse>>), (int)HttpStatusCode.OK)] // trả về dữ liệu model cho FE
        public async Task<IActionResult> UpdateBieuGiaTongHop([FromBody] UpdateBieuGiaTongHopCommand request)
        {
            var data = await _mediator.Send(request);
            return Ok(new ApiSuccessResult<bool>(data: data, message: request.TinhTrang == 0 ? "Gửi duyệt thành công" : "Duyệt thành công"));
        }
    }
}
