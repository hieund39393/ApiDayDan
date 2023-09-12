using Authentication.Application.Commands.DM_KhuVucCommand;
using Authentication.Application.Model.DM_KhuVuc;
using Authentication.Application.Queries.DM_KhuVucQuery;
using Authentication.Infrastructure.Properties;
using EVN.Core.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Authentication.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KhuVucController : ControllerBase
    {
        private readonly IDM_KhuVucQuery _khuVucQuery; //kế thừa interface
        private readonly IMediator _mediator; //kế thừa để sử dụng command

        public KhuVucController(IDM_KhuVucQuery khuVucQuery, IMediator mediator)
        {
            _khuVucQuery = khuVucQuery;
            _mediator = mediator;
        }

        /// <summary>
        /// Danh sách tất cả  khu vực
        /// </summary>
        /// <returns></returns>
        [HttpGet("get-all")]
        [ProducesResponseType(typeof(ApiSuccessResult<List<SelectItem>>), (int)HttpStatusCode.OK)] // trả về dữ liệu model cho FE
        public async Task<IActionResult> GetAll()
        {
            var data = await _khuVucQuery.GetAll();
            return Ok(new ApiSuccessResult<List<SelectItem>>(data: data));
        }
        /// <summary>
        /// Danh sách tất cả  khu vực
        /// </summary>
        /// <returns></returns>
        [HttpGet("get-all-cap-ngam")]
        [ProducesResponseType(typeof(ApiSuccessResult<List<SelectItem>>), (int)HttpStatusCode.OK)] // trả về dữ liệu model cho FE
        public async Task<IActionResult> GetAllCapNgam()
        {
            var data = await _khuVucQuery.GetAllCapNgam();
            return Ok(new ApiSuccessResult<List<SelectItem>>(data: data));
        }

        /// <summary>
        /// Danh sách   khu vực có phân trang, tổng số , tìm kiếm
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(ApiSuccessResult<IList<DM_KhuVucResponse>>), (int)HttpStatusCode.OK)] // trả về dữ liệu model cho FE
        public async Task<IActionResult> GetListUser([FromQuery] DM_KhuVucRequest request)
        {
            var data = await _khuVucQuery.GetList(request);
            return Ok(new ApiSuccessResult<IList<DM_KhuVucResponse>>
            {
                Data = data.Data,
                Paging = data.Paging
            });
        }

        /// <summary>
        /// Tạo mới  khu vực
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(ApiSuccessResult<bool>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Create([FromBody] CreateDM_KhuVucCommand command)
        {
            var user = await _mediator.Send(command);
            return Ok(new ApiSuccessResult<bool>(data: user, message: string.Format(Resources.MSG_CREATE_SUCCESS, " khu vực")));
        }

        /// <summary>
        /// Sửa  khu vực
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(typeof(ApiSuccessResult<bool>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Update([FromBody] UpdateDM_KhuVucCommand command)
        {
            var user = await _mediator.Send(command);
            return Ok(new ApiSuccessResult<bool>(data: user, message: string.Format(Resources.MSG_UPDATE_SUCCESS, " khu vực")));
        }

        /// <summary>
        /// Xoá  khu vực
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(ApiSuccessResult<bool>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var user = await _mediator.Send(new DeleteDM_KhuVucCommand(id));
            return Ok(new ApiSuccessResult<bool>(data: user, message: string.Format(Resources.MSG_DELETE_SUCCESS, " khu vực")));
        }
    }
}
