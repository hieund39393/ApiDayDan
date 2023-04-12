using Authentication.Application.Commands.DonGiaChietTinhCommand;
using Authentication.Application.Model.DonGiaChietTinh;
using Authentication.Application.Queries.DonGiaChietTinhQuery;
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
    public class DonGiaChietTinhController : ControllerBase
    {
        private readonly IDonGiaChietTinhQuery _DonGiaChietTinhQuery; //kế thừa interface
        private readonly IMediator _mediator; //kế thừa để sử dụng command

        public DonGiaChietTinhController(IDonGiaChietTinhQuery DonGiaChietTinhQuery, IMediator mediator)
        {
            _DonGiaChietTinhQuery = DonGiaChietTinhQuery;
            _mediator = mediator;
        }

        ///// <summary>
        ///// Danh sách tất cả đơn giá chiết tinh
        ///// </summary>
        ///// <returns></returns>
        //[HttpGet("get-all")]
        //[ProducesResponseType(typeof(ApiSuccessResult<List<SelectItem>>), (int)HttpStatusCode.OK)] // trả về dữ liệu model cho FE
        //public async Task<IActionResult> GetAll()
        //{
        //    var data = await _DonGiaChietTinhQuery.GetAll();
        //    return Ok(new ApiSuccessResult<List<SelectItem>>(data: data));
        //}

        /// <summary>
        /// Danh sách đơn giá chiết tinh có phân trang, tổng số , tìm kiếm
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(ApiSuccessResult<IList<DonGiaChietTinhResponse>>), (int)HttpStatusCode.OK)] // trả về dữ liệu model cho FE
        public async Task<IActionResult> GetListUser([FromQuery] DonGiaChietTinhRequest request)
        {
            var data = await _DonGiaChietTinhQuery.GetList(request);
            return Ok(new ApiSuccessResult<IList<DonGiaChietTinhResponse>>
            {
                Data = data.Data,
                Paging = data.Paging
            });
        }

        /// <summary>
        /// Tạo mới đơn giá chiết tinh
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(ApiSuccessResult<bool>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Create([FromBody] CreateDonGiaChietTinhCommand command)
        {
            var user = await _mediator.Send(command);
            return Ok(new ApiSuccessResult<bool>(data: user, message: string.Format(Resources.MSG_CREATE_SUCCESS, "đơn giá chiết tinh")));
        }

        /// <summary>
        /// Sửa đơn giá chiết tinh
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(typeof(ApiSuccessResult<bool>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Update([FromBody] UpdateDonGiaChietTinhCommand command)
        {
            var user = await _mediator.Send(command);
            return Ok(new ApiSuccessResult<bool>(data: user, message: string.Format(Resources.MSG_UPDATE_SUCCESS, "đơn giá chiết tinh")));
        }

        /// <summary>
        /// Xoá đơn giá chiết tinh
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(ApiSuccessResult<bool>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var user = await _mediator.Send(new DeleteDonGiaChietTinhCommand(id));
            return Ok(new ApiSuccessResult<bool>(data: user, message: string.Format(Resources.MSG_DELETE_SUCCESS, "đơn giá chiết tinh")));
        }
    }
}
