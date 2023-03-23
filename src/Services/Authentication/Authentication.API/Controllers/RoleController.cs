using Authentication.Application.Commands.RoleCommand;
using Authentication.Application.Model.Role;
using Authentication.Application.Queries.RoleQuery;
using Authentication.Infrastructure.Properties;
using AutoMapper;
using EVN.Core.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Authentication.API.Controllers
{
    /// <summary>
    /// Quản lý vai trò, quyền
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly IRoleQuery _rolequery;
        public RoleController(IMediator mediator, IMapper mapper, IRoleQuery rolequery)
        {
            _mediator = mediator;
            _mapper = mapper;
            _rolequery = rolequery;
        }

        /// <summary>
        /// Thêm mới vai trò
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] RoleCreateOrUpdate command)
        {
            var user = await _mediator.Send(command);
            return Ok(new ApiSuccessResult<bool>(data: user, message: string.Format(Resources.MSG_CREATE_SUCCESS, "vai trò")));
        }

        /// <summary>
        /// Cập nhật vai trò
        /// </summary>
        /// <param name="id"></param>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] RoleCreateOrUpdate command)
        {
            var user = await _mediator.Send(command);
            return Ok(new ApiSuccessResult<bool>(data: user, message: string.Format(Resources.MSG_UPDATE_SUCCESS, "vai trò")));
        }

        /// <summary>
        /// Xóa vai trò
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var user = await _mediator.Send(new RoleDeteleCommand(id));
            return Ok(new ApiSuccessResult<bool>(data: user, message: string.Format(Resources.MSG_DELETE_SUCCESS, "vai trò")));
        }

        /// <summary>
        /// Lấy danh sách vai trò paging
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetListRole([FromQuery] RoleRequest request)
        {
            var data = await _rolequery.GetListRole(request);
            return Ok(new ApiSuccessResult<IList<RoleResponse>>
            {
                Data = data.Data,
                Paging = data.Paging
            });
        }

        /// <summary>
        /// Lấy danh sách người dùng theo vai trò
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        [HttpGet("get-user-role")]
        public async Task<IActionResult> GetUserRole([FromQuery] Guid roleId)
        {
            var data = await _rolequery.GetUserRole(roleId);
            return Ok(new ApiSuccessResult<List<GetUserRoleResponse>>(data: data));
        }

        [HttpDelete("delete-user-role")]
        public async Task<IActionResult> DeleteUserRole([FromRoute] DeleteUserRoleCommand inputModel)
        {
            var data = await _mediator.Send(inputModel);
            return Ok(new ApiSuccessResult<bool>(data: data, message: string.Format(Resources.MSG_DELETE_SUCCESS, "phân quyền")));
        }

    }
}
