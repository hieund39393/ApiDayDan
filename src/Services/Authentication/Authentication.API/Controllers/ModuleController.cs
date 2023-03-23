using Authentication.Application.Commands.ModuleCommand;
using Authentication.Application.Commands.RoleCommand;
using Authentication.Application.Model.Module;
using Authentication.Application.Model.Role;
using Authentication.Application.Model.User;
using Authentication.Application.Queries.ModuleQuery;
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
    //[Authorize]
    public class ModuleController : Controller
    {
        /// <summary>
        /// Quản lý module
        /// </summary>
        private readonly IMediator _mediator;
        private readonly IModuleQuery _moduleQuery;
        private readonly IFileService _fileService;
        public ModuleController(IMediator mediator, IModuleQuery moduleQuery, IFileService fileService)
        {
            _mediator = mediator;
            _moduleQuery = moduleQuery;
            _fileService = fileService;
        }

        /// <summary>
        /// Thêm mới module
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] ModuleCreateOrUpdate command)
        {
            var imageUrl = await _fileService.OnPostUploadAsync(command.FileAnhModule);
            command.Icon = imageUrl;
            var user = await _mediator.Send(command);
            return Ok(new ApiSuccessResult<bool>(data: user, message: string.Format(Resources.MSG_CREATE_SUCCESS, "module")));
        }


        /// <summary>
        /// Thay đổi thông tin module
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] ModuleCreateOrUpdate command)
        {
            var imageUrl = await _fileService.OnPostUploadAsync(command.FileAnhModule);
            command.Icon = imageUrl;
            var user = await _mediator.Send(command);
            return Ok(new ApiSuccessResult<bool>(data: user, message: string.Format(Resources.MSG_UPDATE_SUCCESS, "module")));
        }

        /// <summary>
        /// Xóa module
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        //[HasPermission(Permissions.All, Permissions.UnitDelete)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var user = await _mediator.Send(new ModuleDeteleCommand(id));
            return Ok(new ApiSuccessResult<bool>(data: user, message: string.Format(Resources.MSG_DELETE_SUCCESS, "module")));
        }

        /// <summary>
        /// Danh sách module paging
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> ListModule([FromQuery] ModuleRequest request)
        {
            var data = await _moduleQuery.GetListModule(request);
            return Ok(new ApiSuccessResult<List<ModuleResponse>>(data: data));
        }

    }
}
