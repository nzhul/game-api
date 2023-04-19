using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Server.Application.Features.UnitConfigurations;
using Server.Data.MapEntities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Server.Api.Controllers
{
    [Authorize(Policy = "RequireAdmin")]
    [Route("unit-configurations")]
    public class UnitConfigurationsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UnitConfigurationsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{creatureType?}")]
        public async Task<Dictionary<CreatureType, UnitConfigurationDto>> GetConfiguration(CreatureType? creatureType = null)
        {
            return await _mediator.Send(new UnitConfigurationQuery(creatureType));
        }
    }
}
