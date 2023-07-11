using Authentication.Application.Commands.CauHinhCommand;
using Authentication.Application.Model.CauHinh;
using Authentication.Application.Queries.CommonQuery;
using Authentication.Infrastructure.AggregatesModel.UserAggregate;
using Authentication.Infrastructure.Properties;
using EVN.Core.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Authentication.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CauHinhController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ICommonQuery _commonQuery;

        public CauHinhController(IMediator mediator, ICommonQuery commonQuery)
        {
            _mediator = mediator;
            _commonQuery = commonQuery;
        }

        [HttpPost]
        public async Task<IActionResult> CreateCauHinh([FromBody] CreateCauHinhCommand request)
        {
            var data = await _mediator.Send(request);
            return Ok(new ApiSuccessResult<bool>(data: data, message: string.Format(Resources.MSG_CREATE_SUCCESS, "cấu hình")));
        }

        [HttpGet]
        public async Task<IActionResult> GetListCauHinh([FromQuery] GetListCauHinhRequest request)
        {
            var data = await _commonQuery.ListCauHinh(request);
            return Ok(new ApiSuccessResult<object>(data: data));
        }
    }
}
