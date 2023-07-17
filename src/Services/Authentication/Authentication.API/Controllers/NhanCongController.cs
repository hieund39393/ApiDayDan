using Authentication.Application.Queries.DM_NhanCongQuery;
using Authentication.Infrastructure.Properties;
using EVN.Core.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using System;
using Authentication.Application.Model.DonGiaNhanCong;
using Authentication.Application.Commands.DM_NhanCongCommand;

namespace Authentication.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NhanCongController : ControllerBase
    {
        private readonly IDM_NhanCongQuery _dM_NhanCongQuery; //kế thừa interface
        private readonly IMediator _mediator; //kế thừa để sử dụng command

        public NhanCongController(IDM_NhanCongQuery bieuGiaQuery, IMediator mediator)
        {
            _dM_NhanCongQuery = bieuGiaQuery;
            _mediator = mediator;
        }

        /// <summary>
        /// Danh sách tất cả danh mục nhân công
        /// </summary>
        /// <returns></returns>
        [HttpGet("get-all")]
        [ProducesResponseType(typeof(ApiSuccessResult<List<SelectItem>>), (int)HttpStatusCode.OK)] // trả về dữ liệu model cho FE
        public async Task<IActionResult> GetAll()
        {
            var data = await _dM_NhanCongQuery.GetAll();
            return Ok(new ApiSuccessResult<List<SelectItem>>(data: data));
        }

        /// <summary>
        /// Danh sách danh mục nhân công có phân trang, tổng số , tìm kiếm
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(ApiSuccessResult<IList<DM_NhanCongResponse>>), (int)HttpStatusCode.OK)] // trả về dữ liệu model cho FE
        public async Task<IActionResult> GetListUser([FromQuery] DM_NhanCongRequest request)
        {
            var data = await _dM_NhanCongQuery.GetList(request);
            return Ok(new ApiSuccessResult<IList<DM_NhanCongResponse>>
            {
                Data = data.Data,
                Paging = data.Paging
            });
        }

        /// <summary>
        /// Tạo mới danh mục nhân công
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(ApiSuccessResult<bool>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Create([FromBody] CreateDM_NhanCongCommand command)
        {
            var user = await _mediator.Send(command);
            return Ok(new ApiSuccessResult<bool>(data: user, message: string.Format(Resources.MSG_CREATE_SUCCESS, "danh mục nhân công")));
        }

        /// <summary>
        /// Sửa danh mục nhân công
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(typeof(ApiSuccessResult<bool>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Update([FromBody] UpdateDM_NhanCongCommand command)
        {
            var user = await _mediator.Send(command);
            return Ok(new ApiSuccessResult<bool>(data: user, message: string.Format(Resources.MSG_UPDATE_SUCCESS, "danh mục nhân công")));
        }

        /// <summary>
        /// Xoá danh mục nhân công
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(ApiSuccessResult<bool>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var user = await _mediator.Send(new DeleteDM_NhanCongCommand(id));
            return Ok(new ApiSuccessResult<bool>(data: user, message: string.Format(Resources.MSG_DELETE_SUCCESS, "danh mục nhân công")));
        }
    }
}
