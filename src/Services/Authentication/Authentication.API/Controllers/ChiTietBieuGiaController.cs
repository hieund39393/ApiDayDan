using Authentication.Application.Commands.ChiTietBieuGiaCommand;
using Authentication.Application.Model.ChiTietBieuGia;
using Authentication.Application.Model.Menu;
using Authentication.Application.Queries.ChiTietBieuGiaQuery;
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
    public class ChiTietBieuGiaController : ControllerBase
    {
        private readonly IChiTietBieuGiaQuery _ChiTietBieuGiaQuery; //kế thừa interface
        private readonly IMediator _mediator; //kế thừa để sử dụng command

        public ChiTietBieuGiaController(IChiTietBieuGiaQuery bieuGiaQuery, IMediator mediator)
        {
            _ChiTietBieuGiaQuery = bieuGiaQuery;
            _mediator = mediator;
        }

        /// <summary>
        /// lấy danh sách đơn giá theo phân loại
        /// </summary>
        /// <param name="IdPhanLoai"></param>
        /// <returns></returns>
        [HttpGet("get-don-gia-phan-loai")]
        [ProducesResponseType(typeof(ApiSuccessResult<IList<SelectItem>>), (int)HttpStatusCode.OK)] // trả về dữ liệu model cho FE
        public async Task<IActionResult> GetDonGiaChietTinh([FromQuery] int IdPhanLoai)
        {
            var data = await _ChiTietBieuGiaQuery.GetDonGiaChietTinh(IdPhanLoai);
            return Ok(new ApiSuccessResult<List<SelectItem>>(data: data));
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
            var data = await _ChiTietBieuGiaQuery.GetBieuGiaByLoaiBieuGia(loaibieugia);
            return Ok(new ApiSuccessResult<List<SelectItem>>(data: data));
        }

        /// <summary>
        /// Danh sách chi tiết biểu giá có phân trang, tổng số , tìm kiếm
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(ApiSuccessResult<IList<ChiTietBieuGiaResponse>>), (int)HttpStatusCode.OK)] // trả về dữ liệu model cho FE
        public async Task<IActionResult> GetList([FromQuery] ChiTietBieuGiaRequest request)
        {
            var data = await _ChiTietBieuGiaQuery.GetList(request);
            return Ok(new ApiSuccessResult<ChiTietBieuGiaResult>
            {
                Data = data
            });
        }
        [HttpPut]
        [ProducesResponseType(typeof(ApiSuccessResult<bool>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Update([FromBody] UpdateChiTietBieuGiaCommand command)
        {
            var data = await _mediator.Send(command);
            return Ok(new ApiSuccessResult<bool>(data: data, message: string.Format(Resources.MSG_UPDATE_SUCCESS, "chi tiết biểu giá")));
        }

    }
}
