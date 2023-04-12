using Authentication.Application.Commands.DM_VatLieuChietTinhCommand;
using Authentication.Application.Model.DM_VatLieuChietTinh;
using Authentication.Application.Queries.DM_VatLieuChietTinhQuery;
using Authentication.Infrastructure.Properties;
using EVN.Core.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using System;

namespace Authentication.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VatLieuChietTinhController : ControllerBase
    {
        private readonly IDM_VatLieuChietTinhQuery _dM_VatLieuChietTinhQuery; //kế thừa interface
        private readonly IMediator _mediator; //kế thừa để sử dụng command

        public VatLieuChietTinhController(IDM_VatLieuChietTinhQuery bieuGiaQuery, IMediator mediator)
        {
            _dM_VatLieuChietTinhQuery = bieuGiaQuery;
            _mediator = mediator;
        }

        /// <summary>
        /// Danh sách tất cả danh mục vật liệu chiết tinh
        /// </summary>
        /// <returns></returns>
        [HttpGet("get-all")]
        [ProducesResponseType(typeof(ApiSuccessResult<List<SelectItem>>), (int)HttpStatusCode.OK)] // trả về dữ liệu model cho FE
        public async Task<IActionResult> GetAll()
        {
            var data = await _dM_VatLieuChietTinhQuery.GetAll();
            return Ok(new ApiSuccessResult<List<SelectItem>>(data: data));
        }

        /// <summary>
        /// Danh sách danh mục vật liệu chiết tinh có phân trang, tổng số , tìm kiếm
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(ApiSuccessResult<IList<DM_VatLieuChietTinhResponse>>), (int)HttpStatusCode.OK)] // trả về dữ liệu model cho FE
        public async Task<IActionResult> GetListUser([FromQuery] DM_VatLieuChietTinhRequest request)
        {
            var data = await _dM_VatLieuChietTinhQuery.GetList(request);
            return Ok(new ApiSuccessResult<IList<DM_VatLieuChietTinhResponse>>
            {
                Data = data.Data,
                Paging = data.Paging
            });
        }

        /// <summary>
        /// Tạo mới danh mục vật liệu chiết tinh
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(ApiSuccessResult<bool>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Create([FromBody] CreateDM_VatLieuChietTinhCommand command)
        {
            var user = await _mediator.Send(command);
            return Ok(new ApiSuccessResult<bool>(data: user, message: string.Format(Resources.MSG_CREATE_SUCCESS, "danh mục vật liệu chiết tinh")));
        }

        /// <summary>
        /// Sửa danh mục vật liệu chiết tinh
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(typeof(ApiSuccessResult<bool>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Update([FromBody] UpdateDM_VatLieuChietTinhCommand command)
        {
            var user = await _mediator.Send(command);
            return Ok(new ApiSuccessResult<bool>(data: user, message: string.Format(Resources.MSG_UPDATE_SUCCESS, "danh mục vật liệu chiết tinh")));
        }

        /// <summary>
        /// Xoá danh mục vật liệu chiết tinh
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(ApiSuccessResult<bool>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var user = await _mediator.Send(new DeleteDM_VatLieuChietTinhCommand(id));
            return Ok(new ApiSuccessResult<bool>(data: user, message: string.Format(Resources.MSG_DELETE_SUCCESS, "danh mục vật liệu chiết tinh")));
        }
    }
}
