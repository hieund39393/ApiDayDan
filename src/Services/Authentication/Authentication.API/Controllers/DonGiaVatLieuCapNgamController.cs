using Authentication.Application.Commands.DonGiaVatLieu_CapNgamCommand;
using Authentication.Application.Queries.DonGiaVatLieu_CapNgamQuery;
using Authentication.Infrastructure.Properties;
using EVN.Core.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using System;
using Authentication.Application.Model.DonGiaVatLieu;
using Authentication.Application.Commands.DonGiaVatLieuCommand;

namespace Authentication.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DonGiaVatLieuCapNgamController : ControllerBase
    {
        private readonly IDonGiaVatLieu_CapNgamQuery _DonGiaVatLieu_CapNgamQuery; //kế thừa interface
        private readonly IMediator _mediator; //kế thừa để sử dụng command

        public DonGiaVatLieuCapNgamController(IDonGiaVatLieu_CapNgamQuery DonGiaVatLieu_CapNgamQuery, IMediator mediator)
        {
            _DonGiaVatLieu_CapNgamQuery = DonGiaVatLieu_CapNgamQuery;
            _mediator = mediator;
        }

        /// <summary>
        /// Danh sách tất cả đơn giá vật liệu cáp ngầm
        /// </summary>
        /// <returns></returns>
        [HttpGet("get-all")]
        [ProducesResponseType(typeof(ApiSuccessResult<List<SelectItem>>), (int)HttpStatusCode.OK)] // trả về dữ liệu model cho FE
        public async Task<IActionResult> GetAll()
        {
            var data = await _DonGiaVatLieu_CapNgamQuery.GetAll();
            return Ok(new ApiSuccessResult<List<SelectItem>>(data: data));
        }

        /// <summary>
        /// Danh sách đơn giá vật liệu cáp ngầm có phân trang, tổng số , tìm kiếm
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(ApiSuccessResult<IList<DonGiaVatLieuResponse>>), (int)HttpStatusCode.OK)] // trả về dữ liệu model cho FE
        public async Task<IActionResult> GetListUser([FromQuery] DonGiaVatLieuRequest request)
        {
            var data = await _DonGiaVatLieu_CapNgamQuery.GetList(request);
            return Ok(new ApiSuccessResult<IList<DonGiaVatLieuResponse>>
            {
                Data = data.Data,
                Paging = data.Paging
            });
        }

        /// <summary>
        /// Tạo mới đơn giá vật liệu cáp ngầm
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(ApiSuccessResult<bool>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Create([FromBody] CreateDonGiaVatLieu_CapNgamCommand command)
        {
            var user = await _mediator.Send(command);
            return Ok(new ApiSuccessResult<bool>(data: user, message: string.Format(Resources.MSG_CREATE_SUCCESS, "đơn giá vật liệu cáp ngầm")));
        }

        /// <summary>
        /// Sửa đơn giá vật liệu cáp ngầm
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(typeof(ApiSuccessResult<bool>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Update([FromBody] UpdateDonGiaVatLieu_CapNgamCommand command)
        {
            var user = await _mediator.Send(command);
            return Ok(new ApiSuccessResult<bool>(data: user, message: string.Format(Resources.MSG_UPDATE_SUCCESS, "đơn giá vật liệu cáp ngầm")));
        }

        /// <summary>
        /// Xoá đơn giá vật liệu cáp ngầm
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(ApiSuccessResult<bool>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var user = await _mediator.Send(new DeleteDonGiaVatLieu_CapNgamCommand(id));
            return Ok(new ApiSuccessResult<bool>(data: user, message: string.Format(Resources.MSG_DELETE_SUCCESS, "đơn giá vật liệu cáp ngầm")));
        }
    }
}
