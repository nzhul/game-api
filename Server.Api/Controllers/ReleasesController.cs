using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Server.Application.Features.Releases;
using Server.Data.Models.Releases;
using System.Threading.Tasks;

namespace Server.Api.Controllers
{
    [AllowAnonymous]
    [Route("[controller]")]
    public class ReleasesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ReleasesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("latest")]
        public async Task<ActionResult<Release>> GetLatestRelease([FromQuery] ReleaseType releaseType)
        {
            return await _mediator.Send(new GetLatestReleaseQuery(releaseType));
        }
    }
}
