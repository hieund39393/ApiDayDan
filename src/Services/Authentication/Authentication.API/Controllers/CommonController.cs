using Authentication.Application.Model.Menu;
using Authentication.Application.Queries.CommonQuery;
using Authentication.Application.Queries.ModuleQuery;
using Authentication.Infrastructure.Properties;
using EVN.Core.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //[Authorize]
    public class CommonController : ControllerBase
    {
        private readonly ICommonQuery _commonQuery;

        public CommonController(ICommonQuery commonQuery)
        {
            _commonQuery = commonQuery;
        }

        [HttpGet("list-module")]
        public async Task<IActionResult> ListModule()
        {
            var data = await _commonQuery.ListModule();
            return Ok(new ApiSuccessResult<List<SelectItem>>(data: data));
        }
        [HttpGet("list-menu")]
        public async Task<IActionResult> ListMenu()
        {
            var data = await _commonQuery.ListMenu();
            return Ok(new ApiSuccessResult<List<MenuItemResponse>>(data: data));
        }

        [HttpGet("list-nhom-quyen")]
        public async Task<IActionResult> ListNhomQuyen()
        {
            var data = await _commonQuery.ListNhomQuyen();
            return Ok(new ApiSuccessResult<List<SelectItem>>(data: data));
        }

        [HttpGet("list-chuc-vu")]
        public async Task<IActionResult> ListChucVu()
        {
            var data = await _commonQuery.ListChucVu();
            return Ok(new ApiSuccessResult<List<SelectItem>>(data: data));
        }

        [HttpGet("list-vung-khuvuc")]
        public async Task<IActionResult> ListVungKhuVuc()
        {
            var data = _commonQuery.ListVungKhuVuc();
            return Ok(new ApiSuccessResult<List<SelectItem>>(data: data));
        }
    }
}
