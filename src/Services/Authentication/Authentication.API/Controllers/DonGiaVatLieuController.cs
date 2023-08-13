using Authentication.Application.Commands.DonGiaVatLieuCommand;
using Authentication.Application.Model.DonGiaVatLieu;
using Authentication.Application.Queries.DonGiaVatLieuQuery;
using Authentication.Infrastructure.Properties;
using EVN.Core.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using System;
using Azure.Core;

namespace Authentication.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DonGiaVatLieuController : ControllerBase
    {
        private readonly IDonGiaVatLieuQuery _DonGiaVatLieuQuery; //kế thừa interface
        private readonly IMediator _mediator; //kế thừa để sử dụng command

        public DonGiaVatLieuController(IDonGiaVatLieuQuery DonGiaVatLieuQuery, IMediator mediator)
        {
            _DonGiaVatLieuQuery = DonGiaVatLieuQuery;
            _mediator = mediator;
        }

        /// <summary>
        /// Danh sách tất cả đơn giá vật liệu
        /// </summary>
        /// <returns></returns>
        [HttpGet("get-all")]
        [ProducesResponseType(typeof(ApiSuccessResult<List<SelectItem>>), (int)HttpStatusCode.OK)] // trả về dữ liệu model cho FE
        public async Task<IActionResult> GetAll()
        {
            var data = await _DonGiaVatLieuQuery.GetAll();
            return Ok(new ApiSuccessResult<List<SelectItem>>(data: data));
        }

        /// <summary>
        /// Danh sách đơn giá vật liệu có phân trang, tổng số , tìm kiếm
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(ApiSuccessResult<IList<DonGiaVatLieuResponse>>), (int)HttpStatusCode.OK)] // trả về dữ liệu model cho FE
        public async Task<IActionResult> GetListUser([FromQuery] DonGiaVatLieuRequest request)
        {
            var data = await _DonGiaVatLieuQuery.GetList(request);
            return Ok(new ApiSuccessResult<List<DonGiaVatLieuResponse>>
            {
                Data = data,
            });
        }

        /// <summary>
        /// Tạo mới đơn giá vật liệu
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(ApiSuccessResult<bool>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Create([FromBody] CreateDonGiaVatLieuCommand command)
        {
            var user = await _mediator.Send(command);
            return Ok(new ApiSuccessResult<bool>(data: user, message: string.Format(Resources.MSG_CREATE_SUCCESS, "đơn giá vật liệu")));
        }

        /// <summary>
        /// Sửa đơn giá vật liệu
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(typeof(ApiSuccessResult<bool>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Update([FromBody] UpdateDonGiaVatLieuCommand command)
        {
            var user = await _mediator.Send(command);
            return Ok(new ApiSuccessResult<bool>(data: user, message: string.Format(Resources.MSG_UPDATE_SUCCESS, "đơn giá vật liệu")));
        }

        /// <summary>
        /// Xoá đơn giá vật liệu
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(ApiSuccessResult<bool>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var user = await _mediator.Send(new DeleteDonGiaVatLieuCommand(id));
            return Ok(new ApiSuccessResult<bool>(data: user, message: string.Format(Resources.MSG_DELETE_SUCCESS, "đơn giá vật liệu")));
        }

        [HttpGet("export")]
        public async Task<IActionResult> Export()
        {
            var data = await _DonGiaVatLieuQuery.Export();
            var fileName = $"DonGiaVatLieuCapTrenKhong.xlsx";
            Response.Headers.Add("Access-Control-Expose-Headers", "Content-Disposition");
            Response.Headers.Add("file-name", fileName);
            return File(data, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
        }

        [HttpPost("import")]
        public async Task<IActionResult> Import([FromForm] IFormFile file)
        {
                var data = await _DonGiaVatLieuQuery.Import(file);
            return Ok(new ApiSuccessResult<bool>(data: data, message: "Import dữ liệu thành công"));
        }
    }
}
