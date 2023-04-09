using Authentication.Application.Commands.BieuGiaCongViecCommand;
using Authentication.Infrastructure.Properties;
using EVN.Core.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using System;
using Authentication.Application.Queries.BieuGiaCongViecQuery;
using Authentication.Application.Model.BieuGiaCongViec;

namespace Authentication.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BieuGiaCongViecController : ControllerBase
    {
        private readonly IBieuGiaCongViecQuery _bieuGiaCongViecQuery; //kế thừa interface
        private readonly IMediator _mediator; //kế thừa để sử dụng command

        public BieuGiaCongViecController(IBieuGiaCongViecQuery bieuGiaQuery, IMediator mediator)
        {
            _bieuGiaCongViecQuery = bieuGiaQuery;
            _mediator = mediator;
        }

        /// <summary>
        /// Danh sách   biểu giá công việc có phân trang, tổng số , tìm kiếm
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(ApiSuccessResult<IList<BieuGiaCongViecResponse>>), (int)HttpStatusCode.OK)] // trả về dữ liệu model cho FE
        public async Task<IActionResult> GetListUser([FromQuery] BieuGiaCongViecRequest request)
        {
            var data = await _bieuGiaCongViecQuery.GetList(request);
            return Ok(new ApiSuccessResult<IList<BieuGiaCongViecResponse>>
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
        public async Task<IActionResult> Create([FromBody] CreateBieuGiaCongViecCommand command)
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
        public async Task<IActionResult> Update([FromBody] UpdateBieuGiaCongViecCommand command)
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
            var user = await _mediator.Send(new DeleteBieuGiaCongViecCommand(id));
            return Ok(new ApiSuccessResult<bool>(data: user, message: string.Format(Resources.MSG_DELETE_SUCCESS, " biểu giá công việc")));
        }
    }
}
