using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Server.Application.Features;
using Server.Application.Features.Users;
using Server.Application.Features.Users.Models;
using Server.Data.Services.Abstraction;
using System.Threading.Tasks;

namespace Server.Api.Controllers
{
    [Authorize(Policy = "RequireAdmin")]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUsersService _usersService;
        private readonly IMediator _mediator;

        public UsersController(IUsersService usersService, IMediator mediator)
        {
            _usersService = usersService;
            _mediator = mediator;
        }

        [HttpGet("{id}", Name = "GetUser")]
        public async Task<ActionResult<UserDetailedDto>> GetUser(int id)
        {
            return await _mediator.Send(new GetUserQuery(id));
        }

        [HttpGet]
        public async Task<ActionResult<ListResult<UserListDto>>> GetUsers([FromQuery] GetUsersQuery @params)
        {
            return await _mediator.Send(@params);
        }

        [HttpPut("{userId}/setoffline")]
        public async Task<IActionResult> SetOffline(int userId)
        {
            await _mediator.Send(new SetOfflineCommand(userId));
            return Ok();
        }

        [HttpPut("{userId}/setonline/{connectionId}")]
        public async Task<IActionResult> SetOnline(int userId, int connectionId)
        {
            await _mediator.Send(new SetOnlineCommand(userId, connectionId));
            return Ok();
        }
    }
}