//using Authentication.Application.Model.ToDoi;
//using Authentication.Application.Queries.DonViQuery;
//using Authentication.Application.Queries.ModuleQuery;
//using Authentication.Application.Queries.PositionQuery;
//using Authentication.Application.Queries.ToDoiQuery;
//using Authentication.Application.Queries.ViTriCongViecQuery;
//using Authentication.Domain.Dtos.Responses;
//using Authentication.Infrastructure.Properties;
//using ExOne.Core.Models;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;
//using System.Collections.Generic;
//using System.Threading.Tasks;

//namespace WebApplication1.Controllers
//{
//    [ApiController]
//    [Route("api/[controller]")]
//    //[Authorize]
//    public class CommonController : ControllerBase
//    {
//        private readonly IModuleQuery _moduleQuery;
//        private readonly IDonViQuery _donViQuery;
//        private readonly IToDoiQuery _toDoiQuery;
//        private readonly IPositionQuery _positionQuery;
//        private readonly IViTriCongViecQuery _viTriCongViecQuery;

//        public CommonController(IModuleQuery moduleQuery, IDonViQuery donViQuery, IPositionQuery positionQuery, IToDoiQuery toDoiQuery, IViTriCongViecQuery viTriCongViecQuery)
//        {
//            _moduleQuery = moduleQuery;
//            _donViQuery = donViQuery;
//            _positionQuery = positionQuery;
//            _toDoiQuery = toDoiQuery;
//            _viTriCongViecQuery = viTriCongViecQuery;
//        }

//        [HttpGet("modules")]
//        public async Task<List<SelectedItem>> Modules()
//        {
//            return await _moduleQuery.GetDropdownList();
//        }

//        [HttpGet("donvi")]
//        public async Task<List<SelectedItem>> DonVis()
//        {
//            return await _donViQuery.GetDropdownList();
//        }

//        [HttpGet("todoi")]
//        public async Task<List<SelectedItem>> ToDoiByDonViCode([FromQuery] ToDoiByDonViIdRequest request)
//        {
//            return await _toDoiQuery.GetDropdownListByDonViCode(request.DonViId);
//        }

//        [HttpGet("positions")]
//        public async Task<List<LookUpResponse>> Position()
//        {
//            return await _positionQuery.GetDropdownList();
//        }

//        [HttpGet("vitricongviec")]
//        public async Task<List<SelectedItem>> ViTriCongViec()
//        {
//            return await _viTriCongViecQuery.GetDropdownList();
//        }

//        [HttpGet("sso")]
//        public async Task<IActionResult> SSOCheck()
//        {
//            return Ok(new ApiSuccessResult<bool>(data: true, message: Resources.MSG_SSO_CHECK_SUCCESS));
//        }

//        [HttpGet("listteam-by-user")]
//        public async Task<IActionResult> UnitByUser()
//        {
//            var a = await _toDoiQuery.GetListTeamByUser();
//            return Ok();
//        }
//    }
//}
