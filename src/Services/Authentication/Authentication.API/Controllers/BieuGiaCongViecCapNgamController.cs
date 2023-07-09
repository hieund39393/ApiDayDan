using Authentication.Application.Commands.BieuGiaCongViec_CapNgamCommand;
using Authentication.Infrastructure.Properties;
using EVN.Core.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using System;
using Authentication.Application.Queries.BieuGiaCongViec_CapNgamQuery;
using Authentication.Application.Model.BieuGiaCongViec;
using System.Net.WebSockets;
using System.Linq;

namespace Authentication.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BieuGiaCongViecCapNgamController : ControllerBase
    {
        private readonly IBieuGiaCongViec_CapNgamQuery _BieuGiaCongViec_CapNgamQuery; //kế thừa interface
        private readonly IMediator _mediator; //kế thừa để sử dụng command

        public BieuGiaCongViecCapNgamController(IBieuGiaCongViec_CapNgamQuery bieuGiaQuery, IMediator mediator)
        {
            _BieuGiaCongViec_CapNgamQuery = bieuGiaQuery;
            _mediator = mediator;
        }

        /// <summary>
        /// Danh sách   biểu giá công việc cáp ngầm có phân trang, tổng số , tìm kiếm
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(ApiSuccessResult<IList<BieuGiaCongViecResponse>>), (int)HttpStatusCode.OK)] // trả về dữ liệu model cho FE
        public async Task<IActionResult> GetList([FromQuery] BieuGiaCongViecRequest request)
        {
            var data = await _BieuGiaCongViec_CapNgamQuery.GetList(request);
            foreach (var item in data.Data)
            {
                item.TenPhanLoai = _BieuGiaCongViec_CapNgamQuery.PhanLoai()
                    .FirstOrDefault(x => x.Value == item.PhanLoai.ToString())?.Name;
            }

            return Ok(new ApiSuccessResult<IList<BieuGiaCongViecResponse>>
            {
                Data = data.Data,
                Paging = data.Paging
            });
        }

        /// <summary>
        /// Tạo mới  biểu giá công việc cáp ngầm
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(ApiSuccessResult<bool>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Create([FromBody] CreateBieuGiaCongViec_CapNgamCommand command)
        {
            var user = await _mediator.Send(command);
            return Ok(new ApiSuccessResult<bool>(data: user, message: string.Format(Resources.MSG_CREATE_SUCCESS, " biểu giá công việc cáp ngầm")));
        }

        /// <summary>
        /// Sửa  biểu giá công việc cáp ngầm
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(typeof(ApiSuccessResult<bool>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Update([FromBody] UpdateBieuGiaCongViec_CapNgamCommand command)
        {
            var user = await _mediator.Send(command);
            return Ok(new ApiSuccessResult<bool>(data: user, message: string.Format(Resources.MSG_UPDATE_SUCCESS, " biểu giá công việc cáp ngầm")));
        }

        /// <summary>
        /// Xoá  biểu giá công việc cáp ngầm
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(ApiSuccessResult<bool>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Update([FromRoute] Guid id)
        {
            var user = await _mediator.Send(new DeleteBieuGiaCongViec_CapNgamCommand(id));
            return Ok(new ApiSuccessResult<bool>(data: user, message: string.Format(Resources.MSG_DELETE_SUCCESS, " biểu giá công việc cáp ngầm")));
        }

        [HttpGet("phanloai")]
        public async Task<IActionResult> PhanLoai()
        {
            var data = _BieuGiaCongViec_CapNgamQuery.PhanLoai();
            return Ok(new ApiSuccessResult<List<SelectItem>>
            {
                Data = data,
            });
        }
    }
}
