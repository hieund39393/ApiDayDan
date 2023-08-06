using Authentication.Application.Commands.DonGiaVatLieu_CapNgamCommand;
using Authentication.Application.Queries.DonGiaVatLieu_CapNgamQuery;
using Authentication.Infrastructure.Properties;
using EVN.Core.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using System;
using Authentication.Application.Model.DonGiaVatLieu;
using Authentication.Application.Commands.DonGiaVatLieuCommand;
using Authentication.Application.Queries.CommonQuery;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Authentication.Infrastructure.AggregatesModel.UserAggregate;

namespace Authentication.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DonGiaVatLieuCapNgamController : ControllerBase
    {
        private readonly IDonGiaVatLieu_CapNgamQuery _DonGiaVatLieu_CapNgamQuery; //kế thừa interface
        private readonly IMediator _mediator; //kế thừa để sử dụng command
        private readonly ICommonQuery _commonQuery;

        public DonGiaVatLieuCapNgamController(IDonGiaVatLieu_CapNgamQuery DonGiaVatLieu_CapNgamQuery, IMediator mediator, ICommonQuery commonQuery)
        {
            _DonGiaVatLieu_CapNgamQuery = DonGiaVatLieu_CapNgamQuery;
            _mediator = mediator;
            _commonQuery = commonQuery;
        }

        /// <summary>
        /// Danh sách tất cả đơn giá vật liệu cáp ngầm
        /// </summary>
        /// <returns></returns>
        [HttpGet("get-all")]
        [ProducesResponseType(typeof(ApiSuccessResult<List<SelectItem>>), (int)HttpStatusCode.OK)] // trả về dữ liệu model cho FE
        public async Task<IActionResult> GetAll()
        {
            var data = await _DonGiaVatLieu_CapNgamQuery.GetAll();
            return Ok(new ApiSuccessResult<List<SelectItem>>(data: data));
        }

        /// <summary>
        /// Danh sách đơn giá vật liệu cáp ngầm có phân trang, tổng số , tìm kiếm
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(ApiSuccessResult<IList<DonGiaVatLieuResponse>>), (int)HttpStatusCode.OK)] // trả về dữ liệu model cho FE
        public async Task<IActionResult> GetListUser([FromQuery] DonGiaVatLieuRequest request)
        {
            var listVungKhuVuc = _commonQuery.ListVungKhuVuc();
            var data = await _DonGiaVatLieu_CapNgamQuery.GetList(request);
            foreach (var item in data)
            {
                item.TenVungKhuVuc = listVungKhuVuc.FirstOrDefault(x => x.Value == item.VungKhuVuc.ToString())?.Name;
            }
            return Ok(new ApiSuccessResult<List<DonGiaVatLieuResponse>>
            {
                Data = data,
            });
        }

        /// <summary>
        /// Tạo mới đơn giá vật liệu cáp ngầm
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(ApiSuccessResult<bool>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Create([FromBody] CreateDonGiaVatLieu_CapNgamCommand command)
        {
            var user = await _mediator.Send(command);
            return Ok(new ApiSuccessResult<bool>(data: user, message: string.Format(Resources.MSG_CREATE_SUCCESS, "đơn giá vật liệu cáp ngầm")));
        }

        /// <summary>
        /// Sửa đơn giá vật liệu cáp ngầm
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(typeof(ApiSuccessResult<bool>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Update([FromBody] UpdateDonGiaVatLieu_CapNgamCommand command)
        {
            var user = await _mediator.Send(command);
            return Ok(new ApiSuccessResult<bool>(data: user, message: string.Format(Resources.MSG_UPDATE_SUCCESS, "đơn giá vật liệu cáp ngầm")));
        }

        /// <summary>
        /// Xoá đơn giá vật liệu cáp ngầm
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(ApiSuccessResult<bool>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var user = await _mediator.Send(new DeleteDonGiaVatLieu_CapNgamCommand(id));
            return Ok(new ApiSuccessResult<bool>(data: user, message: string.Format(Resources.MSG_DELETE_SUCCESS, "đơn giá vật liệu cáp ngầm")));
        }

        [HttpGet("export")]
        public async Task<IActionResult> Export()
        {
            var data = await _DonGiaVatLieu_CapNgamQuery.Export();
            var fileName = $"DonGiaVatLieuCapNgam.xlsx";
            Response.Headers.Add("Access-Control-Expose-Headers", "Content-Disposition");
            Response.Headers.Add("file-name", fileName);
            return File(data, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
        }

        [HttpGet("import")]
        public async Task<IActionResult> Import([FromForm] IFormFile file)
        {
            var data = await _DonGiaVatLieu_CapNgamQuery.Import(file);
            return Ok(new ApiSuccessResult<bool>(data: data, message: "Import dữ liệu thành công"));
        }
    }
}
