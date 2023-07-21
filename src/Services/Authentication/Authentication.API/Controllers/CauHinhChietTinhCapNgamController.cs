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
using System.Linq;
using Authentication.Application.Queries.CommonQuery;

namespace Authentication.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CauHinhChietTinhCapNgamController : ControllerBase
    {
        private readonly ICauHinhChietTinh_CapNgamQuery _cauHinhChietTinh_CapNgamQuery; //kế thừa interface
        private readonly IMediator _mediator; //kế thừa để sử dụng command
        private readonly ICommonQuery _commonQuery;
        public CauHinhChietTinhCapNgamController(ICauHinhChietTinh_CapNgamQuery cauHinhChietTinh_CapNgamQuery, IMediator mediator, ICommonQuery commonQuery)
        {
            _cauHinhChietTinh_CapNgamQuery = cauHinhChietTinh_CapNgamQuery;
            _mediator = mediator;
            _commonQuery = commonQuery;
        }

        /// <summary>
        /// Danh sách   biểu giá có phân trang, tổng số , tìm kiếm
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(ApiSuccessResult<IList<CauHinhChietTinhResponse>>), (int)HttpStatusCode.OK)] // trả về dữ liệu model cho FE
        public async Task<IActionResult> GetListUser([FromQuery] CauHinhChietTinhRequest request)
        {
            var listVungKhuVuc = _commonQuery.ListVungKhuVuc();
            var data = await _cauHinhChietTinh_CapNgamQuery.GetList(request);
            foreach (var item in data.Data.ToList())
            {
                item.TenVungKhuVuc = listVungKhuVuc.FirstOrDefault(x => x.Value == item.VungKhuVuc.ToString())?.Name;
            }
            return Ok(new ApiSuccessResult<IList<CauHinhChietTinhResponse>>
            {
                Data = data.Data,
                Paging = data.Paging
            });
        }

        /// <summary>
        /// Tạo mới  biểu giá
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(ApiSuccessResult<bool>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Create([FromBody] CreateCauHinhChietTinh_CapNgamCommand command)
        {
            var user = await _mediator.Send(command);
            return Ok(new ApiSuccessResult<bool>(data: user, message: string.Format(Resources.MSG_CREATE_SUCCESS, " cấu hình")));
        }

        /// <summary>
        /// Sửa  biểu giá
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(typeof(ApiSuccessResult<bool>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Update([FromBody] UpdateCauHinhChietTinh_CapNgamCommand command)
        {
            var user = await _mediator.Send(command);
            return Ok(new ApiSuccessResult<bool>(data: user, message: string.Format(Resources.MSG_UPDATE_SUCCESS, " cấu hình")));
        }

        /// <summary>
        /// Xoá  biểu giá
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(ApiSuccessResult<bool>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Update([FromRoute] Guid id)
        {
            var user = await _mediator.Send(new DeleteCauHinhChietTinhCommand(id));
            return Ok(new ApiSuccessResult<bool>(data: user, message: string.Format(Resources.MSG_DELETE_SUCCESS, " cấu hình")));
        }

        [HttpGet("get-vat-lieu-by-id")]
        [ProducesResponseType(typeof(ApiSuccessResult<List<SelectItem>>), (int)HttpStatusCode.OK)] // trả về dữ liệu model cho FE
        public async Task<IActionResult> GetVatLieu([FromQuery] GetByIdAndPhanLoaiRequest request)
        {
            var data = await _cauHinhChietTinh_CapNgamQuery.GetVatLieuById(request);
            return Ok(new ApiSuccessResult<List<Guid>>(data: data));
        }
        [HttpGet("get-nhan-cong-by-id")]
        [ProducesResponseType(typeof(ApiSuccessResult<List<SelectItem>>), (int)HttpStatusCode.OK)] // trả về dữ liệu model cho FE
        public async Task<IActionResult> GetNhanCong([FromQuery] GetByIdAndPhanLoaiRequest request)
        {
            var data = await _cauHinhChietTinh_CapNgamQuery.GetNhanCongById(request);
            return Ok(new ApiSuccessResult<List<Guid>>(data: data));
        }
        [HttpGet("get-mtc-by-id")]
        [ProducesResponseType(typeof(ApiSuccessResult<List<SelectItem>>), (int)HttpStatusCode.OK)] // trả về dữ liệu model cho FE
        public async Task<IActionResult> GetMTC([FromQuery] GetByIdAndPhanLoaiRequest request)
        {
            var data = await _cauHinhChietTinh_CapNgamQuery.GetMTCById(request);
            return Ok(new ApiSuccessResult<List<Guid>>(data: data));
        }
    }
}
