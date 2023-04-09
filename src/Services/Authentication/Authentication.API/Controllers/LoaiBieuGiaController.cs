using Authentication.Application.Commands.DM_LoaiBieuGiaCommand;
using Authentication.Application.Commands.UserCommand;
using Authentication.Application.Model.DM_LoaiBieuGia;
using Authentication.Application.Model.Module;
using Authentication.Application.Model.User;
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
    public class LoaiBieuGiaController : ControllerBase
    {
        private readonly IDM_LoaiBieuGiaQuery _bieuGiaQuery; //kế thừa interface
        private readonly IMediator _mediator; //kế thừa để sử dụng command

        public LoaiBieuGiaController(IDM_LoaiBieuGiaQuery bieuGiaQuery, IMediator mediator)
        {
            _bieuGiaQuery = bieuGiaQuery;
            _mediator = mediator;
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
        [ProducesResponseType(typeof(ApiSuccessResult<IList<DM_LoaiBieuGiaResponse>>), (int)HttpStatusCode.OK)] // trả về dữ liệu model cho FE
        public async Task<IActionResult> GetListUser([FromQuery] DM_LoaiBieuGiaRequest request)
        {
            var data = await _bieuGiaQuery.GetList(request);
            return Ok(new ApiSuccessResult<IList<DM_LoaiBieuGiaResponse>>
            {
                Data = data.Data,
                Paging = data.Paging
            });
        }

        /// <summary>
        /// Tạo mới loại biểu giá
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(ApiSuccessResult<bool>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Create([FromBody] CreateDM_LoaiBieuGiaCommand command)
        {
            var user = await _mediator.Send(command);
            return Ok(new ApiSuccessResult<bool>(data: user, message: string.Format(Resources.MSG_CREATE_SUCCESS, "loại biểu giá")));
        }

        /// <summary>
        /// Sửa loại biểu giá
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(typeof(ApiSuccessResult<bool>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Update([FromBody] UpdateDM_LoaiBieuGiaCommand command)
        {
            var user = await _mediator.Send(command);
            return Ok(new ApiSuccessResult<bool>(data: user, message: string.Format(Resources.MSG_UPDATE_SUCCESS, "loại biểu giá")));
        }   
        
        /// <summary>
        /// Xoá loại biểu giá
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(ApiSuccessResult<bool>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var user = await _mediator.Send(new DeleteDM_LoaiBieuGiaCommand(id));
            return Ok(new ApiSuccessResult<bool>(data: user, message: string.Format(Resources.MSG_DELETE_SUCCESS, "loại biểu giá")));
        }


    }
}
