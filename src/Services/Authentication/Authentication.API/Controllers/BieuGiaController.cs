using Authentication.Application.Commands.DM_BieuGiaCommand;
using Authentication.Application.Commands.DM_LoaiBieuGiaCommand;
using Authentication.Application.Model.DM_BieuGia;
using Authentication.Application.Model.DM_LoaiBieuGia;
using Authentication.Application.Queries.DM_BieuGiaQuery;
using Authentication.Application.Queries.DM_LoaiBieuGiaQuery;
using Authentication.Infrastructure.Properties;
using EVN.Core.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Authentication.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class BieuGiaController : ControllerBase
    {
        private readonly IDM_BieuGiaQuery _bieuGiaQuery; //kế thừa interface
        private readonly IMediator _mediator; //kế thừa để sử dụng command

        public BieuGiaController(IMediator mediator, IDM_BieuGiaQuery bieuGiaQuery)
        {
            _mediator = mediator;
            _bieuGiaQuery = bieuGiaQuery;
        }

        /// <summary>
        /// Danh sách tất cả loại biểu giá
        /// </summary>
        /// <returns></returns>
        [HttpGet("get-all")]
        [ProducesResponseType(typeof(ApiSuccessResult<List<SelectItem>>), (int)HttpStatusCode.OK)] // trả về dữ liệu model cho FE
        public async Task<IActionResult> GetAll()
        {
            var data = await _bieuGiaQuery.GetAll();
            return Ok(new ApiSuccessResult<List<SelectItem>>(data: data));
        }

        /// <summary>
        /// Danh sách  loại biểu giá có phân trang, tổng số , tìm kiếm
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(ApiSuccessResult<IList<DM_BieuGiaResponse>>), (int)HttpStatusCode.OK)] // trả về dữ liệu model cho FE
        public async Task<IActionResult> GetListUser([FromQuery] DM_BieuGiaRequest request)
        {
            var data = await _bieuGiaQuery.GetList(request);
            return Ok(new ApiSuccessResult<IList<DM_BieuGiaResponse>>
            {
                Data = data.Data,
                Paging = data.Paging
            });
        }

        /// <summary>
        /// Thêm mới biểu giá
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(ApiSuccessResult<bool>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Create([FromBody] Create_DMBieuGiaCommand command)
        {
            var user = await _mediator.Send(command);
            return Ok(new ApiSuccessResult<bool>(data: user, message: string.Format(Resources.MSG_CREATE_SUCCESS, "Biểu giá")));
        }

        /// <summary>
        /// Sửa biểu giá
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(typeof(ApiSuccessResult<bool>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Update([FromBody] Update_DMBieuGiaCommand command)
        {
            var user = await _mediator.Send(command);
            return Ok(new ApiSuccessResult<bool>(data: user, message: string.Format(Resources.MSG_UPDATE_SUCCESS, "biểu giá")));
        }

        /// <summary>
        /// Xoá loại biểu giá
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(ApiSuccessResult<bool>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var data = await _mediator.Send(new Delete_DMBieuGiaCommand(id));
            return Ok(new ApiSuccessResult<bool>(data: data, message: string.Format(Resources.MSG_DELETE_SUCCESS, "biểu giá")));
        }
    }
}
