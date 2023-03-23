using Authentication.Application.Commands.AuthCommand;
using Authentication.Application.Model.Auth;
using EVN.Core.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Authentication.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        /// <summary>
        /// Constructor
        /// </summary>
        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("login")]
        [ProducesResponseType(typeof(ApiSuccessResult<LoginResponse>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Login([FromBody] LoginCommand commamd)
        {
            var login = await _mediator.Send(commamd);
            return Ok(new ApiSuccessResult<string>(data: login.AccessToken));
        }

    }
}
