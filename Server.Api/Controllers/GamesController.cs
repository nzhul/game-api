using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Server.Application.Features.Games;
using System.Threading.Tasks;

namespace Server.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class GamesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public GamesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPut("{id}/{winnerid}/end")]
        public async Task<ActionResult> EndGame(int id, int winnerId)
        {
            await _mediator.Send(new EndGameCommand(id, winnerId));
            return Ok();
        }
    }
}
