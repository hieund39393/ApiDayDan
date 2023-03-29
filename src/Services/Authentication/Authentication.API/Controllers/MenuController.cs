using Authentication.Application.Model.Menu;
using Authentication.Application.Model.Role;
using Authentication.Application.Model.User;
using Authentication.Application.Queries.MenuQuery;
using Authentication.Application.Queries.RoleQuery;
using Authentication.Application.Services;
using Authentication.Infrastructure.Properties;
using AutoMapper;
using EVN.Core.Attributes;
using EVN.Core.Exceptions;
using EVN.Core.Models;
using EVN.Core.Properties;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static EVN.Core.Common.AppConstants;

namespace Authentication.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class MenuController : Controller
    {
        /// <summary>
        /// Quản lý Menu
        /// </summary>
        private readonly IMediator _mediator;
        private readonly IMenuQuery _MenuQuery;
        public MenuController(IMediator mediator, IMenuQuery MenuQuery)
        {
            _mediator = mediator;
            _MenuQuery = MenuQuery;
        }

        ///// <summary>
        ///// Thêm mới Menu
        ///// </summary>
        ///// <param name="command"></param>
        ///// <returns></returns>
        //[HttpPost]
        //public async Task<IActionResult> Create([FromForm] MenuCreateOrUpdate command)
        //{
        //    var imageUrl = await _fileService.OnPostUploadAsync(command.FileAnhMenu);
        //    command.Icon = imageUrl;
        //    var user = await _mediator.Send(command);
        //    return Ok(new ApiSuccessResult<bool>(data: user, message: string.Format(Resources.MSG_CREATE_SUCCESS, "Menu")));
        //}


        ///// <summary>
        ///// Thay đổi thông tin Menu
        ///// </summary>
        ///// <param name="command"></param>
        ///// <returns></returns>
        //[HttpPut]
        //public async Task<IActionResult> Update([FromBody] MenuCreateOrUpdate command)
        //{
        //    var imageUrl = await _fileService.OnPostUploadAsync(command.FileAnhMenu);
        //    command.Icon = imageUrl;
        //    var user = await _mediator.Send(command);
        //    return Ok(new ApiSuccessResult<bool>(data: user, message: string.Format(Resources.MSG_UPDATE_SUCCESS, "Menu")));
        //}

        ///// <summary>
        ///// Xóa Menu
        ///// </summary>
        ///// <param name="id"></param>
        ///// <returns></returns>
        ////[HasPermission(Permissions.All, Permissions.UnitDelete)]
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> Delete([FromRoute] Guid id)
        //{
        //    var user = await _mediator.Send(new MenuDeteleCommand(id));
        //    return Ok(new ApiSuccessResult<bool>(data: user, message: string.Format(Resources.MSG_DELETE_SUCCESS, "Menu")));
        //}

        /// <summary>
        /// Danh sách menu phân trang
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> ListMenu([FromQuery] MenuRequest request)
        {
            var data = await _MenuQuery.GetListMenu(request);
            return Ok(new ApiSuccessResult<IList<MenuResponse>>
            {
                Data = data.Data,
                Paging = data.Paging
            });
        }

    }
}
