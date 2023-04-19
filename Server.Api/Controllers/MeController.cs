using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Server.Application.Features;
using Server.Application.Features.Me;
using Server.Application.Features.Users.Models;
using System.Threading.Tasks;

namespace Server.Api.Controllers
{
    /// <summary>
    /// All endpoints that use the _sessionData.UserId should be here.
    /// </summary>
    [Authorize]
    [Route("[controller]")]
    public class MeController : ControllerBase
    {
        private readonly IMediator _mediator;

        public MeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("friends")]
        public async Task<ActionResult<ListResult<UserListDto>>> GetFriends()
        {
            return await _mediator.Send(new GetFriendsQuery());
        }

        [HttpPost("addfriend/{username}")]
        public async Task<IActionResult> SendFriendRequest(string username)
        {
            await _mediator.Send(new SendFriendRequestCommand(username));
            return Ok();
        }

        [HttpPost("approvefriend/{requestId}")]
        public async Task<IActionResult> ApproveFriendRequest(int requestId)
        {
            await _mediator.Send(new ApproveFriendRequestCommand(requestId));
            return Ok();
        }

        [HttpPost("block/{username}")]
        public async Task<IActionResult> BlockUser(string username)
        {
            await _mediator.Send(new BlockUserCommand(username));
            return Ok();
        }
    }
}
