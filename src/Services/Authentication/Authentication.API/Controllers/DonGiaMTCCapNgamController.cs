using Authentication.Application.Commands.DonGiaMTC_CapNgamCommand;
using Authentication.Application.Queries.DonGiaMTC_CapNgamQuery;
using Authentication.Infrastructure.Properties;
using EVN.Core.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using System;
using Authentication.Application.Model.DonGiaMTC;
using Authentication.Application.Commands.DonGiaMTCCommand;

namespace Authentication.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DonGiaMTCCapNgamController : ControllerBase
    {
        private readonly IDonGiaMTC_CapNgamQuery _DonGiaMTC_CapNgamQuery; //kế thừa interface
        private readonly IMediator _mediator; //kế thừa để sử dụng command

        public DonGiaMTCCapNgamController(IDonGiaMTC_CapNgamQuery DonGiaMTC_CapNgamQuery, IMediator mediator)
        {
            _DonGiaMTC_CapNgamQuery = DonGiaMTC_CapNgamQuery;
            _mediator = mediator;
        }

        /// <summary>
        /// Danh sách tất cả đơn giá máy thi công cáp ngầm
        /// </summary>
        /// <returns></returns>
        [HttpGet("get-all")]
        [ProducesResponseType(typeof(ApiSuccessResult<List<SelectItem>>), (int)HttpStatusCode.OK)] // trả về dữ liệu model cho FE
        public async Task<IActionResult> GetAll()
        {
            var data = await _DonGiaMTC_CapNgamQuery.GetAll();
            return Ok(new ApiSuccessResult<List<SelectItem>>(data: data));
        }

        /// <summary>
        /// Danh sách đơn giá máy thi công cáp ngầm có phân trang, tổng số , tìm kiếm
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(ApiSuccessResult<IList<DonGiaMTCResponse>>), (int)HttpStatusCode.OK)] // trả về dữ liệu model cho FE
        public async Task<IActionResult> GetListUser([FromQuery] DonGiaMTCRequest request)
        {
            var data = await _DonGiaMTC_CapNgamQuery.GetList(request);
            return Ok(new ApiSuccessResult<IList<DonGiaMTCResponse>>
            {
                Data = data.Data,
                Paging = data.Paging
            });
        }

        /// <summary>
        /// Tạo mới đơn giá máy thi công cáp ngầm
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(ApiSuccessResult<bool>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Create([FromBody] CreateDonGiaMTC_CapNgamCommand command)
        {
            var user = await _mediator.Send(command);
            return Ok(new ApiSuccessResult<bool>(data: user, message: string.Format(Resources.MSG_CREATE_SUCCESS, "đơn giá máy thi công cáp ngầm")));
        }

        /// <summary>
        /// Sửa đơn giá máy thi công cáp ngầm
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(typeof(ApiSuccessResult<bool>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Update([FromBody] UpdateDonGiaMTC_CapNgamCommand command)
        {
            var user = await _mediator.Send(command);
            return Ok(new ApiSuccessResult<bool>(data: user, message: string.Format(Resources.MSG_UPDATE_SUCCESS, "đơn giá máy thi công cáp ngầm")));
        }

        /// <summary>
        /// Xoá đơn giá máy thi công cáp ngầm
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(ApiSuccessResult<bool>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var user = await _mediator.Send(new DeleteDonGiaMTC_CapNgamCommand(id));
            return Ok(new ApiSuccessResult<bool>(data: user, message: string.Format(Resources.MSG_DELETE_SUCCESS, "đơn giá máy thi công cáp ngầm")));
        }
    }
}
