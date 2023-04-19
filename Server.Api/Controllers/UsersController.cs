using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Server.Application.Features;
using Server.Application.Features.Users;
using Server.Application.Features.Users.Models;
using Server.Data.Services.Abstraction;
using Server.Data.Users;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Server.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUsersService _usersService;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public UsersController(IUsersService usersService, IMapper mapper, IMediator mediator)
        {
            _usersService = usersService;
            _mapper = mapper;
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

        [HttpGet("{userId}/friends")]
        public async Task<IActionResult> GetFriends(int userId)
        {
            if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
            {
                return Unauthorized();
            }

            IEnumerable<User> friends = await _usersService.GetFriends(userId);

            if (friends != null)
            {
                IEnumerable<UserListDto> usersToReturn = _mapper.Map<IEnumerable<UserListDto>>(friends);
                return Ok(usersToReturn);
            }
            else
            {
                return NoContent();
            }
        }

        [HttpPut("{userId}/setoffline")]
        public async Task<IActionResult> SetOffline(int userId)
        {
            string result = await _usersService.SetOffline(userId);

            if (string.IsNullOrEmpty(result))
            {
                return Ok();
            }
            else
            {
                return BadRequest(result);
            }
        }

        [HttpPut("{userId}/setonline/{connectionId}")]
        public async Task<IActionResult> SetOnline(int userId, int connectionId)
        {
            string result = await _usersService.SetOnline(userId, connectionId);

            if (string.IsNullOrEmpty(result))
            {
                return Ok();
            }
            else
            {
                return BadRequest(result);
            }
        }
    }
}