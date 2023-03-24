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

        [HttpGet("list-menu")]
        public async Task<IActionResult> ListMenu()
        {
            var data = await _commonQuery.ListMenu();
            return Ok(new ApiSuccessResult<List<MenuItemResponse>>(data: data));
        }
    }
}
