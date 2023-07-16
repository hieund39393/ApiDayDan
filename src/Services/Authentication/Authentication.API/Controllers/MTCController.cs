using Authentication.Application.Commands.DM_MTCCommand;
using Authentication.Application.Model.DM_MTC;
using Authentication.Application.Queries.DM_MTCQuery;
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
    public class MTCController : ControllerBase
    {
        private readonly IDM_MTCQuery _dM_MTCQuery; //kế thừa interface
        private readonly IMediator _mediator; //kế thừa để sử dụng command

        public MTCController(IDM_MTCQuery bieuGiaQuery, IMediator mediator)
        {
            _dM_MTCQuery = bieuGiaQuery;
            _mediator = mediator;
        }

        /// <summary>
        /// Danh sách tất cả danh mục máy thi công
        /// </summary>
        /// <returns></returns>
        [HttpGet("get-all")]
        [ProducesResponseType(typeof(ApiSuccessResult<List<SelectItem>>), (int)HttpStatusCode.OK)] // trả về dữ liệu model cho FE
        public async Task<IActionResult> GetAll()
        {
            var data = await _dM_MTCQuery.GetAll();
            return Ok(new ApiSuccessResult<List<SelectItem>>(data: data));
        }

        /// <summary>
        /// Danh sách danh mục máy thi công có phân trang, tổng số , tìm kiếm
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(ApiSuccessResult<IList<DM_MTCResponse>>), (int)HttpStatusCode.OK)] // trả về dữ liệu model cho FE
        public async Task<IActionResult> GetListUser([FromQuery] DM_MTCRequest request)
        {
            var data = await _dM_MTCQuery.GetList(request);
            return Ok(new ApiSuccessResult<IList<DM_MTCResponse>>
            {
                Data = data.Data,
                Paging = data.Paging
            });
        }

        /// <summary>
        /// Tạo mới danh mục máy thi công
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(ApiSuccessResult<bool>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Create([FromBody] CreateDM_MTCCommand command)
        {
            var user = await _mediator.Send(command);
            return Ok(new ApiSuccessResult<bool>(data: user, message: string.Format(Resources.MSG_CREATE_SUCCESS, "danh mục máy thi công")));
        }

        /// <summary>
        /// Sửa danh mục máy thi công
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(typeof(ApiSuccessResult<bool>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Update([FromBody] UpdateDM_MTCCommand command)
        {
            var user = await _mediator.Send(command);
            return Ok(new ApiSuccessResult<bool>(data: user, message: string.Format(Resources.MSG_UPDATE_SUCCESS, "danh mục máy thi công")));
        }

        /// <summary>
        /// Xoá danh mục máy thi công
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(ApiSuccessResult<bool>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var user = await _mediator.Send(new DeleteDM_MTCCommand(id));
            return Ok(new ApiSuccessResult<bool>(data: user, message: string.Format(Resources.MSG_DELETE_SUCCESS, "danh mục máy thi công")));
        }
    }
}
