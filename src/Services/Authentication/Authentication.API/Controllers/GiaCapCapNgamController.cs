using Authentication.Infrastructure.Properties;
using EVN.Core.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using System;
using Authentication.Application.Commands.GiaCapCommand;
using Authentication.Application.Model.GiaCap;
using Authentication.Application.Queries.GiaCapQuery;
using Authentication.Application.Queries.CommonQuery;
using System.Linq;

namespace Authentication.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GiaCapCapNgamController : ControllerBase
    {
        private readonly IGiaCap_CapNgamQuery _GiaCap_CapNgamQuery; //kế thừa interface
        private readonly ICommonQuery _commonQuery; //kế thừa interface
        private readonly IMediator _mediator; //kế thừa để sử dụng command

        public GiaCapCapNgamController(IGiaCap_CapNgamQuery GiaCap_CapNgamQuery, IMediator mediator, ICommonQuery commonQuery)
        {
            _GiaCap_CapNgamQuery = GiaCap_CapNgamQuery;
            _commonQuery = commonQuery;
            _mediator = mediator;
        }

        /// <summary>
        /// Danh sách tất cả giá cáp
        /// </summary>
        /// <returns></returns>
        [HttpGet("get-all")]
        [ProducesResponseType(typeof(ApiSuccessResult<List<SelectItem>>), (int)HttpStatusCode.OK)] // trả về dữ liệu model cho FE
        public async Task<IActionResult> GetAll()
        {
            var data = await _GiaCap_CapNgamQuery.GetAll();
            return Ok(new ApiSuccessResult<List<SelectItem>>(data: data));
        }

        /// <summary>
        /// Danh sách giá cáp có phân trang, tổng số , tìm kiếm
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(ApiSuccessResult<IList<GiaCapResponse>>), (int)HttpStatusCode.OK)] // trả về dữ liệu model cho FE
        public async Task<IActionResult> GetListUser([FromQuery] GiaCapRequest request)
        {
            var listVungKhuVuc = _commonQuery.ListVungKhuVuc();

            var data = await _GiaCap_CapNgamQuery.GetList(request);
           
            foreach(var item in data.Data.ToList())
            {
                item.TenVungKhuVuc = listVungKhuVuc.FirstOrDefault(x=>x.Value == item.VungKhuVuc.ToString())?.Name;
            }
            return Ok(new ApiSuccessResult<IList<GiaCapResponse>>
            {
                Data = data.Data,
                Paging = data.Paging
            });
        }

        /// <summary>
        /// Tạo mới giá cáp
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(ApiSuccessResult<bool>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Create([FromBody] CreateGiaCap_CapNgamCommand command)
        {
            var user = await _mediator.Send(command);
            return Ok(new ApiSuccessResult<bool>(data: user, message: string.Format(Resources.MSG_CREATE_SUCCESS, "giá cáp")));
        }

        /// <summary>
        /// Sửa giá cáp
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(typeof(ApiSuccessResult<bool>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Update([FromBody] UpdateGiaCap_CapNgamCommand command)
        {
            var user = await _mediator.Send(command);
            return Ok(new ApiSuccessResult<bool>(data: user, message: string.Format(Resources.MSG_UPDATE_SUCCESS, "giá cáp")));
        }

        /// <summary>
        /// Xoá giá cáp
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(ApiSuccessResult<bool>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var user = await _mediator.Send(new DeleteGiaCap_CapNgamCommand(id));
            return Ok(new ApiSuccessResult<bool>(data: user, message: string.Format(Resources.MSG_DELETE_SUCCESS, "giá cáp")));
        }
    }
}
