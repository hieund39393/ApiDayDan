using Authentication.Application.Commands.MenuCommand;
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

        /// <summary>
        /// Thêm mới Menu
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateOrEditMenuCommand command)
        {
            var user = await _mediator.Send(command);
            return Ok(new ApiSuccessResult<bool>(data: user, message: command.Id == Guid.Empty ? string.Format(Resources.MSG_CREATE_SUCCESS, "trang") :
                string.Format(Resources.MSG_UPDATE_SUCCESS, "trang")));
        }



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
