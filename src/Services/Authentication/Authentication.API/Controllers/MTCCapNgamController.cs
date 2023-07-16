using Authentication.Application.Commands.DM_MTC_CapNgamCommand;
using Authentication.Application.Queries.DM_MTC_CapNgamQuery;
using Authentication.Infrastructure.Properties;
using EVN.Core.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using System;
using Authentication.Application.Model.DM_MTC;

namespace Authentication.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MTCCapNgam : ControllerBase
    {
        private readonly IDM_MTC_CapNgamQuery _DM_MTC_CapNgamQuery; //kế thừa interface
        private readonly IMediator _mediator; //kế thừa để sử dụng command

        public MTCCapNgam(IDM_MTC_CapNgamQuery bieuGiaQuery, IMediator mediator)
        {
            _DM_MTC_CapNgamQuery = bieuGiaQuery;
            _mediator = mediator;
        }

        /// <summary>
        /// Danh sách tất cả danh mục máy thi công cáp ngầm
        /// </summary>
        /// <returns></returns>
        [HttpGet("get-all")]
        [ProducesResponseType(typeof(ApiSuccessResult<List<SelectItem>>), (int)HttpStatusCode.OK)] // trả về dữ liệu model cho FE
        public async Task<IActionResult> GetAll()
        {
            var data = await _DM_MTC_CapNgamQuery.GetAll();
            return Ok(new ApiSuccessResult<List<SelectItem>>(data: data));
        }

        /// <summary>
        /// Danh sách danh mục máy thi công cáp ngầm có phân trang, tổng số , tìm kiếm
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(ApiSuccessResult<IList<DM_MTCResponse>>), (int)HttpStatusCode.OK)] // trả về dữ liệu model cho FE
        public async Task<IActionResult> GetListUser([FromQuery] DM_MTCRequest request)
        {
            var data = await _DM_MTC_CapNgamQuery.GetList(request);
            return Ok(new ApiSuccessResult<IList<DM_MTCResponse>>
            {
                Data = data.Data,
                Paging = data.Paging
            });
        }

        /// <summary>
        /// Tạo mới danh mục máy thi công cáp ngầm
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(ApiSuccessResult<bool>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Create([FromBody] CreateDM_MTC_CapNgamCommand command)
        {
            var user = await _mediator.Send(command);
            return Ok(new ApiSuccessResult<bool>(data: user, message: string.Format(Resources.MSG_CREATE_SUCCESS, "danh mục máy thi công cáp ngầm")));
        }

        /// <summary>
        /// Sửa danh mục máy thi công cáp ngầm
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(typeof(ApiSuccessResult<bool>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Update([FromBody] UpdateDM_MTC_CapNgamCommand command)
        {
            var user = await _mediator.Send(command);
            return Ok(new ApiSuccessResult<bool>(data: user, message: string.Format(Resources.MSG_UPDATE_SUCCESS, "danh mục máy thi công cáp ngầm")));
        }

        /// <summary>
        /// Xoá danh mục máy thi công cáp ngầm
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(ApiSuccessResult<bool>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var user = await _mediator.Send(new DeleteDM_MTC_CapNgamCommand(id));
            return Ok(new ApiSuccessResult<bool>(data: user, message: string.Format(Resources.MSG_DELETE_SUCCESS, "danh mục máy thi công cáp ngầm")));
        }
    }
}
