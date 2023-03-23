using Authentication.Application.Commands.UserCommand;
using Authentication.Application.Model.User;
using Authentication.Application.Queries.UserQuery;
using Authentication.Application.Services;
using Authentication.Infrastructure.Properties;
using EVN.Core.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;


namespace Authentication.API.Controllers
{
    /// <summary>
    /// Quản lý user
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IUserQuery _userquery;
        private readonly IFileService _fileService;


        public UserController(IMediator mediator, IUserQuery userquery, IFileService fileService)
        {
            _mediator = mediator;
            _userquery = userquery;
            _fileService = fileService;
        }

        /// <summary>
        /// Thêm mới người dùng
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(ApiSuccessResult<bool>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Create([FromForm] CreateUserCommand command)
        {
            if (command.DefaultPassword == true || command.SSO == true)
            {
                command.Password = "EVNHaNoi@12345";
                command.ComfirmPassword = "EVNHaNoi@12345";
            }

            var imageUrl = await _fileService.OnPostUploadAsync(command.file);
            command.Avatar = imageUrl;
            var user = await _mediator.Send(command);
            return Ok(new ApiSuccessResult<bool>(data: user, message: string.Format(Resources.MSG_CREATE_SUCCESS, "người dùng")));
        }
       /// <summary>
       /// Thay đổi thông tin người dùng
       /// </summary>
       /// <param name="command"></param>
       /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(typeof(ApiSuccessResult<bool>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Update([FromForm] UpdateUserCommand command)
        {
            var imageUrl = await _fileService.OnPostUploadAsync(command.file);
            command.Avatar = imageUrl;
            var user = await _mediator.Send(command);
            return Ok(new ApiSuccessResult<bool>(data: user, message: string.Format(Resources.MSG_UPDATE_SUCCESS, "người dùng")));
        }

        /// <summary>
        /// Xóa người dùng
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(ApiSuccessResult<bool>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var user = await _mediator.Send(new DeleteCommand(id));
            return Ok(new ApiSuccessResult<bool>(data: user, message: string.Format(Resources.MSG_DELETE_SUCCESS, "người dùng")));
        }

        /// <summary>
        /// Thay đổi mật khẩu người dùng
        /// </summary>
        /// <param name="id">Thay đổi mật khẩu</param>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPut("change-password")]
        [ProducesResponseType(typeof(ApiSuccessResult<bool>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> ChangePassWord([FromBody] ChangePassWordCommand command)
        {
            var user = await _mediator.Send(command);
            return Ok(new ApiSuccessResult<bool>(data: user, message: Resources.MSG_CHANGE_PASSWORD_SUCCESS));
        }

        /// <summary>
        /// Lấy danh sách người dùng paging
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(ApiSuccessResult<IList<UserResponse>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetListUser([FromQuery] UserRequest request)
        {
            var data = await _userquery.GetListUser(request);
            return Ok(new ApiSuccessResult<IList<UserResponse>>
            {
                Data = data.Data,
                Paging = data.Paging
            }); ;
        }
        /// <summary>
        /// Đăng ký quyền cho người dùng
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpPost("add-user-roles")]
        [ProducesResponseType(typeof(ApiSuccessResult<bool>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> AddUserRoles([FromBody] AddUserRolesCommand request)
        {
            var user = await _mediator.Send(request);
            return Ok(new ApiSuccessResult<bool>(data: user, message: Resources.MSG_REGISTER_ROLE_SUCCESS));
        }

        /// <summary>
        /// Thông tin chi tiết người dùng
        /// </summary>
        /// <returns></returns>
        [HttpGet("profile")]
        [Authorize]
        [ProducesResponseType(typeof(ApiSuccessResult<UserProfile>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> GetProfile()
        {
            var userProfile = await _userquery.GetProfile();
            return Ok(new ApiSuccessResult<UserProfile>(data: userProfile));
        }

        /// <summary>
        /// Phân quyền chức năng lấy danh sách các quyền theo từng module
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet("get-module-role")]
        [ProducesResponseType(typeof(ApiSuccessResult<List<GetModuleRoleResponse>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetModuleRole([FromQuery] GetUpModuleRoleRequest request)
        {
            var data = await _userquery.GetModuleRole(request);
            return Ok(new ApiSuccessResult<List<GetModuleRoleResponse>>(data: data));
        }

        [HttpPut("forgot-password")]
        [ProducesResponseType(typeof(ApiSuccessResult<bool>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPassword model)
        {
            var data = await _userquery.ForgotPassword(model);
            return Ok(new ApiSuccessResult<bool>(data: data, message: Resources.RESET_PASSWORD_SUCCESS));
        }
    }
}
