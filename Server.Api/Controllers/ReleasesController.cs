using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Server.Api.Auth;
using Server.Application.Features.Releases;
using Server.Data.Models.Releases;
using System.Threading.Tasks;

namespace Server.Api.Controllers
{
    [Route("[controller]")]
    public class ReleasesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ReleasesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("latest"), AllowAnonymous]
        public async Task<ActionResult<Release>> GetLatestRelease([FromQuery] ReleaseType releaseType)
        {
            return await _mediator.Send(new GetLatestReleaseQuery(releaseType));
        }

        [HttpPost, BasicAuthorization]
        public async Task<ActionResult<Release>> CreateRelease([FromBody] CreateReleaseCommand command)
        {
            await _mediator.Send(command);
            return Ok();
        }
    }
}
