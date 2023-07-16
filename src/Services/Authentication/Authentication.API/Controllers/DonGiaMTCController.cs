using Authentication.Application.Commands.DonGiaMTCCommand;
using Authentication.Application.Model.DonGiaMTC;
using Authentication.Application.Queries.DonGiaMTCQuery;
using Authentication.Infrastructure.Properties;
using EVN.Core.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using System;

namespace Authentication.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DonGiaMTCController : ControllerBase
    {
        private readonly IDonGiaMTCQuery _DonGiaMTCQuery; //kế thừa interface
        private readonly IMediator _mediator; //kế thừa để sử dụng command

        public DonGiaMTCController(IDonGiaMTCQuery DonGiaMTCQuery, IMediator mediator)
        {
            _DonGiaMTCQuery = DonGiaMTCQuery;
            _mediator = mediator;
        }

        /// <summary>
        /// Danh sách tất cả đơn giá máy thi công
        /// </summary>
        /// <returns></returns>
        [HttpGet("get-all")]
        [ProducesResponseType(typeof(ApiSuccessResult<List<SelectItem>>), (int)HttpStatusCode.OK)] // trả về dữ liệu model cho FE
        public async Task<IActionResult> GetAll()
        {
            var data = await _DonGiaMTCQuery.GetAll();
            return Ok(new ApiSuccessResult<List<SelectItem>>(data: data));
        }

        /// <summary>
        /// Danh sách đơn giá máy thi công có phân trang, tổng số , tìm kiếm
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(ApiSuccessResult<IList<DonGiaMTCResponse>>), (int)HttpStatusCode.OK)] // trả về dữ liệu model cho FE
        public async Task<IActionResult> GetListUser([FromQuery] DonGiaMTCRequest request)
        {
            var data = await _DonGiaMTCQuery.GetList(request);
            return Ok(new ApiSuccessResult<List<DonGiaMTCResponse>>
            {
                Data = data
            });
        }

        /// <summary>
        /// Tạo mới đơn giá máy thi công
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(ApiSuccessResult<bool>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Create([FromBody] CreateDonGiaMTCCommand command)
        {
            var user = await _mediator.Send(command);
            return Ok(new ApiSuccessResult<bool>(data: user, message: string.Format(Resources.MSG_CREATE_SUCCESS, "đơn giá máy thi công")));
        }

        /// <summary>
        /// Sửa đơn giá máy thi công
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(typeof(ApiSuccessResult<bool>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Update([FromBody] UpdateDonGiaMTCCommand command)
        {
            var user = await _mediator.Send(command);
            return Ok(new ApiSuccessResult<bool>(data: user, message: string.Format(Resources.MSG_UPDATE_SUCCESS, "đơn giá máy thi công")));
        }

        /// <summary>
        /// Xoá đơn giá máy thi công
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(ApiSuccessResult<bool>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var user = await _mediator.Send(new DeleteDonGiaMTCCommand(id));
            return Ok(new ApiSuccessResult<bool>(data: user, message: string.Format(Resources.MSG_DELETE_SUCCESS, "đơn giá máy thi công")));
        }
    }
}
