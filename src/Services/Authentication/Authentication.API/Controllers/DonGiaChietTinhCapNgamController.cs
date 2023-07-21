using Authentication.Application.Commands.DonGiaChietTinhCommand;
using Authentication.Application.Model.DonGiaChietTinh;
using Authentication.Application.Queries.DonGiaChietTinhQuery;
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
    public class DonGiaChietTinhCapNgamController : ControllerBase
    {
        private readonly IDonGiaChietTinhQuery _DonGiaChietTinhQuery; //kế thừa interface
        private readonly IMediator _mediator; //kế thừa để sử dụng command

        public DonGiaChietTinhCapNgamController(IDonGiaChietTinhQuery DonGiaChietTinhQuery, IMediator mediator)
        {
            _DonGiaChietTinhQuery = DonGiaChietTinhQuery;
            _mediator = mediator;
        }

        ///// <summary>
        ///// Danh sách tất cả đơn giá chiết tinh
        ///// </summary>
        ///// <returns></returns>
        //[HttpGet("get-all")]
        //[ProducesResponseType(typeof(ApiSuccessResult<List<SelectItem>>), (int)HttpStatusCode.OK)] // trả về dữ liệu model cho FE
        //public async Task<IActionResult> GetAll()
        //{
        //    var data = await _DonGiaChietTinhQuery.GetAll();
        //    return Ok(new ApiSuccessResult<List<SelectItem>>(data: data));
        //}

        /// <summary>
        /// Danh sách đơn giá chiết tinh có phân trang, tổng số , tìm kiếm
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(ApiSuccessResult<List<DonGiaChietTinhResponse>>), (int)HttpStatusCode.OK)] // trả về dữ liệu model cho FE
        public async Task<IActionResult> GetListUser([FromQuery] DonGiaChietTinhRequest request)
        {
            var data = await _DonGiaChietTinhQuery.GetList(request);
            return Ok(new ApiSuccessResult<List<DonGiaChietTinhResponse>>
            {
                Data = data,
            });
        }

       
        /// <summary>
        /// Sửa đơn giá chiết tinh
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(typeof(ApiSuccessResult<bool>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Update([FromBody] UpdateDonGiaChietTinhCommand command)
        {
            var user = await _mediator.Send(command);
            return Ok(new ApiSuccessResult<bool>(data: user, message: string.Format(Resources.MSG_UPDATE_SUCCESS, "đơn giá chiết tinh")));
        }

      
    }
}
