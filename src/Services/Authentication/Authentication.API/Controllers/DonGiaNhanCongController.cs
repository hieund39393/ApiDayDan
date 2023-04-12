using Authentication.Application.Commands.DonGiaNhanCongCommand;
using Authentication.Application.Model.DonGiaNhanCong;
using Authentication.Application.Queries.DonGiaNhanCongQuery;
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
    public class DonGiaNhanCongController : ControllerBase
    {
        private readonly IDonGiaNhanCongQuery _DonGiaNhanCongQuery; //kế thừa interface
        private readonly IMediator _mediator; //kế thừa để sử dụng command

        public DonGiaNhanCongController(IDonGiaNhanCongQuery DonGiaNhanCongQuery, IMediator mediator)
        {
            _DonGiaNhanCongQuery = DonGiaNhanCongQuery;
            _mediator = mediator;
        }

        ///// <summary>
        ///// Danh sách tất cả đơn giá nhân công
        ///// </summary>
        ///// <returns></returns>
        //[HttpGet("get-all")]
        //[ProducesResponseType(typeof(ApiSuccessResult<List<SelectItem>>), (int)HttpStatusCode.OK)] // trả về dữ liệu model cho FE
        //public async Task<IActionResult> GetAll()
        //{
        //    var data = await _DonGiaNhanCongQuery.GetAll();
        //    return Ok(new ApiSuccessResult<List<SelectItem>>(data: data));
        //}

        /// <summary>
        /// Danh sách đơn giá nhân công có phân trang, tổng số , tìm kiếm
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(ApiSuccessResult<IList<DonGiaNhanCongResponse>>), (int)HttpStatusCode.OK)] // trả về dữ liệu model cho FE
        public async Task<IActionResult> GetListUser([FromQuery] DonGiaNhanCongRequest request)
        {
            var data = await _DonGiaNhanCongQuery.GetList(request);
            return Ok(new ApiSuccessResult<IList<DonGiaNhanCongResponse>>
            {
                Data = data.Data,
                Paging = data.Paging
            });
        }

        /// <summary>
        /// Tạo mới đơn giá nhân công
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(ApiSuccessResult<bool>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Create([FromBody] CreateDonGiaNhanCongCommand command)
        {
            var user = await _mediator.Send(command);
            return Ok(new ApiSuccessResult<bool>(data: user, message: string.Format(Resources.MSG_CREATE_SUCCESS, "đơn giá nhân công")));
        }

        /// <summary>
        /// Sửa đơn giá nhân công
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(typeof(ApiSuccessResult<bool>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Update([FromBody] UpdateDonGiaNhanCongCommand command)
        {
            var user = await _mediator.Send(command);
            return Ok(new ApiSuccessResult<bool>(data: user, message: string.Format(Resources.MSG_UPDATE_SUCCESS, "đơn giá nhân công")));
        }

        /// <summary>
        /// Xoá đơn giá nhân công
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(ApiSuccessResult<bool>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var user = await _mediator.Send(new DeleteDonGiaNhanCongCommand(id));
            return Ok(new ApiSuccessResult<bool>(data: user, message: string.Format(Resources.MSG_DELETE_SUCCESS, "đơn giá nhân công")));
        }
    }
}
