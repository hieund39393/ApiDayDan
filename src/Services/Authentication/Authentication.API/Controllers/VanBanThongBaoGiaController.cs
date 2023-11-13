using Authentication.Application.Commands.CauHinhCommand;
using Authentication.Application.Commands.DM_VatLieuCommand;
using Authentication.Application.Model.CauHinh;
using Authentication.Application.Queries.CommonQuery;
using Authentication.Infrastructure.AggregatesModel.UserAggregate;
using Authentication.Infrastructure.Properties;
using EVN.Core.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System;
using System.Threading.Tasks;

namespace Authentication.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VanBanThongBaoGiaController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ICommonQuery _commonQuery;

        public VanBanThongBaoGiaController(IMediator mediator, ICommonQuery commonQuery)
        {
            _mediator = mediator;
            _commonQuery = commonQuery;
        }

        [HttpPost]
        public async Task<IActionResult> CreateVanBanThongBao([FromForm] VanBanThongBaoCommand request)
        {
            var data = await _mediator.Send(request);
            return Ok(new ApiSuccessResult<bool>(data: data, message: string.Format(Resources.MSG_CREATE_SUCCESS, "văn bản thông báo")));
        }

        /// <summary>
        /// Xoá danh mục văn bản thông báo
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(ApiSuccessResult<bool>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var user = await _mediator.Send(new DeleteVanBanThongBaoCommand(id));
            return Ok(new ApiSuccessResult<bool>(data: user, message: string.Format(Resources.MSG_DELETE_SUCCESS, "văn bản thông báo")));
        }
        [HttpGet]
        public async Task<IActionResult> GetListVanBanThongBao([FromQuery] VanBanThongBaoRequest request)
        {
            var data = await _commonQuery.ListVanBanThongBao(request);
            return Ok(new ApiSuccessResult<object>(data: data));
        } 
        [HttpGet("van-ban")]
        public async Task<IActionResult> GetVanBan([FromQuery] VanBanThongBaoRequest request)
        {
            var data = await _commonQuery.GetVanBan(request);
            return Ok(new ApiSuccessResult<string>(data: data));
        }
    }
}
