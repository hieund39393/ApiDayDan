using Authentication.Application.Commands.DonGiaNhanCong_CapNgamCommand;
using Authentication.Application.Queries.DonGiaNhanCong_CapNgamQuery;
using Authentication.Infrastructure.Properties;
using EVN.Core.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using System;
using Authentication.Application.Model.DonGiaNhanCong;

namespace Authentication.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DonGiaNhanCongCapNgamController : ControllerBase
    {
        private readonly IDonGiaNhanCong_CapNgamQuery _DonGiaNhanCong_CapNgamQuery; //kế thừa interface
        private readonly IMediator _mediator; //kế thừa để sử dụng command

        public DonGiaNhanCongCapNgamController(IDonGiaNhanCong_CapNgamQuery DonGiaNhanCong_CapNgamQuery, IMediator mediator)
        {
            _DonGiaNhanCong_CapNgamQuery = DonGiaNhanCong_CapNgamQuery;
            _mediator = mediator;
        }


        [HttpGet("get-all")]
        [ProducesResponseType(typeof(ApiSuccessResult<IList<DonGiaNhanCongResponse>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAll()
        {
            var data = await _DonGiaNhanCong_CapNgamQuery.GetAll();
            return Ok(new ApiSuccessResult<List<SelectItem>>(data: data));
        }

        /// <summary>
        /// Danh sách đơn giá nhân công cáp ngầm có phân trang, tổng số , tìm kiếm
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(ApiSuccessResult<IList<DonGiaNhanCongResponse>>), (int)HttpStatusCode.OK)] // trả về dữ liệu model cho FE
        public async Task<IActionResult> GetListUser([FromQuery] DonGiaNhanCongRequest request)
        {
            var data = await _DonGiaNhanCong_CapNgamQuery.GetList(request);
            return Ok(new ApiSuccessResult<IList<DonGiaNhanCongResponse>>
            {
                Data = data.Data,
                Paging = data.Paging
            });
        }

        /// <summary>
        /// Tạo mới đơn giá nhân công cáp ngầm
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(ApiSuccessResult<bool>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Create([FromBody] CreateDonGiaNhanCong_CapNgamCommand command)
        {
            var user = await _mediator.Send(command);
            return Ok(new ApiSuccessResult<bool>(data: user, message: string.Format(Resources.MSG_CREATE_SUCCESS, "đơn giá nhân công cáp ngầm")));
        }

        /// <summary>
        /// Sửa đơn giá nhân công cáp ngầm
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(typeof(ApiSuccessResult<bool>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Update([FromBody] UpdateDonGiaNhanCong_CapNgamCommand command)
        {
            var user = await _mediator.Send(command);
            return Ok(new ApiSuccessResult<bool>(data: user, message: string.Format(Resources.MSG_UPDATE_SUCCESS, "đơn giá nhân công cáp ngầm")));
        }

        /// <summary>
        /// Xoá đơn giá nhân công cáp ngầm
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(ApiSuccessResult<bool>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var user = await _mediator.Send(new DeleteDonGiaNhanCong_CapNgamCommand(id));
            return Ok(new ApiSuccessResult<bool>(data: user, message: string.Format(Resources.MSG_DELETE_SUCCESS, "đơn giá nhân công cáp ngầm")));
        }
    }
}
