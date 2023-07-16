using Authentication.Infrastructure.Properties;
using EVN.Core.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using System;
using Authentication.Application.Queries.CauHinhChietTinhQuery;
using Authentication.Application.Commands.CauHinhChietTinhCommand;
using Authentication.Application.Model.CauHinhChietTinh;
using Authentication.Application.Queries.DM_BieuGiaQuery;

namespace Authentication.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CauHinhChietTinhController : ControllerBase
    {
        private readonly ICauHinhChietTinhQuery _CauHinhChietTinhQuery; //kế thừa interface
        private readonly IMediator _mediator; //kế thừa để sử dụng command

        public CauHinhChietTinhController(ICauHinhChietTinhQuery bieuGiaQuery, IMediator mediator)
        {
            _CauHinhChietTinhQuery = bieuGiaQuery;
            _mediator = mediator;
        }

        /// <summary>
        /// Danh sách   biểu giá công việc có phân trang, tổng số , tìm kiếm
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(ApiSuccessResult<IList<CauHinhChietTinhResponse>>), (int)HttpStatusCode.OK)] // trả về dữ liệu model cho FE
        public async Task<IActionResult> GetListUser([FromQuery] CauHinhChietTinhRequest request)
        {
            var data = await _CauHinhChietTinhQuery.GetList(request);
            return Ok(new ApiSuccessResult<IList<CauHinhChietTinhResponse>>
            {
                Data = data.Data,
                Paging = data.Paging
            });
        }

        /// <summary>
        /// Tạo mới  biểu giá công việc
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(ApiSuccessResult<bool>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Create([FromBody] CreateCauHinhChietTinhCommand command)
        {
            var user = await _mediator.Send(command);
            return Ok(new ApiSuccessResult<bool>(data: user, message: string.Format(Resources.MSG_CREATE_SUCCESS, " biểu giá công việc")));
        }

        /// <summary>
        /// Sửa  biểu giá công việc
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(typeof(ApiSuccessResult<bool>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Update([FromBody] UpdateCauHinhChietTinhCommand command)
        {
            var user = await _mediator.Send(command);
            return Ok(new ApiSuccessResult<bool>(data: user, message: string.Format(Resources.MSG_UPDATE_SUCCESS, " biểu giá công việc")));
        }

        /// <summary>
        /// Xoá  biểu giá công việc
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(ApiSuccessResult<bool>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Update([FromRoute] Guid id)
        {
            var user = await _mediator.Send(new DeleteCauHinhChietTinhCommand(id));
            return Ok(new ApiSuccessResult<bool>(data: user, message: string.Format(Resources.MSG_DELETE_SUCCESS, " biểu giá công việc")));
        }

        [HttpGet("get-vat-lieu-by-id")]
        [ProducesResponseType(typeof(ApiSuccessResult<List<SelectItem>>), (int)HttpStatusCode.OK)] // trả về dữ liệu model cho FE
        public async Task<IActionResult> GetVatLieu([FromQuery] GetByIdAndPhanLoaiRequest request)
        {
            var data = await _CauHinhChietTinhQuery.GetVatLieuById(request);
            return Ok(new ApiSuccessResult<List<Guid>>(data: data));
        }
        [HttpGet("get-nhan-cong-by-id")]
        [ProducesResponseType(typeof(ApiSuccessResult<List<SelectItem>>), (int)HttpStatusCode.OK)] // trả về dữ liệu model cho FE
        public async Task<IActionResult> GetNhanCong([FromQuery] GetByIdAndPhanLoaiRequest request)
        {
            var data = await _CauHinhChietTinhQuery.GetNhanCongById(request);
            return Ok(new ApiSuccessResult<List<Guid>>(data: data));
        }
        [HttpGet("get-mtc-by-id")]
        [ProducesResponseType(typeof(ApiSuccessResult<List<SelectItem>>), (int)HttpStatusCode.OK)] // trả về dữ liệu model cho FE
        public async Task<IActionResult> GetMTC([FromQuery] GetByIdAndPhanLoaiRequest request)
        {
            var data = await _CauHinhChietTinhQuery.GetMTCById(request);
            return Ok(new ApiSuccessResult<List<Guid>>(data: data));
        }
    }
}
