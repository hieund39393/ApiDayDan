using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Authentication.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserClaimController : Controller
    {
        private readonly IMediator _mediator;
        public UserClaimController(IMediator mediator)
        {
            _mediator = mediator;
        }
        ///// <summary>
        ///// Tạo mới phân quyền dữ liệu cho người dùng
        ///// </summary>
        ///// <returns></returns>
        //[HttpPost]
        //public async Task<IActionResult> CreateUserClaim([FromBody] AddUserClaimCommand request)
        //{
        //    var userclaim = await _mediator.Send(request);
        //    return Ok(new ApiSuccessResult<bool>(data: userclaim, message: string.Format(Resources.MSG_CREATE_SUCCESS, "phân quyền dữ liệu cho người dùng")));
        //}

        ///// <summary>
        ///// Thay đổi phân quyền dữ liệu cho người dùng
        ///// </summary>
        ///// <param name="request"></param>
        ///// <returns></returns>
        //[HttpPut]
        //public async Task<IActionResult> UpdateUserClaim([FromBody] UpdateUserClaimCommand request)
        //{
        //    var userclaim = await _mediator.Send(request);
        //    return Ok(new ApiSuccessResult<bool>(data: userclaim, message: string.Format(Resources.MSG_UPDATE_SUCCESS, "phân quyền dữ liệu cho người dùng")));
        //}
    }
}
