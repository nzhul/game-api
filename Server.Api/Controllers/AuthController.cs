using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Server.Application.Features.Auth;
using Server.Data.Users;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Server.Api.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly SignInManager<User> _signInManager;

        private readonly IMediator _mediator;

        public AuthController(
            SignInManager<User> signInManager,
            IMediator mediator)
        {
            this._signInManager = signInManager;
            _mediator = mediator;
        }

        [HttpPost("login")]
        public async Task<ActionResult<LoginResult>> Login([FromBody] LoginQuery query, CancellationToken cancellationToken)
        {
            return await _mediator.Send(query, cancellationToken);
        }

        [HttpPost("register")]
        public async Task<ActionResult> Register([FromBody] RegisterCommand command, CancellationToken cancellationToken)
        {
            await _mediator.Send(command, cancellationToken);
            return StatusCode((int)HttpStatusCode.Created);
        }

        [Authorize]
        [HttpGet("logout")]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Ok();
        }
    }
}