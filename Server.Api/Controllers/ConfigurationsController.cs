using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Server.Application.Features.UnitConfigurations;
using Server.Common.Settings;
using Server.Data.MapEntities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Server.Api.Controllers
{
    //[Authorize(Policy = "RequireAdmin")]
    [AllowAnonymous]
    [Route("configurations")]
    public class ConfigurationsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly GlobalSettings _globalSettings;

        public ConfigurationsController(IMediator mediator, IOptions<GlobalSettings> globalSettings)
        {
            _mediator = mediator;
            _globalSettings = globalSettings.Value;
        }

        [HttpGet("global")]
        public async Task<GlobalSettings> GetGlobalSettings()
        {
            return await Task.FromResult(_globalSettings);
        }

        [HttpGet("units/{creatureType?}")]
        public async Task<Dictionary<CreatureType, UnitConfigurationDto>> GetConfiguration(CreatureType? creatureType = null)
        {
            return await _mediator.Send(new UnitConfigurationQuery(creatureType));
        }
    }
}
