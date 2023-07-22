using Authentication.Application.Commands.ChiTietBieuGiaCommand;
using Authentication.Application.Model.ChiTietBieuGia;
using Authentication.Application.Model.DM_KhuVuc;
using Authentication.Application.Model.Menu;
using Authentication.Application.Queries.ChiTietBieuGia_CapNgamQuery;
using Authentication.Application.Queries.ChiTietBieuGiaQuery;
using Authentication.Application.Queries.DM_KhuVucQuery;
using Authentication.Infrastructure.Properties;
using EVN.Core.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Authentication.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChiTietBieuGiaCapNgamController : ControllerBase
    {
        private readonly IChiTietBieuGia_CapNgamQuery _ChiTietBieuGia_CapNgamQuery; //kế thừa interface
        private readonly IMediator _mediator; //kế thừa để sử dụng command

        public ChiTietBieuGiaCapNgamController(IChiTietBieuGia_CapNgamQuery bieuGiaQuery, IMediator mediator)
        {
            _ChiTietBieuGia_CapNgamQuery = bieuGiaQuery;
            _mediator = mediator;
        }


        /// <summary>
        /// lấy danh sách biểu giá theo loại biểu giá
        /// </summary>
        /// <param name="loaibieugia"></param>
        /// <returns></returns>
        [HttpGet("get-bieu-gia")]
        [ProducesResponseType(typeof(ApiSuccessResult<IList<SelectItem>>), (int)HttpStatusCode.OK)] // trả về dữ liệu model cho FE
        public async Task<IActionResult> GetBieuGiaByLoaiBieuGia([FromQuery] Guid loaibieugia)
        {
            var data = await _ChiTietBieuGia_CapNgamQuery.GetBieuGiaByLoaiBieuGia(loaibieugia);
            return Ok(new ApiSuccessResult<List<SelectItem>>(data: data));
        }

        /// <summary>
        /// Danh sách chi tiết biểu giá có phân trang, tổng số , tìm kiếm
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(ApiSuccessResult<IList<ChiTietBieuGiaResponse>>), (int)HttpStatusCode.OK)] // trả về dữ liệu model cho FE
        public async Task<IActionResult> GetList([FromQuery] GetListChiTietBieuGia_CapNgamCommand request)
        {
            var data = await _mediator.Send(request);
            return Ok(new ApiSuccessResult<ChiTietBieuGiaResult>
            {
                Data = data
            });
        }
        [HttpPut]
        [ProducesResponseType(typeof(ApiSuccessResult<bool>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Update([FromBody] UpdateChiTietBieuGia_CapNgamCommand command)
        {
            var data = await _mediator.Send(command);
            return Ok(new ApiSuccessResult<bool>(data: data, message: string.Format(Resources.MSG_UPDATE_SUCCESS, "chi tiết biểu giá")));
        }
        [HttpPost("dong-bo")]
        [ProducesResponseType(typeof(ApiSuccessResult<bool>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DongBo([FromBody] SyncChiTietBieuGia_CapNgamCommand command)
        {
            var data = await _mediator.Send(command);
            return Ok(new ApiSuccessResult<bool>(data: data, message: string.Format("Đồng bộ chi tiết biểu giá thành công")));
        }
        [HttpGet("check-data")]
        public async Task<IActionResult> CheckData([FromQuery] ChiTietBieuGiaRequest request)
        {
            var data = await _ChiTietBieuGia_CapNgamQuery.KiemTraDuLieu(request);
            return Ok(new ApiSuccessResult<int>
            {
                Data = data
            });
        }
        [HttpGet("get-don-gia")]
        public async Task<IActionResult> GetDonGia([FromQuery] GetDonGiaRequest request)
        {
            var data = await _ChiTietBieuGia_CapNgamQuery.GetDonGia(request);
            return Ok(new ApiSuccessResult<List<GetDonGiaResponse>>
            {
                Data = data
            });
        }
    }
}
